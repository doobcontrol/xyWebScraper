using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json.Nodes;

namespace xy.scraper.configControl
{
    public partial class ScraperConfig : UserControl
    {
        public ScraperConfig()
        {
            InitializeComponent();

            tabControl1.DrawItem 
                += new DrawItemEventHandler(tabControl_DrawVerticalItem);

            defaultPageConfig.PageID = "pageModel1";
            tabControl1.TabPages[0].Text = defaultPageConfig.PageID;
        }

        private void tbAddPageConfig_Click(object sender, EventArgs e)
        {
            PageConfig pc = new PageConfig();
            pc.Dock = DockStyle.Fill;
            pc.PageID = "pageModel" + (tabControl1.TabPages.Count + 1);
            TabPage tp = new TabPage();
            tp.Text = pc.PageID;
            tp.Controls.Add(pc);
            tabControl1.TabPages.Add(tp);
        }

        private void tbDelPageConfig_Click(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex !=0 )
            {
                int index = tabControl1.SelectedIndex;
                TabPage tabPage = tabControl1.TabPages[index];
                tabControl1.TabPages.Remove(tabPage);
                tabPage.Dispose();
                tabControl1.SelectedIndex = index - 1;
            }
        }

        //make tabControl vertical(tab left)
        private void tabControl_DrawVerticalItem(
            Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;
            TabControl tabControl = (TabControl)sender;

            // Get the item from the collection.
            TabPage _tabPage = tabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.White);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", 10.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        public JsonArray JsonObj
        {
            get
            {
                JsonArray json = new JsonArray();
                foreach (TabPage tp in tabControl1.Controls)
                {
                    PageConfig sc = (PageConfig)tp.Controls[0];
                    json.Add(sc.JsonObj);
                }

                return json;
            }

            set
            {
                foreach (JsonObject item in value)
                {
                    PageConfig pc;
                    TabControl tc = tabControl1;
                    TabPage tp;
                    if (value.IndexOf(item) == 0)
                    {
                        tp = ((TabPage)tc.Controls[0]);
                        pc = ((PageConfig)tp.Controls[0]);
                    }
                    else
                    {
                        pc = new PageConfig();
                        pc.Dock = DockStyle.Fill;
                        tp = new TabPage();
                        tc.Controls.Add(tp);
                        tp.Controls.Add(pc);
                    }
                    pc.JsonObj = item;
                    tp.Text = pc.PageID;
                }
            }
        }
    }
}
