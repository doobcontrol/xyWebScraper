namespace xy.scraper.configControl
{
    partial class SearchConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchConfig));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            panel1 = new Panel();
            defaultDearchLayer = new SearchLayer();
            toolStrip1 = new ToolStrip();
            tbAddSearchLayer = new ToolStripButton();
            tbDelSearchLayer = new ToolStripButton();
            tabPage2 = new TabPage();
            txtAddAfter = new TextBox();
            txtAddBefore = new TextBox();
            panel3 = new Panel();
            lbReplaceList = new ListBox();
            txtAddReplace = new TextBox();
            toolStrip2 = new ToolStrip();
            tbAddReplace = new ToolStripButton();
            tbDelReplace = new ToolStripButton();
            panel2 = new Panel();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            tabPage3 = new TabPage();
            cbSearchList = new CheckBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tabPage2.SuspendLayout();
            panel3.SuspendLayout();
            toolStrip2.SuspendLayout();
            panel2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(606, 298);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(toolStrip1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(598, 270);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Search Layers";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(defaultDearchLayer);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(592, 239);
            panel1.TabIndex = 2;
            // 
            // defaultDearchLayer
            // 
            defaultDearchLayer.BorderStyle = BorderStyle.FixedSingle;
            defaultDearchLayer.Dock = DockStyle.Top;
            defaultDearchLayer.End = "";
            defaultDearchLayer.Location = new Point(0, 0);
            defaultDearchLayer.Name = "defaultDearchLayer";
            defaultDearchLayer.Padding = new Padding(0, 0, 3, 0);
            defaultDearchLayer.Size = new Size(592, 48);
            defaultDearchLayer.Start = "";
            defaultDearchLayer.TabIndex = 1;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { tbAddSearchLayer, tbDelSearchLayer });
            toolStrip1.Location = new Point(3, 3);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(592, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // tbAddSearchLayer
            // 
            tbAddSearchLayer.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbAddSearchLayer.Image = (Image)resources.GetObject("tbAddSearchLayer.Image");
            tbAddSearchLayer.ImageTransparentColor = Color.Magenta;
            tbAddSearchLayer.Name = "tbAddSearchLayer";
            tbAddSearchLayer.Size = new Size(23, 22);
            tbAddSearchLayer.Text = "toolStripButton1";
            tbAddSearchLayer.Click += tbAddSearchLayer_Click;
            // 
            // tbDelSearchLayer
            // 
            tbDelSearchLayer.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbDelSearchLayer.Image = (Image)resources.GetObject("tbDelSearchLayer.Image");
            tbDelSearchLayer.ImageTransparentColor = Color.Magenta;
            tbDelSearchLayer.Name = "tbDelSearchLayer";
            tbDelSearchLayer.Size = new Size(23, 22);
            tbDelSearchLayer.Text = "toolStripButton1";
            tbDelSearchLayer.Click += tbDelSearchLayer_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(txtAddAfter);
            tabPage2.Controls.Add(txtAddBefore);
            tabPage2.Controls.Add(panel3);
            tabPage2.Controls.Add(panel2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(598, 270);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Final Handle";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtAddAfter
            // 
            txtAddAfter.Dock = DockStyle.Top;
            txtAddAfter.Location = new Point(105, 206);
            txtAddAfter.Name = "txtAddAfter";
            txtAddAfter.Size = new Size(490, 23);
            txtAddAfter.TabIndex = 5;
            // 
            // txtAddBefore
            // 
            txtAddBefore.Dock = DockStyle.Top;
            txtAddBefore.Location = new Point(105, 183);
            txtAddBefore.Name = "txtAddBefore";
            txtAddBefore.Size = new Size(490, 23);
            txtAddBefore.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Controls.Add(lbReplaceList);
            panel3.Controls.Add(txtAddReplace);
            panel3.Controls.Add(toolStrip2);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(105, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(490, 180);
            panel3.TabIndex = 3;
            // 
            // lbReplaceList
            // 
            lbReplaceList.Dock = DockStyle.Top;
            lbReplaceList.FormattingEnabled = true;
            lbReplaceList.ItemHeight = 15;
            lbReplaceList.Location = new Point(0, 48);
            lbReplaceList.Name = "lbReplaceList";
            lbReplaceList.Size = new Size(490, 124);
            lbReplaceList.TabIndex = 2;
            // 
            // txtAddReplace
            // 
            txtAddReplace.Dock = DockStyle.Top;
            txtAddReplace.Location = new Point(0, 25);
            txtAddReplace.Name = "txtAddReplace";
            txtAddReplace.Size = new Size(490, 23);
            txtAddReplace.TabIndex = 4;
            // 
            // toolStrip2
            // 
            toolStrip2.Items.AddRange(new ToolStripItem[] { tbAddReplace, tbDelReplace });
            toolStrip2.Location = new Point(0, 0);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(490, 25);
            toolStrip2.TabIndex = 3;
            toolStrip2.Text = "toolStrip2";
            // 
            // tbAddReplace
            // 
            tbAddReplace.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbAddReplace.Image = (Image)resources.GetObject("tbAddReplace.Image");
            tbAddReplace.ImageTransparentColor = Color.Magenta;
            tbAddReplace.Name = "tbAddReplace";
            tbAddReplace.Size = new Size(23, 22);
            tbAddReplace.Text = "toolStripButton1";
            tbAddReplace.Click += tbAddReplace_Click;
            // 
            // tbDelReplace
            // 
            tbDelReplace.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbDelReplace.Image = (Image)resources.GetObject("tbDelReplace.Image");
            tbDelReplace.ImageTransparentColor = Color.Magenta;
            tbDelReplace.Name = "tbDelReplace";
            tbDelReplace.Size = new Size(23, 22);
            tbDelReplace.Text = "toolStripButton1";
            tbDelReplace.Click += tbDelReplace_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(102, 264);
            panel2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 203);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 2;
            label3.Text = "Add After";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 180);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 1;
            label2.Text = "Add Before";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 14);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 0;
            label1.Text = "Replace";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(cbSearchList);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(598, 270);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Other Setting";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbSearchList
            // 
            cbSearchList.AutoSize = true;
            cbSearchList.Location = new Point(10, 12);
            cbSearchList.Name = "cbSearchList";
            cbSearchList.Size = new Size(82, 19);
            cbSearchList.TabIndex = 0;
            cbSearchList.Text = "Search List";
            cbSearchList.UseVisualStyleBackColor = true;
            // 
            // SearchConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Name = "SearchConfig";
            Size = new Size(606, 298);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ToolStrip toolStrip1;
        private ToolStripButton tbAddSearchLayer;
        private SearchLayer defaultDearchLayer;
        private ToolStripButton tbDelSearchLayer;
        private Panel panel1;
        private Panel panel2;
        private Label label3;
        private Label label2;
        private Label label1;
        private ListBox lbReplaceList;
        private Panel panel3;
        private TextBox txtAddBefore;
        private TextBox txtAddReplace;
        private ToolStrip toolStrip2;
        private ToolStripButton tbAddReplace;
        private ToolStripButton tbDelReplace;
        private TextBox txtAddAfter;
        private TabPage tabPage3;
        private CheckBox cbSearchList;
    }
}
