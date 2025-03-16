namespace xy.scraper.configControl
{
    partial class PageConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageConfig));
            panel1 = new Panel();
            txtCoding = new TextBox();
            lbEncoding = new Label();
            txtPageID = new TextBox();
            lbPageConfigID = new Label();
            panel2 = new Panel();
            nextsCb = new CheckBox();
            filesCb = new CheckBox();
            pathsCb = new CheckBox();
            tabControl1 = new TabControl();
            pathsTp = new TabPage();
            tcPath = new TabControl();
            tcPathdefaultPage = new TabPage();
            searchConfig1 = new SearchConfig();
            toolStrip1 = new ToolStrip();
            tbAddPath = new ToolStripButton();
            tbDelPath = new ToolStripButton();
            filesTp = new TabPage();
            tcFile = new TabControl();
            tabPage1 = new TabPage();
            searchConfig2 = new SearchConfig();
            toolStrip2 = new ToolStrip();
            tbAddFile = new ToolStripButton();
            tbDelFile = new ToolStripButton();
            nextsTp = new TabPage();
            tcNext = new TabControl();
            tabPage3 = new TabPage();
            searchConfig3 = new SearchConfig();
            panel3 = new Panel();
            cbIsAutoUrl = new CheckBox();
            lbNextPageConfigID = new Label();
            txtNextUrlPageID = new TextBox();
            toolStrip3 = new ToolStrip();
            tbAddNext = new ToolStripButton();
            tbDelNext = new ToolStripButton();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            pathsTp.SuspendLayout();
            tcPath.SuspendLayout();
            tcPathdefaultPage.SuspendLayout();
            toolStrip1.SuspendLayout();
            filesTp.SuspendLayout();
            tcFile.SuspendLayout();
            tabPage1.SuspendLayout();
            toolStrip2.SuspendLayout();
            nextsTp.SuspendLayout();
            tcNext.SuspendLayout();
            tabPage3.SuspendLayout();
            panel3.SuspendLayout();
            toolStrip3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(txtCoding);
            panel1.Controls.Add(lbEncoding);
            panel1.Controls.Add(txtPageID);
            panel1.Controls.Add(lbPageConfigID);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(656, 109);
            panel1.TabIndex = 0;
            // 
            // txtCoding
            // 
            txtCoding.Location = new Point(102, 44);
            txtCoding.Name = "txtCoding";
            txtCoding.Size = new Size(244, 23);
            txtCoding.TabIndex = 4;
            txtCoding.Text = "UTF-8";
            // 
            // lbEncoding
            // 
            lbEncoding.AutoSize = true;
            lbEncoding.Location = new Point(13, 47);
            lbEncoding.Name = "lbEncoding";
            lbEncoding.Size = new Size(57, 15);
            lbEncoding.TabIndex = 3;
            lbEncoding.Text = "Encoding";
            // 
            // txtPageID
            // 
            txtPageID.Location = new Point(102, 12);
            txtPageID.Name = "txtPageID";
            txtPageID.Size = new Size(244, 23);
            txtPageID.TabIndex = 2;
            txtPageID.TextChanged += txtPageID_TextChanged;
            // 
            // lbPageConfigID
            // 
            lbPageConfigID.AutoSize = true;
            lbPageConfigID.Location = new Point(13, 15);
            lbPageConfigID.Name = "lbPageConfigID";
            lbPageConfigID.Size = new Size(83, 15);
            lbPageConfigID.TabIndex = 1;
            lbPageConfigID.Text = "PageConfig ID";
            // 
            // panel2
            // 
            panel2.Controls.Add(nextsCb);
            panel2.Controls.Add(filesCb);
            panel2.Controls.Add(pathsCb);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 73);
            panel2.Name = "panel2";
            panel2.Size = new Size(656, 36);
            panel2.TabIndex = 0;
            // 
            // nextsCb
            // 
            nextsCb.AutoSize = true;
            nextsCb.Location = new Point(193, 14);
            nextsCb.Name = "nextsCb";
            nextsCb.Size = new Size(53, 19);
            nextsCb.TabIndex = 2;
            nextsCb.Text = "nexts";
            nextsCb.UseVisualStyleBackColor = true;
            // 
            // filesCb
            // 
            filesCb.AutoSize = true;
            filesCb.Location = new Point(108, 14);
            filesCb.Name = "filesCb";
            filesCb.Size = new Size(47, 19);
            filesCb.TabIndex = 1;
            filesCb.Text = "files";
            filesCb.UseVisualStyleBackColor = true;
            // 
            // pathsCb
            // 
            pathsCb.AutoSize = true;
            pathsCb.Location = new Point(15, 14);
            pathsCb.Name = "pathsCb";
            pathsCb.Size = new Size(55, 19);
            pathsCb.TabIndex = 0;
            pathsCb.Text = "paths";
            pathsCb.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(pathsTp);
            tabControl1.Controls.Add(filesTp);
            tabControl1.Controls.Add(nextsTp);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 109);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(656, 298);
            tabControl1.TabIndex = 1;
            // 
            // pathsTp
            // 
            pathsTp.Controls.Add(tcPath);
            pathsTp.Controls.Add(toolStrip1);
            pathsTp.Location = new Point(4, 24);
            pathsTp.Name = "pathsTp";
            pathsTp.Padding = new Padding(3);
            pathsTp.Size = new Size(648, 270);
            pathsTp.TabIndex = 0;
            pathsTp.Text = "paths";
            pathsTp.UseVisualStyleBackColor = true;
            // 
            // tcPath
            // 
            tcPath.Controls.Add(tcPathdefaultPage);
            tcPath.Dock = DockStyle.Fill;
            tcPath.Location = new Point(3, 28);
            tcPath.Name = "tcPath";
            tcPath.SelectedIndex = 0;
            tcPath.Size = new Size(642, 239);
            tcPath.TabIndex = 3;
            // 
            // tcPathdefaultPage
            // 
            tcPathdefaultPage.Controls.Add(searchConfig1);
            tcPathdefaultPage.Location = new Point(4, 24);
            tcPathdefaultPage.Name = "tcPathdefaultPage";
            tcPathdefaultPage.Padding = new Padding(3);
            tcPathdefaultPage.Size = new Size(634, 211);
            tcPathdefaultPage.TabIndex = 0;
            tcPathdefaultPage.Text = "path";
            tcPathdefaultPage.UseVisualStyleBackColor = true;
            // 
            // searchConfig1
            // 
            searchConfig1.Dock = DockStyle.Fill;
            searchConfig1.Location = new Point(3, 3);
            searchConfig1.Name = "searchConfig1";
            searchConfig1.Size = new Size(628, 205);
            searchConfig1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { tbAddPath, tbDelPath });
            toolStrip1.Location = new Point(3, 3);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(642, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // tbAddPath
            // 
            tbAddPath.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbAddPath.Image = (Image)resources.GetObject("tbAddPath.Image");
            tbAddPath.ImageTransparentColor = Color.Magenta;
            tbAddPath.Name = "tbAddPath";
            tbAddPath.Size = new Size(23, 22);
            tbAddPath.Text = "toolStripButton1";
            // 
            // tbDelPath
            // 
            tbDelPath.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbDelPath.Image = (Image)resources.GetObject("tbDelPath.Image");
            tbDelPath.ImageTransparentColor = Color.Magenta;
            tbDelPath.Name = "tbDelPath";
            tbDelPath.Size = new Size(23, 22);
            tbDelPath.Text = "toolStripButton1";
            // 
            // filesTp
            // 
            filesTp.Controls.Add(tcFile);
            filesTp.Controls.Add(toolStrip2);
            filesTp.Location = new Point(4, 24);
            filesTp.Name = "filesTp";
            filesTp.Padding = new Padding(3);
            filesTp.Size = new Size(648, 270);
            filesTp.TabIndex = 1;
            filesTp.Text = "files";
            filesTp.UseVisualStyleBackColor = true;
            // 
            // tcFile
            // 
            tcFile.Controls.Add(tabPage1);
            tcFile.Dock = DockStyle.Fill;
            tcFile.Location = new Point(3, 28);
            tcFile.Name = "tcFile";
            tcFile.SelectedIndex = 0;
            tcFile.Size = new Size(642, 239);
            tcFile.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(searchConfig2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(634, 211);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "file";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // searchConfig2
            // 
            searchConfig2.Dock = DockStyle.Fill;
            searchConfig2.Location = new Point(3, 3);
            searchConfig2.Name = "searchConfig2";
            searchConfig2.Size = new Size(628, 205);
            searchConfig2.TabIndex = 4;
            // 
            // toolStrip2
            // 
            toolStrip2.Items.AddRange(new ToolStripItem[] { tbAddFile, tbDelFile });
            toolStrip2.Location = new Point(3, 3);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(642, 25);
            toolStrip2.TabIndex = 3;
            toolStrip2.Text = "toolStrip2";
            // 
            // tbAddFile
            // 
            tbAddFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbAddFile.Image = (Image)resources.GetObject("tbAddFile.Image");
            tbAddFile.ImageTransparentColor = Color.Magenta;
            tbAddFile.Name = "tbAddFile";
            tbAddFile.Size = new Size(23, 22);
            tbAddFile.Text = "toolStripButton1";
            // 
            // tbDelFile
            // 
            tbDelFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbDelFile.Image = (Image)resources.GetObject("tbDelFile.Image");
            tbDelFile.ImageTransparentColor = Color.Magenta;
            tbDelFile.Name = "tbDelFile";
            tbDelFile.Size = new Size(23, 22);
            tbDelFile.Text = "toolStripButton1";
            // 
            // nextsTp
            // 
            nextsTp.Controls.Add(tcNext);
            nextsTp.Controls.Add(toolStrip3);
            nextsTp.Location = new Point(4, 24);
            nextsTp.Name = "nextsTp";
            nextsTp.Size = new Size(648, 270);
            nextsTp.TabIndex = 2;
            nextsTp.Text = "nexts";
            nextsTp.UseVisualStyleBackColor = true;
            // 
            // tcNext
            // 
            tcNext.Controls.Add(tabPage3);
            tcNext.Dock = DockStyle.Fill;
            tcNext.Location = new Point(0, 25);
            tcNext.Name = "tcNext";
            tcNext.SelectedIndex = 0;
            tcNext.Size = new Size(648, 245);
            tcNext.TabIndex = 6;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(searchConfig3);
            tabPage3.Controls.Add(panel3);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(640, 217);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "Next";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // searchConfig3
            // 
            searchConfig3.Dock = DockStyle.Fill;
            searchConfig3.Location = new Point(3, 45);
            searchConfig3.Name = "searchConfig3";
            searchConfig3.Size = new Size(634, 169);
            searchConfig3.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Controls.Add(lbNextPageConfigID);
            panel3.Controls.Add(txtNextUrlPageID);
            panel3.Controls.Add(cbIsAutoUrl);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(634, 42);
            panel3.TabIndex = 6;
            // 
            // cbIsAutoUrl
            // 
            cbIsAutoUrl.AutoSize = true;
            cbIsAutoUrl.Location = new Point(348, 17);
            cbIsAutoUrl.Name = "cbIsAutoUrl";
            cbIsAutoUrl.Size = new Size(82, 19);
            cbIsAutoUrl.TabIndex = 3;
            cbIsAutoUrl.Text = "checkBox1";
            cbIsAutoUrl.UseVisualStyleBackColor = true;
            // 
            // lbNextPageConfigID
            // 
            lbNextPageConfigID.AutoSize = true;
            lbNextPageConfigID.Location = new Point(9, 15);
            lbNextPageConfigID.Name = "lbNextPageConfigID";
            lbNextPageConfigID.Size = new Size(83, 15);
            lbNextPageConfigID.TabIndex = 0;
            lbNextPageConfigID.Text = "PageConfig ID";
            // 
            // txtNextUrlPageID
            // 
            txtNextUrlPageID.Location = new Point(98, 12);
            txtNextUrlPageID.Name = "txtNextUrlPageID";
            txtNextUrlPageID.Size = new Size(244, 23);
            txtNextUrlPageID.TabIndex = 2;
            // 
            // toolStrip3
            // 
            toolStrip3.Items.AddRange(new ToolStripItem[] { tbAddNext, tbDelNext });
            toolStrip3.Location = new Point(0, 0);
            toolStrip3.Name = "toolStrip3";
            toolStrip3.Size = new Size(648, 25);
            toolStrip3.TabIndex = 3;
            toolStrip3.Text = "toolStrip3";
            // 
            // tbAddNext
            // 
            tbAddNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbAddNext.Image = (Image)resources.GetObject("tbAddNext.Image");
            tbAddNext.ImageTransparentColor = Color.Magenta;
            tbAddNext.Name = "tbAddNext";
            tbAddNext.Size = new Size(23, 22);
            tbAddNext.Text = "toolStripButton1";
            // 
            // tbDelNext
            // 
            tbDelNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbDelNext.Image = (Image)resources.GetObject("tbDelNext.Image");
            tbDelNext.ImageTransparentColor = Color.Magenta;
            tbDelNext.Name = "tbDelNext";
            tbDelNext.Size = new Size(23, 22);
            tbDelNext.Text = "toolStripButton1";
            // 
            // PageConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            Name = "PageConfig";
            Size = new Size(656, 407);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabControl1.ResumeLayout(false);
            pathsTp.ResumeLayout(false);
            pathsTp.PerformLayout();
            tcPath.ResumeLayout(false);
            tcPathdefaultPage.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            filesTp.ResumeLayout(false);
            filesTp.PerformLayout();
            tcFile.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            nextsTp.ResumeLayout(false);
            nextsTp.PerformLayout();
            tcNext.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            toolStrip3.ResumeLayout(false);
            toolStrip3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TabControl tabControl1;
        private TabPage pathsTp;
        private TabPage filesTp;
        private TabPage nextsTp;
        private CheckBox pathsCb;
        private CheckBox nextsCb;
        private CheckBox filesCb;
        private TextBox txtPageID;
        private Label lbPageConfigID;
        private TextBox txtCoding;
        private Label lbEncoding;
        private SearchConfig searchConfig1;
        private ToolStrip toolStrip1;
        private ToolStripButton tbAddPath;
        private ToolStripButton tbDelPath;
        private TabControl tcPath;
        private TabPage tcPathdefaultPage;
        private ToolStrip toolStrip2;
        private ToolStripButton tbAddFile;
        private ToolStripButton tbDelFile;
        private ToolStrip toolStrip3;
        private ToolStripButton tbAddNext;
        private ToolStripButton tbDelNext;
        private SearchConfig searchConfig2;
        private SearchConfig searchConfig3;
        private TabControl tcFile;
        private TabPage tabPage1;
        private TabControl tcNext;
        private TabPage tabPage3;
        private Panel panel3;
        private Label lbNextPageConfigID;
        private TextBox txtNextUrlPageID;
        private CheckBox cbIsAutoUrl;
    }
}
