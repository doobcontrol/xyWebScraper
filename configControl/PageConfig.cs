﻿using System;
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
using System.Globalization;
using System.Text.RegularExpressions;
using System.Resources;
using xy.scraper.configControl.Properties;

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

            setUiText();
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
        private void setUiText()
        {
            lbPageConfigID.Text = Resources.lbPageConfigID;
            lbNextPageConfigID.Text = Resources.lbPageConfigID;
            lbEncoding.Text = Resources.lbEncoding;
            pathsCb.Text = Resources.pathsCb;
            filesCb.Text = Resources.filesCb;
            nextsCb.Text = Resources.nextsCb;
            pathsTp.Text = Resources.pathsCb;
            filesTp.Text = Resources.filesCb;
            nextsTp.Text = Resources.nextsCb;
            tcPath.Controls[0].Text = Resources.pathsCb;
            tcFile.Controls[0].Text = Resources.filesCb;
            tcNext.Controls[0].Text = Resources.nextsCb;
            tbAddPath.ToolTipText = Resources.tbAddPath;
            tbDelPath.ToolTipText = Resources.tbDelPath;
            tbAddFile.ToolTipText = Resources.tbAddFile;
            tbDelFile.ToolTipText = Resources.tbDelFile;
            tbAddNext.ToolTipText = Resources.tbAddNext;
            tbDelNext.ToolTipText = Resources.tbDelNext;
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
            tc.SelectedTab = tp;

            if (tc == tcNext)
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
            label.Text = lbNextPageConfigID.Text;
            label.Top = lbNextPageConfigID.Top;
            label.Left = lbNextPageConfigID.Left;

            TextBox textBox = new TextBox();
            panel.Controls.Add(textBox);
            textBox.Top = txtNextUrlPageID.Top;
            textBox.Left = txtNextUrlPageID.Left;
            textBox.Width = txtNextUrlPageID.Width;
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

        private void txtPageID_TextChanged(object sender, EventArgs e)
        {
            onPageIDChanged?.Invoke(this, e);
        }

        private EventHandler onPageIDChanged;
        public event EventHandler PageIDChanged
        {
            add
            {
                onPageIDChanged += value;
            }
            remove
            {
                onPageIDChanged -= value;
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
        public string Encoding
        {
            get
            {
                return txtCoding.Text;
            }
            set
            {
                txtCoding.Text = value;
            }
        }

        //for test
        public SearchConfig CurrentSearchConfig
        {
            get
            {
                return (SearchConfig)((TabControl)tabControl1.SelectedTab.Controls[0])
                    .SelectedTab.Controls[0];
            }
        }

        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();

                json[JCfgName.cfgid] = PageID;
                json[JCfgName.encoding] = txtCoding.Text;

                if (pathsCb.Checked)
                {
                    JsonArray paths = new JsonArray();
                    json[JCfgName.paths] = paths;
                    foreach (TabPage tp in tcPath.Controls)
                    {
                        SearchConfig sc = (SearchConfig)tp.Controls[0];
                        paths.Add(sc.JsonObj);
                    }
                }

                if (filesCb.Checked)
                {
                    JsonArray files = new JsonArray();
                    json[JCfgName.files] = files;
                    foreach (TabPage tp in tcFile.Controls)
                    {
                        SearchConfig sc = (SearchConfig)tp.Controls[0];
                        files.Add(sc.JsonObj);
                    }
                }

                if (nextsCb.Checked)
                {
                    JsonArray nexts = new JsonArray();
                    json[JCfgName.nexts] = nexts;
                    foreach (TabPage tp in tcNext.Controls)
                    {
                        SearchConfig sc = (SearchConfig)tp.Controls[0];

                        JsonObject nextObj = new JsonObject();
                        nextObj[JCfgName.cfgid] = ((TextBox)tp.Controls[1].Controls[1]).Text;
                        nextObj[JCfgName.searchs] = sc.JsonObj;

                        nexts.Add(nextObj);
                    }
                }

                return json;
            }

            set
            {
                PageID
                    = value[JCfgName.cfgid].GetValue<String>();
                txtCoding.Text
                    = value[JCfgName.encoding].GetValue<String>();

                if (value.ContainsKey(JCfgName.paths))
                {
                    JsonArray paths
                        = value[JCfgName.paths].AsArray();
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

                if (value.ContainsKey(JCfgName.files))
                {
                    JsonArray files
                        = value[JCfgName.files].AsArray();
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

                if (value.ContainsKey(JCfgName.nexts))
                {
                    JsonArray nexts
                    = value[JCfgName.nexts].AsArray();
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
                        sc.JsonObj = item[JCfgName.searchs].AsObject();

                        ((TextBox)tp.Controls[1].Controls[1]).Text
                            = item[JCfgName.cfgid].GetValue<String>();
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
