namespace OneRostertoCSVCompare
{
    partial class ScanResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanResult));
            this.resultListView = new System.Windows.Forms.ListView();
            this.nextPage = new System.Windows.Forms.Button();
            this.prevPage = new System.Windows.Forms.Button();
            this.page = new System.Windows.Forms.Label();
            this.resultLimit = new System.Windows.Forms.ComboBox();
            this.resultsPerPage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // resultListView
            // 
            this.resultListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.resultListView.FullRowSelect = true;
            this.resultListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.resultListView.Location = new System.Drawing.Point(12, 12);
            this.resultListView.Name = "resultListView";
            this.resultListView.Size = new System.Drawing.Size(813, 251);
            this.resultListView.TabIndex = 0;
            this.resultListView.UseCompatibleStateImageBehavior = false;
            // 
            // nextPage
            // 
            this.nextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextPage.Location = new System.Drawing.Point(750, 268);
            this.nextPage.Name = "nextPage";
            this.nextPage.Size = new System.Drawing.Size(75, 23);
            this.nextPage.TabIndex = 1;
            this.nextPage.Text = "Next";
            this.nextPage.UseVisualStyleBackColor = true;
            this.nextPage.Click += new System.EventHandler(this.nextPage_Click);
            // 
            // prevPage
            // 
            this.prevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prevPage.Location = new System.Drawing.Point(669, 268);
            this.prevPage.Name = "prevPage";
            this.prevPage.Size = new System.Drawing.Size(75, 23);
            this.prevPage.TabIndex = 2;
            this.prevPage.Text = "Previous";
            this.prevPage.UseVisualStyleBackColor = true;
            this.prevPage.Click += new System.EventHandler(this.prevPage_Click);
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.page.AutoSize = true;
            this.page.Location = new System.Drawing.Point(13, 277);
            this.page.Name = "page";
            this.page.Size = new System.Drawing.Size(31, 13);
            this.page.TabIndex = 3;
            this.page.Text = "page";
            this.page.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resultLimit
            // 
            this.resultLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resultLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resultLimit.FormattingEnabled = true;
            this.resultLimit.Location = new System.Drawing.Point(525, 270);
            this.resultLimit.Name = "resultLimit";
            this.resultLimit.Size = new System.Drawing.Size(138, 21);
            this.resultLimit.TabIndex = 4;
            this.resultLimit.SelectedIndexChanged += new System.EventHandler(this.limit_Change);
            // 
            // resultsPerPage
            // 
            this.resultsPerPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsPerPage.AutoSize = true;
            this.resultsPerPage.Location = new System.Drawing.Point(430, 273);
            this.resultsPerPage.Name = "resultsPerPage";
            this.resultsPerPage.Size = new System.Drawing.Size(89, 13);
            this.resultsPerPage.TabIndex = 5;
            this.resultsPerPage.Text = "Results Per Page";
            this.resultsPerPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScanResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 298);
            this.Controls.Add(this.resultsPerPage);
            this.Controls.Add(this.resultLimit);
            this.Controls.Add(this.page);
            this.Controls.Add(this.prevPage);
            this.Controls.Add(this.nextPage);
            this.Controls.Add(this.resultListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(853, 337);
            this.Name = "ScanResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ScanResult";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView resultListView;
        private System.Windows.Forms.Button nextPage;
        private System.Windows.Forms.Button prevPage;
        private System.Windows.Forms.Label page;
        private System.Windows.Forms.ComboBox resultLimit;
        private System.Windows.Forms.Label resultsPerPage;
    }
}