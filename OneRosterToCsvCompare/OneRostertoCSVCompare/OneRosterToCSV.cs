using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LumenWorks.Framework.IO.Csv;
using Newtonsoft.Json.Linq;
using OneRosterOAuth;
using OneRostertoCSVCompare.Objects;

namespace OneRostertoCSVCompare
{
    public partial class OneRosterToCSV : Form
    {
        public OneRosterToCSV()
        {
            InitializeComponent();
            AddDropdown();
            LoadSettings();
        }

        private readonly DataTable _csvDataTable = new DataTable();
        private readonly DataTable _apiDataTable = new DataTable();
        private readonly DataTable _resultDataTable = new DataTable();
        private OneRosterConnection _oneRosterConnection;
        private readonly List<BackgroundWorker> _runningWorkers = new List<BackgroundWorker>();
        
        private int _currentStep;

        public void AddDropdown()
        {
            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p0 academicSessions",
                Value = @"/learningdata/v1/academicSessions"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p0 classes",
                Value = @"/learningdata/v1/classes"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p0 courses",
                Value = @"/learningdata/v1/classes"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p0 demographics",
                Value = @"/learningdata/v1/demographics"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p0 enrollments",
                Value = @"/learningdata/v1/classes"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p0 orgs",
                Value = @"/learningdata/v1/orgs"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p0 users",
                Value = @"/learningdata/v1/users"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p1 academicSessions",
                Value = @"/ims/oneroster/v1p1/academicSessions"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p1 classes",
                Value = @"/ims/oneroster/v1p1/classes"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p1 courses",
                Value = @"/ims/oneroster/v1p1/courses"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p1 demographics",
                Value = @"/ims/oneroster/v1p1/demographics"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p1 enrollments",
                Value = @"/ims/oneroster/v1p1/enrollments"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p1 orgs",
                Value = @"/ims/oneroster/v1p1/orgs"
            });

