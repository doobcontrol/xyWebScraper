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
            label2 = new Label();
            label1 = new Label();
            txtStart = new TextBox();
            txtEnd = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(59, 46);
            panel1.TabIndex = 0;
            panel1.Click += panel1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 26);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 1;
            label2.Text = "End";
            label2.Click += panel1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 3);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 0;
            label1.Text = "Start";
            label1.Click += panel1_Click;
            // 
            // txtStart
            // 
            txtStart.Dock = DockStyle.Top;
            txtStart.Location = new Point(59, 0);
            txtStart.Name = "txtStart";
            txtStart.Size = new Size(220, 23);
            txtStart.TabIndex = 1;
            // 
            // txtEnd
            // 
            txtEnd.Dock = DockStyle.Top;
            txtEnd.Location = new Point(59, 23);
            txtEnd.Name = "txtEnd";
            txtEnd.Size = new Size(220, 23);
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
        private Label label2;
        private Label label1;
        private TextBox txtStart;
        private TextBox txtEnd;
    }
}
