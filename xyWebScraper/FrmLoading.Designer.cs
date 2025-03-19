namespace xy.scraper.xyWebScraper
{
    partial class FrmLoading
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbMsg = new Label();
            SuspendLayout();
            // 
            // lbMsg
            // 
            lbMsg.Dock = DockStyle.Fill;
            lbMsg.Location = new Point(0, 0);
            lbMsg.Name = "lbMsg";
            lbMsg.Size = new Size(441, 38);
            lbMsg.TabIndex = 0;
            lbMsg.Text = "label1";
            lbMsg.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmLoading
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(441, 38);
            Controls.Add(lbMsg);
            Name = "FrmLoading";
            Text = "FrmLoading";
            ResumeLayout(false);
        }

        #endregion

        private Label lbMsg;
    }
}