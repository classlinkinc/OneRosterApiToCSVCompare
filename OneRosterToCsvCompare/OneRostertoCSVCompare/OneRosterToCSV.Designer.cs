namespace OneRostertoCSVCompare
{
    partial class OneRosterToCSV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OneRosterToCSV));
            this.label1 = new System.Windows.Forms.Label();
            this.oneRosterUrl = new System.Windows.Forms.TextBox();
            this.oneRosterKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.oneRosterSecret = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.csvFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.csvBrowse = new System.Windows.Forms.Button();
            this.csvFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.runNow = new System.Windows.Forms.Button();
            this.csvReaderWorker = new System.ComponentModel.BackgroundWorker();
            this.oneRosterWorker = new System.ComponentModel.BackgroundWorker();
            this.compareWorker = new System.ComponentModel.BackgroundWorker();
            this.csvType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.viewResults = new System.Windows.Forms.Button();
            this.compareProgressBar = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.csvStatus = new System.Windows.Forms.Label();
            this.apiStatus = new System.Windows.Forms.Label();
            this.compareStatus = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "OneRoster Url:";
            // 
            // oneRosterUrl
            // 
            this.oneRosterUrl.Location = new System.Drawing.Point(114, 10);
            this.oneRosterUrl.Name = "oneRosterUrl";
            this.oneRosterUrl.Size = new System.Drawing.Size(386, 20);
            this.oneRosterUrl.TabIndex = 1;
            // 
            // oneRosterKey
            // 
            this.oneRosterKey.Location = new System.Drawing.Point(114, 36);
            this.oneRosterKey.Name = "oneRosterKey";
            this.oneRosterKey.Size = new System.Drawing.Size(386, 20);
            this.oneRosterKey.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "OneRoster Key:";
            // 
            // oneRosterSecret
            // 
            this.oneRosterSecret.Location = new System.Drawing.Point(114, 62);
            this.oneRosterSecret.Name = "oneRosterSecret";
            this.oneRosterSecret.Size = new System.Drawing.Size(386, 20);
            this.oneRosterSecret.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "OneRoster Secret:";
            // 
            // csvFile
            // 
            this.csvFile.Location = new System.Drawing.Point(114, 88);
            this.csvFile.Name = "csvFile";
            this.csvFile.Size = new System.Drawing.Size(306, 20);
            this.csvFile.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "OneRoster CSV:";
            // 
            // csvBrowse
            // 
            this.csvBrowse.Location = new System.Drawing.Point(427, 86);
            this.csvBrowse.Name = "csvBrowse";
            this.csvBrowse.Size = new System.Drawing.Size(73, 23);
            this.csvBrowse.TabIndex = 8;
            this.csvBrowse.Text = "Browse";
            this.csvBrowse.UseVisualStyleBackColor = true;
            this.csvBrowse.Click += new System.EventHandler(this.CsvBrowse_Click);
            // 
            // runNow
            // 
            this.runNow.Location = new System.Drawing.Point(427, 261);
            this.runNow.Name = "runNow";
            this.runNow.Size = new System.Drawing.Size(75, 23);
            this.runNow.TabIndex = 9;
            this.runNow.Text = "Run";
            this.runNow.UseVisualStyleBackColor = true;
            this.runNow.Click += new System.EventHandler(this.RunNow_Click);
            // 
            // csvReaderWorker
            // 
            this.csvReaderWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Csv_DoWork);
            this.csvReaderWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_Complete);
            // 
            // oneRosterWorker
            // 
            this.oneRosterWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnerRoster_DoWork);
            this.oneRosterWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_Complete);
            // 
            // compareWorker
            // 
            this.compareWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CompareWorker_DoWork);
            this.compareWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CompareWorker_Complete);
            // 
            // csvType
            // 
            this.csvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.csvType.FormattingEnabled = true;
            this.csvType.Location = new System.Drawing.Point(114, 114);
            this.csvType.MaxDropDownItems = 5;
            this.csvType.Name = "csvType";
            this.csvType.Size = new System.Drawing.Size(121, 21);
            this.csvType.Sorted = true;
            this.csvType.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "CSV Type:";
            // 
            // viewResults
            // 
            this.viewResults.Location = new System.Drawing.Point(326, 261);
            this.viewResults.Name = "viewResults";
            this.viewResults.Size = new System.Drawing.Size(94, 23);
            this.viewResults.TabIndex = 12;
            this.viewResults.Text = "View Results";
            this.viewResults.UseVisualStyleBackColor = true;
            this.viewResults.Click += new System.EventHandler(this.ViewResults_Click);
            // 
            // compareProgressBar
            // 
            this.compareProgressBar.Location = new System.Drawing.Point(18, 230);
            this.compareProgressBar.Name = "compareProgressBar";
            this.compareProgressBar.Size = new System.Drawing.Size(484, 23);
            this.compareProgressBar.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "API Status:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "CSV Status:";
            // 
            // csvStatus
            // 
            this.csvStatus.AutoSize = true;
            this.csvStatus.Location = new System.Drawing.Point(82, 165);
            this.csvStatus.Name = "csvStatus";
            this.csvStatus.Size = new System.Drawing.Size(0, 13);
            this.csvStatus.TabIndex = 17;
            // 
            // apiStatus
            // 
            this.apiStatus.AutoSize = true;
            this.apiStatus.Location = new System.Drawing.Point(83, 143);
            this.apiStatus.Name = "apiStatus";
            this.apiStatus.Size = new System.Drawing.Size(0, 13);
            this.apiStatus.TabIndex = 16;
            // 
            // compareStatus
            // 
            this.compareStatus.AutoSize = true;
            this.compareStatus.Location = new System.Drawing.Point(106, 214);
            this.compareStatus.Name = "compareStatus";
            this.compareStatus.Size = new System.Drawing.Size(0, 13);
            this.compareStatus.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Compare Status:";
            // 
            // saveSettingsBtn
            // 
            this.saveSettingsBtn.Location = new System.Drawing.Point(416, 115);
            this.saveSettingsBtn.Name = "saveSettingsBtn";
            this.saveSettingsBtn.Size = new System.Drawing.Size(84, 23);
            this.saveSettingsBtn.TabIndex = 20;
            this.saveSettingsBtn.Text = "Save Settings";
            this.saveSettingsBtn.UseVisualStyleBackColor = true;
            this.saveSettingsBtn.Click += new System.EventHandler(this.SaveSettings);
            // 
            // OneRosterToCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 296);
            this.Controls.Add(this.saveSettingsBtn);
            this.Controls.Add(this.compareStatus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.csvStatus);
            this.Controls.Add(this.apiStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.compareProgressBar);
            this.Controls.Add(this.viewResults);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.csvType);
            this.Controls.Add(this.runNow);
            this.Controls.Add(this.csvBrowse);
            this.Controls.Add(this.csvFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.oneRosterSecret);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.oneRosterKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.oneRosterUrl);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(547, 335);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(547, 335);
            this.Name = "OneRosterToCSV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OneRoster API to CSV Compare";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox oneRosterUrl;
        private System.Windows.Forms.TextBox oneRosterKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox oneRosterSecret;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox csvFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button csvBrowse;
        private System.Windows.Forms.OpenFileDialog csvFileDialog;
        private System.Windows.Forms.Button runNow;
        private System.ComponentModel.BackgroundWorker csvReaderWorker;
        private System.ComponentModel.BackgroundWorker oneRosterWorker;
        private System.ComponentModel.BackgroundWorker compareWorker;
        private System.Windows.Forms.ComboBox csvType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button viewResults;
        private System.Windows.Forms.ProgressBar compareProgressBar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label csvStatus;
        private System.Windows.Forms.Label apiStatus;
        private System.Windows.Forms.Label compareStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button saveSettingsBtn;
    }
}

