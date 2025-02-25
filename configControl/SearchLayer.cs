using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Nodes;
using xy.scraper.page.parserConfig;
using xy.scraper.configControl.Properties;

namespace xy.scraper.configControl
{
    public partial class SearchLayer : UserControl
    {
        public SearchLayer()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;

            defaultBackColor = panel1.BackColor;

            setUiText();
        }
        private void setUiText()
        {
            lbStart.Text = Resources.lbStart;
            lbEnd.Text = Resources.lbEnd;
        }

        public string Start
        {
            get
            {
                return txtStart.Text;
            }

            set
            {
                txtStart.Text = value;
            }
        }
        public string End
        {
            get
            {
                return txtEnd.Text;
            }

            set
            {
                txtEnd.Text = value;
            }
        }

        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();
                json[JCfgName.start] = Start;
                json[JCfgName.end] = End;
                return json;
            }

            set
            {
                Start = value[JCfgName.start].GetValue<String>();
                End = value[JCfgName.end].GetValue<String>();
            }
        }

        #region Make control selectable

        private Color defaultBackColor;
        private Color selectedBackColor = Color.LightBlue;
        private void SearchLayer_Enter(object sender, EventArgs e)
        {
            panel1.BackColor = selectedBackColor;
            this.BorderStyle = BorderStyle.Fixed3D;
        }

        private void SearchLayer_Leave(object sender, EventArgs e)
        {
            panel1.BackColor = defaultBackColor;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.FindForm().ActiveControl = txtStart;
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down) return true;
            if (keyData == Keys.Left || keyData == Keys.Right) return true;
            return base.IsInputKey(keyData);
        }
        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.Focused)
            {
                var rc = this.ClientRectangle;
                rc.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(pe.Graphics, rc);
            }
        }

        #endregion
    }
}