            csvType.Items.Add(new ComboBoxItem
            {
                Text = @"v1p1 users",
                Value = @"/ims/oneroster/v1p1/users"
            });
        }

        public bool CheckFields()
        {
            if (string.IsNullOrEmpty(oneRosterUrl.Text))
            {
                MessageBox.Show(@"Please enter the OneRoster Server API URL", @"Error");
                oneRosterUrl.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(oneRosterKey.Text))
            {
                MessageBox.Show(@"Please enter the OneRoster Server API KEY", @"Error");
                oneRosterKey.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(oneRosterSecret.Text))
            {
                MessageBox.Show(@"Please enter the OneRoster Server API Secret", @"Error");
                oneRosterSecret.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(csvFile.Text))
            {
                if(!File.Exists(csvFile.Text))
                MessageBox.Show(@"Please enter a valid OneRoster CSV", @"Error");
                csvBrowse.PerformClick();
                return false;
            }


            if (string.IsNullOrEmpty(csvType.SelectedItem?.ToString()))
            {
                MessageBox.Show(@"Please select the type of OneRoster CSV", @"Error");
                csvType.DroppedDown = true;
                return false;
            }

            return true;
        }

        private void CsvBrowse_Click(object sender, EventArgs e)
        {
            csvFileDialog.Filter = @"CSV Files (*.csv)|*.csv";
            DialogResult csvResult = csvFileDialog.ShowDialog();
            if (csvResult == DialogResult.OK)
            {
                csvFile.Text = csvFileDialog.FileName;
            }
        }

        private void RunNow_Click(object sender, EventArgs e)
        {
            if (!CheckFields()) return;
            _oneRosterConnection = new OneRosterConnection(oneRosterKey.Text, oneRosterSecret.Text);

            compareProgressBar.Minimum = 0;
            compareProgressBar.Maximum = 100;
            compareProgressBar.Step = 0;

            csvStatus.Text = @"Waiting...";
            apiStatus.Text = @"Waiting...";
            compareStatus.Text = @"Waiting...";

            _runningWorkers.Add(csvReaderWorker);
            _runningWorkers.Add(oneRosterWorker);
            csvReaderWorker.RunWorkerAsync();
            oneRosterWorker.RunWorkerAsync();

            _resultDataTable.PrimaryKey = new DataColumn[0];
            _resultDataTable.Columns.Clear();
            _resultDataTable.Clear();
            viewResults.Enabled = false;
            runNow.Enabled = false;
        }

        // CSV Worker
        private void Csv_DoWork(object sender, DoWorkEventArgs e)
        {
            var fileName = csvFile.Text;

            using (var csvReader = new CsvReader(new StreamReader(fileName), true))
            {
                Invoke(new Action(() =>
                {
                    csvStatus.Text = @"Parsing CSV...";
                }));
                _csvDataTable.PrimaryKey = new DataColumn[0];
                _csvDataTable.Clear();
                _csvDataTable.Columns.Clear();
                _csvDataTable.Load(csvReader);
                try
                {
                    _csvDataTable.PrimaryKey = new[] {_csvDataTable.Columns["sourcedId"]};
                }
                catch
                {
                    MessageBox.Show(@"There is a duplicate sourcedId in your CSV");
                }
                
                Invoke(new Action(() =>
                {
                    csvStatus.Text = $@"Parsed {_csvDataTable.Rows.Count} rows.";
                }));
            }
        }

        // OneRoster Worker
        private void OnerRoster_DoWork(object sender, DoWorkEventArgs e)
        {
            var baseUrl = string.Empty;
            var endpoint = string.Empty;
            Invoke(new Action(() =>
            {
                baseUrl = oneRosterUrl.Text.TrimEnd('/');
                endpoint = (csvType.SelectedItem as ComboBoxItem)?.Value;
            }));

            if (string.IsNullOrEmpty(endpoint)) return;

            var url = baseUrl + endpoint;
            _apiDataTable.PrimaryKey = new DataColumn[0];
            _apiDataTable.Clear();
            _apiDataTable.Columns.Clear();

            Invoke(new Action(() =>
            {
                apiStatus.Text = @"Retrieving objects from OneRoster API.";
            }));


            var lastRun = 0;
            const int limit = 1000;
            var offset = 0;
            do
            {
                try
                {
                    using (Stream response = _oneRosterConnection.makeRequest(url + $"?limit={limit}&offset={offset}")
                        .GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(response))
                        {
                            var jobj = JObject.Parse(streamReader.ReadToEnd()).ToDictionary();
                            var j2 = (jobj[jobj.Keys.First()] as dynamic[])
                                .Select(obj => ((IDictionary<string, object>) obj).Flatten().ToStringDictionary())
                                .ToList();
                            lastRun = j2.Count;

                            if (lastRun > 0)
                            {
                                    foreach (var col in j2.SelectMany(q => q.Keys).Distinct())
                                    {
                                    
                                        var colName = col;
                                        if (!col.Contains("metadata") && colName.Contains("."))
                                        {
                                            if (!colName.Contains("sourcedId") || colName.Contains("children")) continue;
                                            var colParts = colName.Split('.');
                                            colName = colParts[0];
                                            for (var i = 1; i < colParts.Length; i++)
                                            {
                                                colName +=
                                                    Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(
                                                        colParts[i]);
                                            }
                                        }
                                        if (!_apiDataTable.Columns.Contains(colName))
                                            _apiDataTable.Columns.Add(new DataColumn(colName));
                                    }

                                    if(_apiDataTable.PrimaryKey.Length == 0)
                                        _apiDataTable.PrimaryKey = new[] { _apiDataTable.Columns["sourcedId"] };

                                foreach (var j in j2)
                                {
                                    DataRow r = _apiDataTable.NewRow();
                                    foreach (var k in j.Keys)
                                    {
                                        var colName = k;
                                        if (!colName.Contains("metadata") && colName.Contains("."))
                                        {
                                            if (!colName.Contains("sourcedId") || colName.Contains("children")) continue;
                                            var colParts = k.Split('.');
                                            colName = colParts[0];
                                            for (var i = 1; i < colParts.Length; i++)
                                            {
                                                colName +=
                                                    Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(
                                                        colParts[i]);
                                            }
                                        }
                                        r[colName] = j[k];
                                    }
                                    _apiDataTable.Rows.Add(r);
                                }
                            }

                            Invoke(new Action(() =>
                            {
                                apiStatus.Text = $@"Retrieved {_apiDataTable.Rows.Count} objects.";
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"ERROR: " + ex.Message);
                }
                
                offset += limit;
            } while (lastRun > 0);
        }

        // Compare Worker
        private void CompareWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var colList = new List<string>();
            var count = 1;
            Invoke(new Action(() =>
            {
                compareStatus.Text = @"Finding fields missing from CSV";
            }));

            _resultDataTable.Columns.Add("id");
            _resultDataTable.Columns.Add("sourcedId");
            _resultDataTable.Columns.Add("status");
            _resultDataTable.PrimaryKey = new[] { _resultDataTable.Columns["id"] };

            foreach (DataColumn col in _apiDataTable.Columns)
            {
                if (!_csvDataTable.Columns.Contains(col.ToString()))
                {
                    DataRow dr = _resultDataTable.NewRow();
                    dr["id"] = count;
                    dr["sourcedId"] = col.ToString();
                    dr["status"] = "Column missing in CSV";
                    _resultDataTable.Rows.Add(dr);
                    count++;
                }
                else
                {
                    colList.Add(col.ToString());
                }
            }


            Invoke(new Action(() =>
            {
                compareStatus.Text = @"Finding fields missing from API";
            }));
            foreach (object col in _csvDataTable.Columns)
            {
                if (!_apiDataTable.Columns.Contains(col.ToString()))
                {
                    DataRow dr = _resultDataTable.NewRow();
                    dr["id"] = count;
                    dr["sourcedId"] = col.ToString();
                    dr["status"] = "Column missing in API";
                    _resultDataTable.Rows.Add(dr);
                    count++;
                }
                else
                {
                    colList.Add(col.ToString());
                }
            }

            Invoke(new Action(() =>
            {
                compareStatus.Text = @"Finding duplicate sourcedIds from CSV";
            }));

            var dupeList = _csvDataTable
                .AsEnumerable()
                .GroupBy(c => c.Field<string>("sourcedId"))
                .Where(c => c.Count() > 1)
                .Select(c => new Result
                {
                    SourcedId = c.Key,
                    Status = "Duplicate sourcedId"
                });
            foreach (Result d in dupeList)
            {
                DataRow dr = _resultDataTable.NewRow();
                dr["id"] = count;
                dr["sourcedId"] = d.SourcedId;
                dr["status"] = d.Status;
                _resultDataTable.Rows.Add(dr);
                count++;
            }

            Invoke(new Action(() =>
            {
                compareStatus.Text = @"Finding sourcedIds missing from CSV";
            }));
            // Get rows not found in CSV
            var csvMiss = _apiDataTable.AsEnumerable()
                .Select(r => r.Field<string>("sourcedId"))
                .Except(_csvDataTable.AsEnumerable()
                    .Where(r => !string.Equals("toBeDeleted", r.Field<string>("status")))
                    .Select(r => r.Field<string>("sourcedId")))
                .Select(c => new Result
                {
                    SourcedId = c,
                    Status = "Not found in CSV"
                });
            foreach (Result c in csvMiss)
            {
                DataRow dr = _resultDataTable.NewRow();
                dr["id"] = count;
                dr["sourcedId"] = c.SourcedId;
                dr["status"] = c.Status;
                _resultDataTable.Rows.Add(dr);
                count++;
            }

            Invoke(new Action(() =>
            {
                compareStatus.Text = @"Finding sourcedIds missing from API";
            }));
            // Get rows not found in API
            var apiMiss = _csvDataTable.AsEnumerable()
                .Select(r => r.Field<string>("sourcedId"))
                .Except(_apiDataTable.AsEnumerable()
                    .Select(r => r.Field<string>("sourcedId")))
                .Select(c => new Result
                {
                    SourcedId = c,
                    Status = "Not found in API"
                });

            foreach (Result a in apiMiss)
            {
                DataRow dr = _resultDataTable.NewRow();
                dr["id"] = count;
                dr["sourcedId"] = a.SourcedId;
                dr["status"] = a.Status;
                _resultDataTable.Rows.Add(dr);
                count++;
            }

            Invoke(new Action(() =>
            {
                compareStatus.Text = @"Finding sourcedIds in both CSV and API";
            }));
            // Get rows in both CSV and API
            var matchSourcesIds = from csv in _csvDataTable.AsEnumerable()
                join api in _apiDataTable.AsEnumerable()
                on csv.Field<string>("sourcedId") equals api.Field<string>("sourcedId")
                select csv.Field<string>("sourcedId");

            colList = colList.Distinct().ToList();

            var sourcesIds = matchSourcesIds as string[] ?? matchSourcesIds.ToArray();
            Invoke(new Action(() =>
            {
                compareProgressBar.Maximum = sourcesIds.Length;
                compareProgressBar.Minimum = 0;
                compareProgressBar.Value = 0;
            }));
            _currentStep = 0;

            Invoke(new Action(() =>
            {
                compareStatus.Text = @"Finding values that do not match by sourcedId";
            }));
            // Fields that do not match
            var timer = new System.Timers.Timer
            {
                Enabled = true,
                Interval = 5000
        };
            timer.Start();
            timer.Elapsed += Timer_Tick;
            foreach (var sourcedId in sourcesIds)
            {
                DataRow csvRow = _csvDataTable.AsEnumerable().First(c => c.Field<string>("sourcedId").Equals(sourcedId));
                DataRow apiRow = _apiDataTable.AsEnumerable().First(c => c.Field<string>("sourcedId").Equals(sourcedId));
                foreach (var col in colList)
                {
                    if(string.Equals("status", col, StringComparison.OrdinalIgnoreCase) 
                        || string.Equals("dateLastModified", col, StringComparison.OrdinalIgnoreCase)) continue;
                    if (Equals(csvRow[col].ToString().Trim(), apiRow[col].ToString().Trim())) continue;

                    DataRow dr = _resultDataTable.NewRow();
                    dr["id"] = count;
                    dr["sourcedId"] = sourcedId;
                    dr["status"] = $"CSV and API differ in field {col}: CSV: {csvRow[col]} API: {apiRow[col]}";
                    _resultDataTable.Rows.Add(dr);
                    count++;
                }
                _currentStep++;
            }
        }

        private void CompareWorker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                runNow.Enabled = true;
                viewResults.Enabled = true;
                compareStatus.Text = @"Complete";
            }));
        }

        private void Worker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            var worker = (BackgroundWorker) sender;
            _runningWorkers.Remove(worker);
            if (_runningWorkers.Count > 0) return;

            compareWorker.RunWorkerAsync();
        }

        private void ViewResults_Click(object sender, EventArgs e)
        {
            var popup = new ScanResult(_resultDataTable);
            popup.ShowDialog();
            popup.Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                compareProgressBar.Value = _currentStep;
            }));
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            saveSettingsBtn.Enabled = false;
            if (!string.IsNullOrEmpty(Properties.Settings.Default["key"].ToString()) ||
                !string.IsNullOrEmpty(Properties.Settings.Default["secret"].ToString()) ||
                !string.IsNullOrEmpty(Properties.Settings.Default["url"].ToString()) ||
                !string.IsNullOrEmpty(Properties.Settings.Default["filePath"].ToString()) ||
                (int)Properties.Settings.Default["fileTypeIndex"] != 0)
            {
                DialogResult response = MessageBox.Show(@"Some settings are already set
Overwrite?", @"Save Settings",
                    MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    MessageBox.Show(UpdateSettings() ? @"Settings updated" : @"An error occured while saving settings");
                }
                else
                {
                    MessageBox.Show(@"Settings not updated");
                }
            }
            else
            {
                MessageBox.Show(UpdateSettings() ? @"Settings saved" : @"An error occured while saving settings");
            }
            saveSettingsBtn.Enabled = true;
        }

        private bool UpdateSettings()
        {
            try
            {
                Properties.Settings.Default["key"] = oneRosterKey.Text;
                Properties.Settings.Default["secret"] = oneRosterSecret.Text;
                Properties.Settings.Default["url"] = oneRosterUrl.Text;
                Properties.Settings.Default["filePath"] = csvFile.Text;
                Properties.Settings.Default["fileTypeIndex"] = csvType.SelectedIndex;
                Properties.Settings.Default.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void LoadSettings()
        {
            oneRosterUrl.Text = Properties.Settings.Default.url;
            oneRosterKey.Text = Properties.Settings.Default.key;
            oneRosterSecret.Text = Properties.Settings.Default.secret;
            csvFile.Text = Properties.Settings.Default.filePath;
            csvType.SelectedIndex = Properties.Settings.Default.fileTypeIndex;
        }


    }
}