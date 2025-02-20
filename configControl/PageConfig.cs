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

namespace xy.scraper.configControl
{
    public partial class PageConfig : UserControl
    {
        List<CheckBox> CheckBoxList = new List<CheckBox>();
        public PageConfig()
        {
            InitializeComponent();

            CheckBoxList.Add(pathsCb);
            CheckBoxList.Add(filesCb);
            CheckBoxList.Add(nextsCb);

            pathsCb.Tag = pathsTp;
            filesCb.Tag = filesTp;
            nextsCb.Tag = nextsTp;

            pathsCb.Checked = true;
            filesCb.Checked = true;
            nextsCb.Checked = true;

            pathsCb.CheckedChanged
                += new EventHandler(searchConfig_CheckedChanged);
            filesCb.CheckedChanged
                += new EventHandler(searchConfig_CheckedChanged);
            nextsCb.CheckedChanged
                += new EventHandler(searchConfig_CheckedChanged);

            tbAddPath.Tag = tcPath;
            tbDelPath.Tag = tcPath;
            tbAddFile.Tag = tcFile;
            tbDelFile.Tag = tcFile;
            tbAddNext.Tag = tcNext;
            tbDelNext.Tag = tcNext;

            tbAddPath.Click += new EventHandler(tbAdd_Click);
            tbDelPath.Click += new EventHandler(tbDel_Click);
            tbAddFile.Click += new EventHandler(tbAdd_Click);
            tbDelFile.Click += new EventHandler(tbDel_Click);
            tbAddNext.Click += new EventHandler(tbAdd_Click);
            tbDelNext.Click += new EventHandler(tbDel_Click);
        }
        private void searchConfig_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
            foreach (CheckBox cb in CheckBoxList)
            {
                if (cb.Checked)
                {
                    tabControl1.TabPages.Add((TabPage)cb.Tag);
                }
            }
        }

        private void tbAdd_Click(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)((ToolStripButton)sender).Tag;
            TabPage tp = new TabPage();
            tp.Text = ((TabPage)tc.Controls[0]).Text 
                + (tc.Controls.Count + 1);
            tc.Controls.Add(tp);
            SearchConfig sc = new SearchConfig();
            sc.Dock = DockStyle.Fill;
            tp.Controls.Add(sc);

