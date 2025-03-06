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
            tpSearchLayers = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            defaultDearchLayer = new SearchLayer();
            toolStrip1 = new ToolStrip();
            tbAddSearchLayer = new ToolStripButton();
            tbDelSearchLayer = new ToolStripButton();
            tpFinalHandle = new TabPage();
            txtAddAfter = new TextBox();
            txtAddBefore = new TextBox();
            panel3 = new Panel();
            lbReplaceList = new ListBox();
            txtAddReplace = new TextBox();
            toolStrip2 = new ToolStrip();
            tbAddReplace = new ToolStripButton();
            tbDelReplace = new ToolStripButton();
            panel2 = new Panel();
            lbAddAfter = new Label();
            lbAddBefore = new Label();
            lbReplace = new Label();
            tpOtherSetting = new TabPage();
            cbSearchList = new CheckBox();
            tabControl1.SuspendLayout();
            tpSearchLayers.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tpFinalHandle.SuspendLayout();
            panel3.SuspendLayout();
            toolStrip2.SuspendLayout();
            panel2.SuspendLayout();
            tpOtherSetting.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tpSearchLayers);
            tabControl1.Controls.Add(tpFinalHandle);
            tabControl1.Controls.Add(tpOtherSetting);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(606, 298);
            tabControl1.TabIndex = 0;
            // 
            // tpSearchLayers
            // 
            tpSearchLayers.Controls.Add(tableLayoutPanel1);
            tpSearchLayers.Controls.Add(toolStrip1);
            tpSearchLayers.Location = new Point(4, 24);
            tpSearchLayers.Name = "tpSearchLayers";
            tpSearchLayers.Padding = new Padding(3);
            tpSearchLayers.Size = new Size(598, 270);
            tpSearchLayers.TabIndex = 0;
            tpSearchLayers.Text = "Search Layers";
            tpSearchLayers.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(defaultDearchLayer, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 28);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(592, 239);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // defaultDearchLayer
            // 
            defaultDearchLayer.BorderStyle = BorderStyle.FixedSingle;
            defaultDearchLayer.Dock = DockStyle.Top;
            defaultDearchLayer.End = "";
            defaultDearchLayer.Location = new Point(3, 3);
            defaultDearchLayer.Name = "defaultDearchLayer";
            defaultDearchLayer.Padding = new Padding(0, 0, 3, 0);
            defaultDearchLayer.Size = new Size(586, 49);
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
            // tpFinalHandle
            // 
            tpFinalHandle.Controls.Add(txtAddAfter);
            tpFinalHandle.Controls.Add(txtAddBefore);
            tpFinalHandle.Controls.Add(panel3);
            tpFinalHandle.Controls.Add(panel2);
            tpFinalHandle.Location = new Point(4, 24);
            tpFinalHandle.Name = "tpFinalHandle";
            tpFinalHandle.Padding = new Padding(3);
            tpFinalHandle.Size = new Size(598, 270);
            tpFinalHandle.TabIndex = 1;
            tpFinalHandle.Text = "Final Handle";
            tpFinalHandle.UseVisualStyleBackColor = true;
            // 
            // txtAddAfter
            // 
            txtAddAfter.Dock = DockStyle.Top;
            txtAddAfter.Location = new Point(125, 206);
            txtAddAfter.Name = "txtAddAfter";
            txtAddAfter.Size = new Size(470, 23);
            txtAddAfter.TabIndex = 5;
            // 
            // txtAddBefore
            // 
            txtAddBefore.Dock = DockStyle.Top;
            txtAddBefore.Location = new Point(125, 183);
            txtAddBefore.Name = "txtAddBefore";
            txtAddBefore.Size = new Size(470, 23);
            txtAddBefore.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Controls.Add(lbReplaceList);
            panel3.Controls.Add(txtAddReplace);
            panel3.Controls.Add(toolStrip2);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(125, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(470, 180);
            panel3.TabIndex = 3;
            // 
            // lbReplaceList
            // 
            lbReplaceList.Dock = DockStyle.Top;
            lbReplaceList.FormattingEnabled = true;
            lbReplaceList.ItemHeight = 15;
            lbReplaceList.Location = new Point(0, 48);
            lbReplaceList.Name = "lbReplaceList";
            lbReplaceList.Size = new Size(470, 124);
            lbReplaceList.TabIndex = 2;
            // 
            // txtAddReplace
            // 
            txtAddReplace.Dock = DockStyle.Top;
            txtAddReplace.Location = new Point(0, 25);
            txtAddReplace.Name = "txtAddReplace";
            txtAddReplace.Size = new Size(470, 23);
            txtAddReplace.TabIndex = 4;
            // 
            // toolStrip2
            // 
            toolStrip2.Items.AddRange(new ToolStripItem[] { tbAddReplace, tbDelReplace });
            toolStrip2.Location = new Point(0, 0);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(470, 25);
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
            panel2.Controls.Add(lbAddAfter);
            panel2.Controls.Add(lbAddBefore);
            panel2.Controls.Add(lbReplace);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(122, 264);
            panel2.TabIndex = 1;
            // 
            // lbAddAfter
            // 
            lbAddAfter.AutoSize = true;
            lbAddAfter.Location = new Point(26, 203);
            lbAddAfter.Name = "lbAddAfter";
            lbAddAfter.Size = new Size(58, 15);
            lbAddAfter.TabIndex = 2;
            lbAddAfter.Text = "Add After";
            // 
            // lbAddBefore
            // 
            lbAddBefore.AutoSize = true;
            lbAddBefore.Location = new Point(26, 180);
            lbAddBefore.Name = "lbAddBefore";
            lbAddBefore.Size = new Size(66, 15);
            lbAddBefore.TabIndex = 1;
            lbAddBefore.Text = "Add Before";
            // 
            // lbReplace
            // 
            lbReplace.AutoSize = true;
            lbReplace.Location = new Point(26, 14);
            lbReplace.Name = "lbReplace";
            lbReplace.Size = new Size(48, 15);
            lbReplace.TabIndex = 0;
            lbReplace.Text = "Replace";
            // 
            // tpOtherSetting
            // 
            tpOtherSetting.Controls.Add(cbSearchList);
            tpOtherSetting.Location = new Point(4, 24);
            tpOtherSetting.Name = "tpOtherSetting";
            tpOtherSetting.Size = new Size(598, 270);
            tpOtherSetting.TabIndex = 2;
            tpOtherSetting.Text = "Other Setting";
            tpOtherSetting.UseVisualStyleBackColor = true;
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
            tpSearchLayers.ResumeLayout(false);
            tpSearchLayers.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tpFinalHandle.ResumeLayout(false);
            tpFinalHandle.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tpOtherSetting.ResumeLayout(false);
            tpOtherSetting.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tpSearchLayers;
        private TabPage tpFinalHandle;
        private ToolStrip toolStrip1;
        private ToolStripButton tbAddSearchLayer;
        private SearchLayer defaultDearchLayer;
        private ToolStripButton tbDelSearchLayer;
        private Panel panel2;
        private Label lbAddAfter;
        private Label lbAddBefore;
        private Label lbReplace;
        private ListBox lbReplaceList;
        private Panel panel3;
        private TextBox txtAddBefore;
        private TextBox txtAddReplace;
        private ToolStrip toolStrip2;
        private ToolStripButton tbAddReplace;
        private ToolStripButton tbDelReplace;
        private TextBox txtAddAfter;
        private TabPage tpOtherSetting;
        private CheckBox cbSearchList;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
