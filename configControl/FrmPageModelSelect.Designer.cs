namespace xy.scraper.configControl
{
    partial class FrmPageModelSelect
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
            panel1 = new Panel();
            panel2 = new Panel();
            btnCancel = new Button();
            btnOk = new Button();
            lbPageModelList = new ListBox();
            statusStrip1 = new StatusStrip();
            tslbSelectMsg = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 225);
            panel1.Name = "panel1";
            panel1.Size = new Size(618, 35);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnCancel);
            panel2.Controls.Add(btnOk);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(451, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(167, 35);
            panel2.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(86, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "button2";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(5, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 0;
            btnOk.Text = "button1";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // lbPageModelList
            // 
            lbPageModelList.Dock = DockStyle.Fill;
            lbPageModelList.FormattingEnabled = true;
            lbPageModelList.ItemHeight = 15;
            lbPageModelList.Location = new Point(0, 0);
            lbPageModelList.Name = "lbPageModelList";
            lbPageModelList.SelectionMode = SelectionMode.MultiSimple;
            lbPageModelList.Size = new Size(618, 225);
            lbPageModelList.TabIndex = 3;
            lbPageModelList.SelectedIndexChanged += lbPageModelList_SelectedIndexChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tslbSelectMsg });
            statusStrip1.Location = new Point(0, 260);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(618, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // tslbSelectMsg
            // 
            tslbSelectMsg.Name = "tslbSelectMsg";
            tslbSelectMsg.Size = new Size(118, 17);
            tslbSelectMsg.Text = "toolStripStatusLabel1";
            // 
            // FrmPageModelSelect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 282);
            Controls.Add(lbPageModelList);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "FrmPageModelSelect";
            Text = "FrmPageModelSelect";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Panel panel2;
        private Button btnCancel;
        private Button btnOk;
        private ListBox lbPageModelList;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tslbSelectMsg;
    }
}