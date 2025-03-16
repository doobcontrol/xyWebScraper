namespace xy.scraper.configControl
{
    partial class AutoGrowthUrl
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
            lbAutoGrowthPar = new Label();
            txtAutoGrowthPar = new TextBox();
            txtCheckExist = new TextBox();
            lbCheckExist = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbAutoGrowthPar
            // 
            lbAutoGrowthPar.AutoSize = true;
            lbAutoGrowthPar.Dock = DockStyle.Fill;
            lbAutoGrowthPar.Location = new Point(3, 0);
            lbAutoGrowthPar.Name = "lbAutoGrowthPar";
            lbAutoGrowthPar.Size = new Size(144, 23);
            lbAutoGrowthPar.TabIndex = 0;
            lbAutoGrowthPar.Text = "label1";
            lbAutoGrowthPar.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtAutoGrowthPar
            // 
            txtAutoGrowthPar.Dock = DockStyle.Fill;
            txtAutoGrowthPar.Location = new Point(153, 3);
            txtAutoGrowthPar.Name = "txtAutoGrowthPar";
            txtAutoGrowthPar.Size = new Size(253, 23);
            txtAutoGrowthPar.TabIndex = 1;
            // 
            // txtCheckExist
            // 
            txtCheckExist.Dock = DockStyle.Fill;
            txtCheckExist.Location = new Point(153, 26);
            txtCheckExist.Name = "txtCheckExist";
            txtCheckExist.Size = new Size(253, 23);
            txtCheckExist.TabIndex = 3;
            // 
            // lbCheckExist
            // 
            lbCheckExist.AutoSize = true;
            lbCheckExist.Dock = DockStyle.Fill;
            lbCheckExist.Location = new Point(3, 23);
            lbCheckExist.Name = "lbCheckExist";
            lbCheckExist.Size = new Size(144, 25);
            lbCheckExist.TabIndex = 2;
            lbCheckExist.Text = "label2";
            lbCheckExist.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(lbAutoGrowthPar, 0, 0);
            tableLayoutPanel1.Controls.Add(txtCheckExist, 1, 1);
            tableLayoutPanel1.Controls.Add(lbCheckExist, 0, 1);
            tableLayoutPanel1.Controls.Add(txtAutoGrowthPar, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tableLayoutPanel1.Size = new Size(359, 48);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // AutoGrowthUrl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "AutoGrowthUrl";
            Size = new Size(359, 51);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lbAutoGrowthPar;
        private TextBox txtAutoGrowthPar;
        private TextBox txtCheckExist;
        private Label lbCheckExist;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
