namespace xy.scraper.xyWebScraper
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            statusStrip1 = new StatusStrip();
            tslbMsg = new ToolStripStatusLabel();
            spbFileTask = new ToolStripProgressBar();
            panel1 = new Panel();
            panel3 = new Panel();
            txtUrl = new TextBox();
            cbConfigIdList = new ComboBox();
            pbScrapeFlag = new PictureBox();
            panel2 = new Panel();
            toolStrip1 = new ToolStrip();
            tbStart = new ToolStripButton();
            tbLog = new ToolStripButton();
            tbBreakPoint = new ToolStripButton();
            tbSetting = new ToolStripButton();
            toolTip1 = new ToolTip(components);
            dataGridView1 = new DataGridView();
            panelPageTaskInfo = new Panel();
            lbFilesCount = new Label();
            lbCurrentPage = new Label();
            lbPageTask = new Label();
            panelScrappingShow = new Panel();
            txtLog = new TextBox();
            splitter1 = new Splitter();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbScrapeFlag).BeginInit();
            panel2.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelPageTaskInfo.SuspendLayout();
            panelScrappingShow.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tslbMsg, spbFileTask });
            statusStrip1.Location = new Point(0, 573);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(914, 27);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // tslbMsg
            // 
            tslbMsg.Name = "tslbMsg";
            tslbMsg.Size = new Size(781, 21);
            tslbMsg.Spring = true;
            tslbMsg.Text = "toolStripStatusLabel1";
            tslbMsg.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // spbFileTask
            // 
            spbFileTask.Name = "spbFileTask";
            spbFileTask.RightToLeft = RightToLeft.No;
            spbFileTask.Size = new Size(114, 19);
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 44);
            panel1.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.Controls.Add(txtUrl);
            panel3.Controls.Add(cbConfigIdList);
            panel3.Controls.Add(pbScrapeFlag);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(0, 7, 0, 0);
            panel3.Size = new Size(748, 44);
            panel3.TabIndex = 1;
            // 
            // txtUrl
            // 
            txtUrl.Dock = DockStyle.Fill;
            txtUrl.Location = new Point(221, 7);
            txtUrl.Margin = new Padding(3, 4, 3, 4);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(527, 27);
            txtUrl.TabIndex = 3;
            txtUrl.TextChanged += txtUrl_TextChanged;
            // 
            // cbConfigIdList
            // 
            cbConfigIdList.Dock = DockStyle.Left;
            cbConfigIdList.DropDownStyle = ComboBoxStyle.DropDownList;
            cbConfigIdList.FormattingEnabled = true;
            cbConfigIdList.Location = new Point(24, 7);
            cbConfigIdList.Margin = new Padding(3, 4, 3, 4);
            cbConfigIdList.Name = "cbConfigIdList";
            cbConfigIdList.Size = new Size(197, 28);
            cbConfigIdList.TabIndex = 1;
            cbConfigIdList.SelectedIndexChanged += cbConfigIdList_SelectedIndexChanged;
            // 
            // pbScrapeFlag
            // 
            pbScrapeFlag.Dock = DockStyle.Left;
            pbScrapeFlag.Image = Properties.Resources.Button_Blank_Green_icon;
            pbScrapeFlag.Location = new Point(0, 7);
            pbScrapeFlag.Margin = new Padding(3, 4, 3, 4);
            pbScrapeFlag.Name = "pbScrapeFlag";
            pbScrapeFlag.Padding = new Padding(2, 5, 2, 0);
            pbScrapeFlag.Size = new Size(24, 37);
            pbScrapeFlag.TabIndex = 4;
            pbScrapeFlag.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(toolStrip1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(748, 0);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 7, 0, 0);
            panel2.Size = new Size(166, 44);
            panel2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tbStart, tbLog, tbBreakPoint, tbSetting });
            toolStrip1.Location = new Point(0, 7);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(166, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // tbStart
            // 
            tbStart.CheckOnClick = true;
            tbStart.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbStart.Image = Properties.Resources.StartWebSite;
            tbStart.ImageTransparentColor = Color.Magenta;
            tbStart.Name = "tbStart";
            tbStart.Size = new Size(29, 24);
            tbStart.Text = "toolStripButton1";
            tbStart.Click += tbStart_Click;
            // 
            // tbLog
            // 
            tbLog.CheckOnClick = true;
            tbLog.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbLog.Image = Properties.Resources.Log;
            tbLog.ImageTransparentColor = Color.Magenta;
            tbLog.Name = "tbLog";
            tbLog.Size = new Size(29, 24);
            tbLog.Text = "toolStripButton2";
            tbLog.Click += tbLog_Click;
            // 
            // tbBreakPoint
            // 
            tbBreakPoint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbBreakPoint.Image = Properties.Resources.BreakpointsWindow;
            tbBreakPoint.ImageTransparentColor = Color.Magenta;
            tbBreakPoint.Name = "tbBreakPoint";
            tbBreakPoint.Size = new Size(29, 24);
            tbBreakPoint.Text = "toolStripButton1";
            tbBreakPoint.Click += tbBreakPoint_Click;
            // 
            // tbSetting
            // 
            tbSetting.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbSetting.Image = Properties.Resources.ConfigureComputer;
            tbSetting.ImageTransparentColor = Color.Magenta;
            tbSetting.Name = "tbSetting";
            tbSetting.Size = new Size(29, 24);
            tbSetting.Text = "toolStripButton1";
            tbSetting.Click += tbSetting_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 84);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(613, 445);
            dataGridView1.TabIndex = 6;
            // 
            // panelPageTaskInfo
            // 
            panelPageTaskInfo.Controls.Add(lbFilesCount);
            panelPageTaskInfo.Controls.Add(lbCurrentPage);
            panelPageTaskInfo.Controls.Add(lbPageTask);
            panelPageTaskInfo.Dock = DockStyle.Top;
            panelPageTaskInfo.Location = new Point(0, 0);
            panelPageTaskInfo.Margin = new Padding(3, 4, 3, 4);
            panelPageTaskInfo.Name = "panelPageTaskInfo";
            panelPageTaskInfo.Size = new Size(613, 84);
            panelPageTaskInfo.TabIndex = 7;
            // 
            // lbFilesCount
            // 
            lbFilesCount.AutoSize = true;
            lbFilesCount.Location = new Point(14, 57);
            lbFilesCount.Name = "lbFilesCount";
            lbFilesCount.Size = new Size(50, 20);
            lbFilesCount.TabIndex = 2;
            lbFilesCount.Text = "label2";
            // 
            // lbCurrentPage
            // 
            lbCurrentPage.AutoSize = true;
            lbCurrentPage.Location = new Point(14, 31);
            lbCurrentPage.Name = "lbCurrentPage";
            lbCurrentPage.Size = new Size(50, 20);
            lbCurrentPage.TabIndex = 1;
            lbCurrentPage.Text = "label2";
            // 
            // lbPageTask
            // 
            lbPageTask.AutoSize = true;
            lbPageTask.Location = new Point(14, 4);
            lbPageTask.Name = "lbPageTask";
            lbPageTask.Size = new Size(50, 20);
            lbPageTask.TabIndex = 0;
            lbPageTask.Text = "label1";
            // 
            // panelScrappingShow
            // 
            panelScrappingShow.Controls.Add(dataGridView1);
            panelScrappingShow.Controls.Add(panelPageTaskInfo);
            panelScrappingShow.Dock = DockStyle.Fill;
            panelScrappingShow.Location = new Point(0, 44);
            panelScrappingShow.Margin = new Padding(3, 4, 3, 4);
            panelScrappingShow.Name = "panelScrappingShow";
            panelScrappingShow.Size = new Size(613, 529);
            panelScrappingShow.TabIndex = 8;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Right;
            txtLog.Location = new Point(616, 44);
            txtLog.Margin = new Padding(3, 4, 3, 4);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(298, 529);
            txtLog.TabIndex = 9;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(613, 44);
            splitter1.Margin = new Padding(3, 4, 3, 4);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 529);
            splitter1.TabIndex = 10;
            splitter1.TabStop = false;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(panelScrappingShow);
            Controls.Add(splitter1);
            Controls.Add(txtLog);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmMain";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbScrapeFlag).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelPageTaskInfo.ResumeLayout(false);
            panelPageTaskInfo.PerformLayout();
            panelScrappingShow.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tslbMsg;
        private Panel panel1;
        private Panel panel3;
        private TextBox txtUrl;
        private ComboBox cbConfigIdList;
        private Panel panel2;
        private ToolStrip toolStrip1;
        private ToolStripButton tbStart;
        private ToolTip toolTip1;
        private ToolStripProgressBar spbFileTask;
        private DataGridView dataGridView1;
        private Panel panelPageTaskInfo;
        private Label lbCurrentPage;
        private Label lbPageTask;
        private ToolStripButton tbLog;
        private Panel panelScrappingShow;
        private TextBox txtLog;
        private Splitter splitter1;
        private PictureBox pbScrapeFlag;
        private ToolStripButton tbSetting;
        private ToolStripButton tbBreakPoint;
        private Label lbFilesCount;
    }
}