            if(tc == tcNext)
            {
                addNextTargetTextBox(tp);
            }
        }
        private void addNextTargetTextBox(TabPage tp)
        {
            Panel panel = new Panel();
            tp.Controls.Add(panel);
            panel.Dock = DockStyle.Top;
            panel.Height = panel3.Height;

            Label label = new Label();
            panel.Controls.Add(label);
            label.Text = label3.Text;
            label.Top = label3.Top;
            label.Left = label3.Left;

            TextBox textBox = new TextBox();
            panel.Controls.Add(textBox);
            textBox.Top = textBox1.Top;
            textBox.Left = textBox1.Left;
            textBox.Width = textBox1.Width;
        }

        private void tbDel_Click(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)((ToolStripButton)sender).Tag;
            TabPage tp = tc.SelectedTab;
            if (tc.Controls.IndexOf(tp) != 0)
            {
                tc.Controls.Remove(tp);
                tp.Dispose();
            }
        }

        public string PageID
        {
            get
            {
                return txtPageID.Text;
            }

            set
            {
                txtPageID.Text = value;
            }
        }

        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();

                json["cfgig"] = PageID;
                json["encoding"] = txtCoding.Text;

                if (pathsCb.Checked)
                {
                    JsonArray paths = new JsonArray();
                    json["paths"] = paths;
                    foreach (TabPage tp in tcPath.Controls)
                    {
                        SearchConfig sc = (SearchConfig)tp.Controls[0];
                        paths.Add(sc.JsonObj);
                    }
                }

                if (filesCb.Checked)
                {
                    JsonArray files = new JsonArray();
                    json["files"] = files;
                    foreach (TabPage tp in tcFile.Controls)
                    {
                        SearchConfig sc = (SearchConfig)tp.Controls[0];
                        files.Add(sc.JsonObj);
                    }
                }

                if (nextsCb.Checked)
                {
                    JsonArray nexts = new JsonArray();
                    json["nexts"] = nexts;
                    foreach (TabPage tp in tcNext.Controls)
                    {
                        SearchConfig sc = (SearchConfig)tp.Controls[0];

                        JsonObject nextObj = new JsonObject();
                        nextObj["nextcfgid"] = ((TextBox)tp.Controls[1].Controls[1]).Text;
                        nextObj["searchs"] = sc.JsonObj;

                        nexts.Add(nextObj);
                    }
                }

                return json;
            }

            set
            {
                PageID
                    = value["cfgig"].GetValue<String>();
                txtCoding.Text
                    = value["encoding"].GetValue<String>();

                if (value.ContainsKey("paths"))
                {
                    JsonArray paths
                        = value["paths"].AsArray();
                    foreach (JsonObject item in paths)
                    {
                        SearchConfig sc;

                        TabControl tc = tcPath;
                        TabPage tp;
                        if (paths.IndexOf(item) == 0)
                        {
                            tp = ((TabPage)tc.Controls[0]);
                            sc = ((SearchConfig)tp.Controls[0]);
                        }
                        else
                        {
                            sc = new SearchConfig();
                            sc.Dock = DockStyle.Fill;

                            tp = new TabPage();
                            tp.Text = ((TabPage)tc.Controls[0]).Text
                                + (tc.Controls.Count + 1);
                            tc.Controls.Add(tp);
                            tp.Controls.Add(sc);
                        }
                        sc.JsonObj = item;
                    }
                    pathsCb.Checked = true;
                }
                else
                {
                    pathsCb.Checked = false;
                }

                if (value.ContainsKey("files"))
                {
                    JsonArray files
                        = value["files"].AsArray();
                    foreach (JsonObject item in files)
                    {
                        SearchConfig sc;

                        TabControl tc = tcFile;
                        TabPage tp;
                        if (files.IndexOf(item) == 0)
                        {
                            tp = ((TabPage)tc.Controls[0]);
                            sc = ((SearchConfig)tp.Controls[0]);
                        }
                        else
                        {
                            sc = new SearchConfig();
                            sc.Dock = DockStyle.Fill;

                            tp = new TabPage();
                            tp.Text = ((TabPage)tc.Controls[0]).Text
                                + (tc.Controls.Count + 1);
                            tc.Controls.Add(tp);
                            tp.Controls.Add(sc);
                        }
                        sc.JsonObj = item;
                    }
                    filesCb.Checked = true;
                }
                else
                {
                    filesCb.Checked = false;
                }

                if (value.ContainsKey("nexts"))
                {
                    JsonArray nexts
                    = value["nexts"].AsArray();
                    foreach (JsonObject item in nexts)
                    {
                        SearchConfig sc = new SearchConfig();
                        sc.Dock = DockStyle.Fill;

                        TabControl tc = tcNext;
                        TabPage tp;
                        if (nexts.IndexOf(item) == 0)
                        {
                            tp = ((TabPage)tc.Controls[0]);
                            sc = ((SearchConfig)tp.Controls[0]);
                        }
                        else
                        {
                            sc = new SearchConfig();
                            sc.Dock = DockStyle.Fill;

                            tp = new TabPage();
                            tp.Text = ((TabPage)tc.Controls[0]).Text
                                + (tc.Controls.Count + 1);
                            tc.Controls.Add(tp);
                            tp.Controls.Add(sc);
                            addNextTargetTextBox(tp);
                        }
                        sc.JsonObj = item["searchs"].AsObject();

                        ((TextBox)tp.Controls[1].Controls[1]).Text
                            = item["nextcfgid"].GetValue<String>();
                    }
                    nextsCb.Checked = true;
                }
                else
                {
                    nextsCb.Checked = false;
                }
            }
        }
    }
}
