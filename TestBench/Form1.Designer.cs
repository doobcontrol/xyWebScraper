﻿namespace TestBench
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
            tabPage2 = new TabPage();
            scraperConfig1 = new xy.scraper.configControl.ScraperConfig();
            panel4 = new Panel();
            btnLoadConfig = new Button();
            btnSaveConfig = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            tabPage2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Top;
            textBox1.Location = new Point(0, 0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(786, 23);
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
            textBox2.Location = new Point(3, 61);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(786, 548);
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
            tabControl1.Size = new Size(800, 640);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 612);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "scrape test";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(textBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(786, 58);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Controls.Add(button3);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 23);
            panel2.Name = "panel2";
            panel2.Size = new Size(786, 35);
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
            panel3.Location = new Point(703, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(83, 35);
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
            // tabPage2
            // 
            tabPage2.Controls.Add(scraperConfig1);
            tabPage2.Controls.Add(panel4);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 513);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "spare";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // scraperConfig1
            // 
            scraperConfig1.Dock = DockStyle.Fill;
            scraperConfig1.Location = new Point(3, 37);
            scraperConfig1.Name = "scraperConfig1";
            scraperConfig1.Size = new Size(786, 473);
            scraperConfig1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnLoadConfig);
            panel4.Controls.Add(btnSaveConfig);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(786, 34);
            panel4.TabIndex = 1;
            // 
            // btnLoadConfig
            // 
            btnLoadConfig.Location = new Point(86, 8);
            btnLoadConfig.Name = "btnLoadConfig";
            btnLoadConfig.Size = new Size(75, 23);
            btnLoadConfig.TabIndex = 1;
            btnLoadConfig.Text = "LoadConfig";
            btnLoadConfig.UseVisualStyleBackColor = true;
            btnLoadConfig.Click += btnLoadConfig_Click;
            // 
            // btnSaveConfig
            // 
            btnSaveConfig.Location = new Point(5, 8);
            btnSaveConfig.Name = "btnSaveConfig";
            btnSaveConfig.Size = new Size(75, 23);
            btnSaveConfig.TabIndex = 0;
            btnSaveConfig.Text = "SaveConfig";
            btnSaveConfig.UseVisualStyleBackColor = true;
            btnSaveConfig.Click += btnSaveConfig_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 640);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "xyScraper";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
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
        private Button btnLoadConfig;
        private Button btnSaveConfig;
    }
}
