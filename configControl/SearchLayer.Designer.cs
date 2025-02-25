namespace xy.scraper.configControl
{
    partial class SearchLayer
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
            panel1 = new Panel();
            lbEnd = new Label();
            lbStart = new Label();
            txtStart = new TextBox();
            txtEnd = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lbEnd);
            panel1.Controls.Add(lbStart);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(81, 46);
            panel1.TabIndex = 0;
            panel1.Click += panel1_Click;
            // 
            // lbEnd
            // 
            lbEnd.AutoSize = true;
            lbEnd.Location = new Point(13, 26);
            lbEnd.Name = "lbEnd";
            lbEnd.Size = new Size(27, 15);
            lbEnd.TabIndex = 1;
            lbEnd.Text = "End";
            lbEnd.Click += panel1_Click;
            // 
            // lbStart
            // 
            lbStart.AutoSize = true;
            lbStart.Location = new Point(13, 3);
            lbStart.Name = "lbStart";
            lbStart.Size = new Size(31, 15);
            lbStart.TabIndex = 0;
            lbStart.Text = "Start";
            lbStart.Click += panel1_Click;
            // 
            // txtStart
            // 
            txtStart.Dock = DockStyle.Top;
            txtStart.Location = new Point(81, 0);
            txtStart.Name = "txtStart";
            txtStart.Size = new Size(198, 23);
            txtStart.TabIndex = 1;
            // 
            // txtEnd
            // 
            txtEnd.Dock = DockStyle.Top;
            txtEnd.Location = new Point(81, 23);
            txtEnd.Name = "txtEnd";
            txtEnd.Size = new Size(198, 23);
            txtEnd.TabIndex = 2;
            // 
            // SearchLayer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(txtEnd);
            Controls.Add(txtStart);
            Controls.Add(panel1);
            Name = "SearchLayer";
            Padding = new Padding(0, 0, 3, 0);
            Size = new Size(282, 46);
            Enter += SearchLayer_Enter;
            Leave += SearchLayer_Leave;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lbEnd;
        private Label lbStart;
        private TextBox txtStart;
        private TextBox txtEnd;
    }
}
