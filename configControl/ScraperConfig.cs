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
using System.Globalization;
using xy.scraper.configControl.Properties;
using xy.scraper.page;
using System.Text.Json;
using System.Diagnostics;
using System.Security;
using System.Reflection;

namespace xy.scraper.configControl
{
    public partial class ScraperConfig : UserControl
    {
        public ScraperConfig()
        {
            InitializeComponent();

            tabControl1.DrawItem
                += new DrawItemEventHandler(tabControl_DrawVerticalItem);
            tabControl1.ShowToolTips = true;
            defaultPageConfig.PageID = "pageModel1";
            tabControl1.TabPages[0].Text = defaultPageConfig.PageID;

            searchTest1.SearchJsonObj =
                new SearchTest.SearchJsonObject(getCurrentSearchJsonObject);
            searchTest1.GetHtmlStringHandler =
                new SearchTest.GetHtmlString(GetHtmlStringObj);

            defaultPageConfig.PageIDChanged += defaultPageConfig_PageIDChanged;

            setUiText();
        }
        private void setUiText()
        {
            tbAddPageConfig.ToolTipText = Resources.tbAddPageConfig;
            tbCopyPageConfig.ToolTipText = Resources.tbCopyPageConfig;
            tbDelPageConfig.ToolTipText = Resources.tbDelPageConfig;
            tbShowTest.ToolTipText = Resources.tbShowTest;
            tbSave.ToolTipText = Resources.tbSave;
            tbSaveAs.ToolTipText = Resources.tbSaveAs;
            tbImport.ToolTipText = Resources.tbImport;
        }

        private void defaultPageConfig_PageIDChanged(object? sender, EventArgs e)
        {
            PageConfig? senderPageConfig = sender as PageConfig;
            if (senderPageConfig != null)
            {
                TabPage? tp = senderPageConfig.Parent as TabPage;
                if (tp != null)
                {
                    tp.Text = senderPageConfig.PageID;
                    tp.ToolTipText = senderPageConfig.PageID;
                }
            }
        }

        private void tbAddPageConfig_Click(object sender, EventArgs e)
        {
            PageConfig pc = new PageConfig();
            pc.PageIDChanged += defaultPageConfig_PageIDChanged;
            pc.Dock = DockStyle.Fill;
            TabPage tp = new TabPage();
            tp.Controls.Add(pc);
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectedTab = tp;
            pc.PageID = "pageModel" + (tabControl1.TabPages.Count + 1);
        }

        private void tbDelPageConfig_Click(object sender, EventArgs e)
        {
            TabPage? tabPage = tabControl1.SelectedTab;
            if (tabPage != null)
            {
                int deleteIndex = tabControl1.SelectedIndex;
                tabControl1.TabPages.Remove(tabPage);
                tabPage.Dispose();

                int newIndex = deleteIndex - 1;
                if (newIndex > 0)
                {
                    tabControl1.SelectedIndex = newIndex;
                }
            }
        }

        //make tabControl vertical(tab to left)
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

            // Do not change font.
            Font _tabFont = _tabPage.Font;

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void tbCopyPageConfig_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                PageConfig pc = new PageConfig();
                pc.PageIDChanged += defaultPageConfig_PageIDChanged;
                pc.Dock = DockStyle.Fill;
                pc.JsonObj = ((PageConfig)tabControl1.SelectedTab.Controls[0]).JsonObj;
                TabPage tp = new TabPage();
                tp.Text = pc.PageID;
                tp.Controls.Add(pc);
                tabControl1.TabPages.Add(tp);
                tabControl1.SelectedTab = tp;
            }
        }

        private void tbShowTest_Click(object sender, EventArgs e)
        {
            searchTest1.Visible = tbShowTest.Checked;
        }
        public string Encoding
        {
            get
            {
                return ((PageConfig)tabControl1.SelectedTab.Controls[0]).Encoding;
            }
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
                setJsonObj(value, true);
            }
        }

        private void setJsonObj(JsonArray jsonObj, bool init)
        {
            foreach (JsonObject item in jsonObj)
            {
                PageConfig pc;
                TabControl tc = tabControl1;
                TabPage tp;
                if (jsonObj.IndexOf(item) == 0 && init)
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
                pc.PageIDChanged += defaultPageConfig_PageIDChanged;
                pc.JsonObj = item;
            }
        }

        //for test
        public SearchConfig CurrentSearchConfig
        {
            get
            {
                return ((PageConfig)tabControl1.SelectedTab.Controls[0]).CurrentSearchConfig;
            }
        }


        private JsonObject getCurrentSearchJsonObject()
        {
            return CurrentSearchConfig.JsonObj;
        }
        private async Task<string> GetHtmlStringObj(string Url)
        {
            string html =
                    await new HttpClientDownloader().GetHtmlStringAsync(
                        Url,
                        Encoding);
            return html;
        }


        private EventHandler onSaved;
        public event EventHandler Saved
        {
            add
            {
                onSaved += value;
            }
            remove
            {
                onSaved -= value;
            }
        }
        private string confnfigFile = @"xyWebScraper.cfg";
        public string ConfnfigFile { get => confnfigFile; set => confnfigFile = value; }
        private void tbSave_Click(object sender, EventArgs e)
        {
            string jsonString = JsonSerializer.Serialize(JsonObj);
            File.WriteAllText(ConfnfigFile, jsonString);
            onSaved?.Invoke(this, e);
        }

        private void tbSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                FileName = "pageConfig",
                Filter = "page config files (*.cfg)|*.cfg",
                Title = "Save config file"
            };

            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FrmPageModelSelect fpms = new FrmPageModelSelect(
                    JsonObj, 
                    "Save selected page models to - " + saveFileDialog1.FileName, 
                    "Save");
                if(fpms.ShowDialog() == DialogResult.OK)
                {
                    string jsonString = JsonSerializer.Serialize(fpms.SelectedJsonObj);
                    File.WriteAllText(saveFileDialog1.FileName, jsonString);
                }
            }
        }

        private void tbImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select a page config file",
                Filter = "page config files (*.cfg)|*.cfg",
                Title = "Select config file"
            }; 
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string json = File.ReadAllText(openFileDialog1.FileName);
                    FrmPageModelSelect fpms = new FrmPageModelSelect(
                        JsonSerializer.Deserialize<JsonArray>(json),
                        "Import selected page models from - " + openFileDialog1.FileName,
                        "Import");
                    if (fpms.ShowDialog() == DialogResult.OK)
                    {
                        setJsonObj(fpms.SelectedJsonObj, false);
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}
