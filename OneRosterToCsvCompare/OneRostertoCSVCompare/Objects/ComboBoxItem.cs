using System.Collections.Generic;

namespace OneRostertoCSVCompare.Objects
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public List<string> Ignore { get; set; } = new List<string>();

        public override string ToString()
        {
            return Text;
        }
    }
}
