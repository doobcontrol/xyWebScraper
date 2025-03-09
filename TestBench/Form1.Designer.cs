namespace TestBench
{
    partial class Form1
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
            textBox1 = new TextBox();
            label1 = new Label();
            textBox2 = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            panel1 = new Panel();
            panel2 = new Panel();
            button3 = new Button();
            panel3 = new Panel();
            button1 = new Button();
            button2 = new Button();
            panel5 = new Panel();
            panel7 = new Panel();
            label3 = new Label();
            panel6 = new Panel();
            cbConfigIdList = new ComboBox();
            label2 = new Label();
            tabPage2 = new TabPage();
            scraperConfig1 = new xy.scraper.configControl.ScraperConfig();
            panel4 = new Panel();
            btnSaveConfig = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            tabPage2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(335, 7);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(653, 23);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(201, 11);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // textBox2
            // 
            textBox2.Dock = DockStyle.Fill;
            textBox2.Location = new Point(3, 75);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(988, 534);
            textBox2.TabIndex = 3;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1002, 640);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(994, 612);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "scrape test";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel5);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(988, 72);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Controls.Add(button3);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 36);
            panel2.Name = "panel2";
            panel2.Size = new Size(988, 36);
            panel2.TabIndex = 3;
            // 
            // button3
            // 
            button3.Location = new Point(98, 6);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 4;
            button3.Text = "resume";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button3_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(button1);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(905, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(83, 36);
            panel3.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(3, 7);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(17, 7);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 0;
            button2.Text = "Start";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(textBox1);
            panel5.Controls.Add(panel7);
            panel5.Controls.Add(panel6);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(0, 7, 0, 0);
            panel5.Size = new Size(988, 36);
            panel5.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.Controls.Add(label3);
            panel7.Dock = DockStyle.Left;
            panel7.Location = new Point(295, 7);
            panel7.Name = "panel7";
            panel7.Size = new Size(40, 29);
            panel7.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 3);
            label3.Name = "label3";
            label3.Size = new Size(22, 15);
            label3.TabIndex = 3;
            label3.Text = "Url";
            // 
            // panel6
            // 
            panel6.Controls.Add(cbConfigIdList);
            panel6.Controls.Add(label2);
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(0, 7);
            panel6.Name = "panel6";
            panel6.Size = new Size(295, 29);
            panel6.TabIndex = 1;
            // 
            // cbConfigIdList
            // 
            cbConfigIdList.DropDownStyle = ComboBoxStyle.DropDownList;
            cbConfigIdList.FormattingEnabled = true;
            cbConfigIdList.Location = new Point(78, 0);
            cbConfigIdList.Name = "cbConfigIdList";
            cbConfigIdList.Size = new Size(214, 23);
            cbConfigIdList.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 3);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 2;
            label2.Text = "PageModel";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(scraperConfig1);
            tabPage2.Controls.Add(panel4);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(994, 612);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Page Config";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // scraperConfig1
            // 
            scraperConfig1.ConfnfigFile = "xyWebScraper.cfg";
            scraperConfig1.Dock = DockStyle.Fill;
            scraperConfig1.Location = new Point(3, 37);
            scraperConfig1.Name = "scraperConfig1";
            scraperConfig1.Size = new Size(988, 572);
            scraperConfig1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnSaveConfig);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(988, 34);
            panel4.TabIndex = 1;
            // 
            // btnSaveConfig
            // 
            btnSaveConfig.Location = new Point(5, 8);
            btnSaveConfig.Name = "btnSaveConfig";
            btnSaveConfig.Size = new Size(96, 23);
            btnSaveConfig.TabIndex = 0;
            btnSaveConfig.Text = "SaveConfig";
            btnSaveConfig.UseVisualStyleBackColor = true;
            btnSaveConfig.Click += btnSaveConfig_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1002, 640);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "xyScraper";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            tabPage2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Panel panel1;
        private TabPage tabPage2;
        private Button button2;
        private Panel panel2;
        private Panel panel3;
        private Button button1;
        private Button button3;
        private xy.scraper.configControl.ScraperConfig scraperConfig1;
        private Panel panel4;
        private Button btnSaveConfig;
        private Panel panel5;
        private Label label3;
        private Panel panel6;
        private ComboBox cbConfigIdList;
        private Label label2;
        private Panel panel7;
    }
}
