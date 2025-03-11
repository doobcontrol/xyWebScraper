namespace xy.scraper.configControl
{
    partial class SearchTest
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
            tabControl1 = new TabControl();
            tpTest = new TabPage();
            txtShowBox = new TextBox();
            panel3 = new Panel();
            btnSearch = new Button();
            tpHtml = new TabPage();
            txtHtml = new TextBox();
            txtUrl = new TextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            btnGetHtml = new Button();
            lbGetting = new Label();
            tabControl1.SuspendLayout();
            tpTest.SuspendLayout();
            panel3.SuspendLayout();
            tpHtml.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tpTest);
            tabControl1.Controls.Add(tpHtml);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 42);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(784, 382);
            tabControl1.TabIndex = 0;
            // 
            // tpTest
            // 
            tpTest.Controls.Add(txtShowBox);
            tpTest.Controls.Add(panel3);
            tpTest.Location = new Point(4, 24);
            tpTest.Name = "tpTest";
            tpTest.Padding = new Padding(3);
            tpTest.Size = new Size(776, 354);
            tpTest.TabIndex = 0;
            tpTest.Text = "Test";
            tpTest.UseVisualStyleBackColor = true;
            // 
            // txtShowBox
            // 
            txtShowBox.Dock = DockStyle.Fill;
            txtShowBox.Location = new Point(3, 53);
            txtShowBox.Multiline = true;
            txtShowBox.Name = "txtShowBox";
            txtShowBox.ScrollBars = ScrollBars.Vertical;
            txtShowBox.Size = new Size(770, 298);
            txtShowBox.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnSearch);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(770, 50);
            panel3.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(3, 13);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // tpHtml
            // 
            tpHtml.Controls.Add(txtHtml);
            tpHtml.Location = new Point(4, 24);
            tpHtml.Name = "tpHtml";
            tpHtml.Padding = new Padding(3);
            tpHtml.Size = new Size(776, 354);
            tpHtml.TabIndex = 1;
            tpHtml.Text = "Html Text";
            tpHtml.UseVisualStyleBackColor = true;
            // 
            // txtHtml
            // 
            txtHtml.Dock = DockStyle.Fill;
            txtHtml.Location = new Point(3, 3);
            txtHtml.Multiline = true;
            txtHtml.Name = "txtHtml";
            txtHtml.ScrollBars = ScrollBars.Vertical;
            txtHtml.Size = new Size(770, 348);
            txtHtml.TabIndex = 0;
            // 
            // txtUrl
            // 
            txtUrl.Dock = DockStyle.Fill;
            txtUrl.Location = new Point(0, 0);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(675, 23);
            txtUrl.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(txtUrl);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 27);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnGetHtml);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(675, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(109, 27);
            panel2.TabIndex = 2;
            // 
            // btnGetHtml
            // 
            btnGetHtml.Dock = DockStyle.Top;
            btnGetHtml.Location = new Point(0, 0);
            btnGetHtml.Name = "btnGetHtml";
            btnGetHtml.Size = new Size(109, 23);
            btnGetHtml.TabIndex = 0;
            btnGetHtml.Text = "Get Html Text";
            btnGetHtml.UseVisualStyleBackColor = true;
            btnGetHtml.Click += btnGetHtml_Click;
            // 
            // lbGetting
            // 
            lbGetting.AutoSize = true;
            lbGetting.Dock = DockStyle.Top;
            lbGetting.Location = new Point(0, 27);
            lbGetting.Name = "lbGetting";
            lbGetting.Size = new Size(38, 15);
            lbGetting.TabIndex = 3;
            lbGetting.Text = "label1";
            // 
            // SearchTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Controls.Add(lbGetting);
            Controls.Add(panel1);
            Name = "SearchTest";
            Size = new Size(784, 424);
            tabControl1.ResumeLayout(false);
            tpTest.ResumeLayout(false);
            tpTest.PerformLayout();
            panel3.ResumeLayout(false);
            tpHtml.ResumeLayout(false);
            tpHtml.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tpTest;
        private TabPage tpHtml;
        private TextBox txtHtml;
        private TextBox txtUrl;
        private Panel panel1;
        private Panel panel2;
        private Button btnGetHtml;
        private Panel panel3;
        private TextBox txtShowBox;
        private Button btnSearch;
        private Label lbGetting;
    }
}
