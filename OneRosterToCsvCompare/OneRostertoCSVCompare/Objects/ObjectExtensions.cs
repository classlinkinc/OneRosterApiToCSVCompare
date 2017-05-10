using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;

namespace OneRostertoCSVCompare.Objects
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Renames Dictionary Key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="fromKey"></param>
        /// <param name="toKey"></param>
        public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        /// <summary>
        /// String to Base64
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64(this string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Base64 to String
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FromBase64(this string input)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }

        /// <summary>
        /// Turns Dictionary into T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            Type someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        /// <summary>
        /// Turns Dictionary into T1 with value being of type T2
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T1 ToObject<T1, T2>(this IDictionary<string, T2> source)
            where T1 : class, new()
        {
            var someObject = new T1();
            Type someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        /// <summary>
        /// Turns object into dictionary with object vaule
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public static IDictionary<string, string> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => (string)propInfo.GetValue(source, null),
                StringComparer.OrdinalIgnoreCase
            );

        }

        #region JSON to dictionary
        /// <summary>
        /// Turns JSON to nested dictionary
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this JObject json)
        {
            var valuePairs = json.ToObject<Dictionary<string, object>>();
            ProcessJObjectProperties(valuePairs);
            ProcessJArrayProperties(valuePairs);
            return valuePairs;
        }

        /// <summary>
        /// Adds each "JObject" to the dictionary as a dictionary
        /// </summary>
        /// <param name="pvp"></param>
        private static void ProcessJObjectProperties(IDictionary<string, object> pvp)
        {
            var opn = (from p in pvp
                let pn = p.Key
                let v = p.Value
                where v is JObject
                select pn).ToList();

            opn.ForEach(pn => pvp[pn] = ToDictionary((JObject)pvp[pn]));
        }

        /// <summary>
        /// Adds each "JArray" to the dictionary as an Array
        /// </summary>
        /// <param name="pvp"></param>
        private static void ProcessJArrayProperties(IDictionary<string, object> pvp)
        {
            var apn = (from p in pvp
                let pn = p.Key
                let v = p.Value
                where v is JArray
                select pn).ToList();

            apn.ForEach(pn => pvp[pn] = ToArray((JArray)pvp[pn]));
        }

        /// <summary>
        /// Converts JArray/JToken to an Array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static object[] ToArray(this JToken array)
        {
            return array.ToObject<object[]>().Select(ProcessArrayEntry).ToArray();
        }

        /// <summary>
        /// Returns value as a JObject or a JArray depending on type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object ProcessArrayEntry(object value)
        {
            var o = value as JObject;
            var array = value as JArray;
            return o != null ? ToDictionary(o) : array != null ? ToArray(array) : value;
        }

        /// <summary>
        /// Flattens the nested dictionary
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Flatten(this IDictionary<string, object> input)
        {
            var output = new Dictionary<string, object>();
            foreach (var item in input)
            {
                var s = item.Value as string;
                if (s != null)
                {
                    output.Add(item.Key, s);
                }
                else if (item.Value is DateTime)
                {
                    output.Add(item.Key, item.Value);
                }
                else
                {
                    var itemArr = item.Value as object[];
                    var itemDict = item.Value as Dictionary<string, object>;
                    if (itemArr != null)
                    {
                        for (var i = 0; i < itemArr.Length; i++)
                        {
                            output.AddRange(Flatten(item.Key + "." + i,
                                (IEnumerable<KeyValuePair<string, object>>)itemArr[i]));
                        }
                    }
                    else if (itemDict != null)
                    {
                        output.AddRange(Flatten(item.Key, itemDict));
                    }
                    else
                    {
                        // unknown dynamic type... get type and set a breakpoint for debug
                        Type t = item.Value.GetType();
                    }

                }
            }

            return output;
        }

        /// <summary>
        /// Flattens the nested part of the dictionary
        /// </summary>
        /// <param name="root"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static Dictionary<string, object> Flatten(string root, IEnumerable<KeyValuePair<string, object>> input)
        {
            var output = new Dictionary<string, object>();
            foreach (var item in input)
            {
                var s = item.Value as string;
                if (s != null)
                {
                    output.Add(root + "." + item.Key, s);
                }
                else
                {
                    output.AddRange(Flatten(item.Key, (Dictionary<string, object>)item.Value));
                }
            }
            return output;
        }

        /// <summary>
        /// Simple AddRange for Dictionary 
        /// or anything that implements ICollection and IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            foreach (T element in source)
            {
                target.Add(element);
            }
        }

        /// <summary>
        /// Converts Dictionary(string, object) to 
        /// Dictionary(string, string)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToStringDictionary(this IDictionary<string, object> input)
        {
            return input.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        }
        #endregion
    }
}
