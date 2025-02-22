namespace xy.scraper.configControl
{
    partial class ScraperConfig
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScraperConfig));
            defaultPageConfig = new PageConfig();
            toolStrip1 = new ToolStrip();
            tbAddPageConfig = new ToolStripButton();
            tbDelPageConfig = new ToolStripButton();
            tbCopyPageConfig = new ToolStripButton();
            tbShowTest = new ToolStripButton();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            searchTest1 = new SearchTest();
            splitter1 = new Splitter();
            toolStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // defaultPageConfig
            // 
            defaultPageConfig.Dock = DockStyle.Fill;
            defaultPageConfig.Location = new Point(3, 3);
            defaultPageConfig.Name = "defaultPageConfig";
            defaultPageConfig.PageID = "";
            defaultPageConfig.Size = new Size(529, 253);
            defaultPageConfig.TabIndex = 2;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { tbAddPageConfig, tbDelPageConfig, tbCopyPageConfig, tbShowTest });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(693, 25);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // tbAddPageConfig
            // 
            tbAddPageConfig.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbAddPageConfig.Image = (Image)resources.GetObject("tbAddPageConfig.Image");
            tbAddPageConfig.ImageTransparentColor = Color.Magenta;
            tbAddPageConfig.Name = "tbAddPageConfig";
            tbAddPageConfig.Size = new Size(23, 22);
            tbAddPageConfig.Text = "toolStripButton1";
            tbAddPageConfig.Click += tbAddPageConfig_Click;
            // 
            // tbDelPageConfig
            // 
            tbDelPageConfig.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbDelPageConfig.Image = (Image)resources.GetObject("tbDelPageConfig.Image");
            tbDelPageConfig.ImageTransparentColor = Color.Magenta;
            tbDelPageConfig.Name = "tbDelPageConfig";
            tbDelPageConfig.Size = new Size(23, 22);
            tbDelPageConfig.Text = "toolStripButton1";
            tbDelPageConfig.Click += tbDelPageConfig_Click;
            // 
            // tbCopyPageConfig
            // 
            tbCopyPageConfig.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbCopyPageConfig.Image = (Image)resources.GetObject("tbCopyPageConfig.Image");
            tbCopyPageConfig.ImageTransparentColor = Color.Magenta;
            tbCopyPageConfig.Name = "tbCopyPageConfig";
            tbCopyPageConfig.Size = new Size(23, 22);
            tbCopyPageConfig.Text = "toolStripButton1";
            tbCopyPageConfig.Click += tbCopyPageConfig_Click;
            // 
            // tbShowTest
            // 
            tbShowTest.CheckOnClick = true;
            tbShowTest.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbShowTest.Image = (Image)resources.GetObject("tbShowTest.Image");
            tbShowTest.ImageTransparentColor = Color.Magenta;
            tbShowTest.Name = "tbShowTest";
            tbShowTest.Size = new Size(23, 22);
            tbShowTest.Text = "toolStripButton1";
            tbShowTest.Click += tbShowTest_Click;
            // 
            // tabControl1
            // 
            tabControl1.Alignment = TabAlignment.Left;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.ItemSize = new Size(23, 150);
            tabControl1.Location = new Point(0, 25);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(693, 267);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(defaultPageConfig);
            tabPage1.Location = new Point(154, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(535, 259);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // searchTest1
            // 
            searchTest1.Dock = DockStyle.Bottom;
            searchTest1.Location = new Point(0, 295);
            searchTest1.Name = "searchTest1";
            searchTest1.Size = new Size(693, 152);
            searchTest1.TabIndex = 5;
            searchTest1.Visible = false;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Bottom;
            splitter1.Location = new Point(0, 292);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(693, 3);
            splitter1.TabIndex = 6;
            splitter1.TabStop = false;
            // 
            // ScraperConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Controls.Add(splitter1);
            Controls.Add(searchTest1);
            Controls.Add(toolStrip1);
            Name = "ScraperConfig";
            Size = new Size(693, 447);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PageConfig defaultPageConfig;
        private ToolStrip toolStrip1;
        private ToolStripButton tbAddPageConfig;
        private ToolStripButton tbDelPageConfig;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private ToolStripButton tbCopyPageConfig;
        private SearchTest searchTest1;
        private ToolStripButton tbShowTest;
        private Splitter splitter1;
    }
}
