using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using xy.scraper.configControl;
using xy.scraper.page.parserConfig;

namespace configControlTest
{
    [TestClass]
    public class PageConfigTests
    {
        public static Control? GetControl(Control control, string name)
        {
            List<Type> exclusiveInTypes = new List<Type>() { typeof(SearchConfig) };
            return CTool.GetControl(control, name, exclusiveInTypes);
        }

        [TestMethod]
        public void PageConfigTests_SearchTypeCheck()
        {
            PageConfig pageConfig = new PageConfig();
            pageConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.Width *= 2;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(pageConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            CheckBox? pathsCb =
                GetControl(pageConfig, "pathsCb") as CheckBox;
            CheckBox? filesCb =
                GetControl(pageConfig, "filesCb") as CheckBox;
            CheckBox? nextsCb =
                GetControl(pageConfig, "nextsCb") as CheckBox;
            Assert.IsNotNull(pathsCb);
            Assert.IsNotNull(filesCb);
            Assert.IsNotNull(nextsCb);
            Assert.IsTrue(pathsCb.Checked);
            Assert.IsTrue(filesCb.Checked);
            Assert.IsTrue(nextsCb.Checked);

            pathsCb.Checked = false;
            filesCb.Checked = false;

            TabControl? tabControl1
                = GetControl(pageConfig, "tabControl1") as TabControl;
            Assert.IsNotNull(tabControl1);
            Assert.AreEqual(tabControl1.Controls.Count, 1);
            Assert.AreEqual(tabControl1.SelectedIndex, 0);
            Assert.AreEqual(tabControl1.Controls[0].Text, "Urls");

            pathsCb.Checked = true;
            filesCb.Checked = true;
            nextsCb.Checked = false;
            Assert.AreEqual(tabControl1.Controls.Count, 2);
            Assert.AreEqual(tabControl1.SelectedIndex, 0);
            Assert.AreEqual(tabControl1.Controls[0].Text, "paths");
            Assert.AreEqual(tabControl1.Controls[1].Text, "files");

            pathsCb.Checked = true;
            filesCb.Checked = true;
            nextsCb.Checked = true;
            Assert.AreEqual(tabControl1.Controls.Count, 3);
            Assert.AreEqual(tabControl1.SelectedIndex, 0);
            Assert.AreEqual(tabControl1.Controls[0].Text, "paths");
            Assert.AreEqual(tabControl1.Controls[1].Text, "files");
            Assert.AreEqual(tabControl1.Controls[2].Text, "Urls");
        }
        [TestMethod]
        public void PageConfigTests_Paths_Add_Del()
        {
            PageConfig pageConfig = new PageConfig();
            pageConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.Width *= 2;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(pageConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            ToolStrip? toolStrip1 =
                GetControl(pageConfig, "toolStrip1") as ToolStrip;
            Assert.IsNotNull(toolStrip1);
            ToolStripButton? tbAddPath =
                toolStrip1.Items["tbAddPath"] as ToolStripButton;
            ToolStripButton? tbDelPath =
                toolStrip1.Items["tbDelPath"] as ToolStripButton;
            Assert.IsNotNull(tbAddPath);
            Assert.IsNotNull(tbDelPath);

            TabControl? tcPath =
                GetControl(pageConfig, "tcPath") as TabControl;
            Assert.IsNotNull(tcPath);

            int pathCount = 1;
            string path1Name = tcPath.Controls[0].Text;

            Assert.IsNotNull(tcPath);
            Assert.AreEqual(pathCount, tcPath.Controls.Count);
            Assert.AreEqual(pathCount - 1, tcPath.SelectedIndex);
            Assert.AreEqual(path1Name, tcPath.SelectedTab.Text);

            //add one path
            tbAddPath.PerformClick();
            pathCount++;
            Assert.AreEqual(pathCount, tcPath.Controls.Count);
            Assert.AreEqual(pathCount - 1, tcPath.SelectedIndex);
            Assert.AreEqual(path1Name + pathCount, tcPath.SelectedTab.Text);

            //add four paths
            tbAddPath.PerformClick();
            pathCount++;
            tbAddPath.PerformClick();
            pathCount++;
            tbAddPath.PerformClick();
            pathCount++;
            tbAddPath.PerformClick();
            pathCount++;
            Assert.AreEqual(pathCount, tcPath.Controls.Count);
            Assert.AreEqual(pathCount - 1, tcPath.SelectedIndex);
            Assert.AreEqual(path1Name + pathCount, tcPath.SelectedTab.Text);

            //delete one path(the last one)
            tbDelPath.PerformClick();
            pathCount--;
            Assert.AreEqual(pathCount, tcPath.Controls.Count);
            Assert.AreEqual(0, tcPath.SelectedIndex);

            //delete one path(the first one, failure)
            tbDelPath.PerformClick();
            tbDelPath.PerformClick();
            tbDelPath.PerformClick();
            tbDelPath.PerformClick();
            tbDelPath.PerformClick();
            Assert.AreEqual(pathCount, tcPath.Controls.Count);
            Assert.AreEqual(0, tcPath.SelectedIndex);

            //select a path, and delete
            tcPath.SelectedIndex = 1;
            tbDelPath.PerformClick();
            pathCount--;
            Assert.AreEqual(pathCount, tcPath.Controls.Count);
            Assert.AreEqual(0, tcPath.SelectedIndex);

            //delete all paths other and the first one
            for (int i = 1; i < pathCount; i++)
            {
                tcPath.SelectedIndex = tcPath.Controls.Count - 1;
                tbDelPath.PerformClick();
            }
            pathCount = 1;
            Assert.AreEqual(pathCount, tcPath.Controls.Count);
            Assert.AreEqual(0, tcPath.SelectedIndex);
        }
        [TestMethod]
        public void PageConfigTests_Files_Add_Del()
        {
            PageConfig pageConfig = new PageConfig();
            pageConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.Width *= 2;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(pageConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            TabControl? tabControl1 =
                GetControl(pageConfig, "tabControl1") as TabControl;
            Assert.IsNotNull(tabControl1);
            tabControl1.SelectedIndex = 1;

            ToolStrip? toolStrip1 =
                GetControl(pageConfig, "toolStrip2") as ToolStrip;
            Assert.IsNotNull(toolStrip1);
            ToolStripButton? tbAddFile =
                toolStrip1.Items["tbAddFile"] as ToolStripButton;
            ToolStripButton? tbDelFile =
                toolStrip1.Items["tbDelFile"] as ToolStripButton;
            Assert.IsNotNull(tbAddFile);
            Assert.IsNotNull(tbDelFile);

            TabControl? tcFile =
                GetControl(pageConfig, "tcFile") as TabControl;
            Assert.IsNotNull(tcFile);

            int fileCount = 1;
            string file1Name = tcFile.Controls[0].Text;

            Assert.IsNotNull(tcFile);
            Assert.AreEqual(fileCount, tcFile.Controls.Count);
            Assert.AreEqual(fileCount - 1, tcFile.SelectedIndex);
            Assert.AreEqual(file1Name, tcFile.SelectedTab.Text);

            //add one file
            tbAddFile.PerformClick();
            fileCount++;
            Assert.AreEqual(fileCount, tcFile.Controls.Count);
            Assert.AreEqual(fileCount - 1, tcFile.SelectedIndex);
            Assert.AreEqual(file1Name + fileCount, tcFile.SelectedTab.Text);

            //add four files
            tbAddFile.PerformClick();
            fileCount++;
            tbAddFile.PerformClick();
            fileCount++;
            tbAddFile.PerformClick();
            fileCount++;
            tbAddFile.PerformClick();
            fileCount++;
            Assert.AreEqual(fileCount, tcFile.Controls.Count);
            Assert.AreEqual(fileCount - 1, tcFile.SelectedIndex);
            Assert.AreEqual(file1Name + fileCount, tcFile.SelectedTab.Text);

            //delete one file(the last one)
            tbDelFile.PerformClick();
            fileCount--;
            Assert.AreEqual(fileCount, tcFile.Controls.Count);
            Assert.AreEqual(0, tcFile.SelectedIndex);

            //delete one file(the first one, failure)
            tbDelFile.PerformClick();
            tbDelFile.PerformClick();
            tbDelFile.PerformClick();
            tbDelFile.PerformClick();
            tbDelFile.PerformClick();
            Assert.AreEqual(fileCount, tcFile.Controls.Count);
            Assert.AreEqual(0, tcFile.SelectedIndex);

            //select a file, and delete
            tcFile.SelectedIndex = 1;
            tbDelFile.PerformClick();
            fileCount--;
            Assert.AreEqual(fileCount, tcFile.Controls.Count);
            Assert.AreEqual(0, tcFile.SelectedIndex);

            //delete all files other and the first one
            for (int i = 1; i < fileCount; i++)
            {
                tcFile.SelectedIndex = tcFile.Controls.Count - 1;
                tbDelFile.PerformClick();
            }
            fileCount = 1;
            Assert.AreEqual(fileCount, tcFile.Controls.Count);
            Assert.AreEqual(0, tcFile.SelectedIndex);
        }
        [TestMethod]
        public void PageConfigTests_Nexts_Add_Del()
        {
            PageConfig pageConfig = new PageConfig();
            pageConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.Width *= 2;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(pageConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            TabControl? tabControl1 =
                GetControl(pageConfig, "tabControl1") as TabControl;
            Assert.IsNotNull(tabControl1);
            tabControl1.SelectedIndex = 2;

            ToolStrip? toolStrip1 =
                GetControl(pageConfig, "toolStrip3") as ToolStrip;
            Assert.IsNotNull(toolStrip1);
            ToolStripButton? tbAddNext =
                toolStrip1.Items["tbAddNext"] as ToolStripButton;
            ToolStripButton? tbDelNext =
                toolStrip1.Items["tbDelNext"] as ToolStripButton;
            Assert.IsNotNull(tbAddNext);
            Assert.IsNotNull(tbDelNext);

            TabControl? tcNext =
                GetControl(pageConfig, "tcNext") as TabControl;
            Assert.IsNotNull(tcNext);

            int nextCount = 1;
            string next1Name = tcNext.Controls[0].Text;

            Assert.IsNotNull(tcNext);
            Assert.AreEqual(nextCount, tcNext.Controls.Count);
            Assert.AreEqual(nextCount - 1, tcNext.SelectedIndex);
            Assert.AreEqual(next1Name, tcNext.SelectedTab.Text);

            //add one next
            tbAddNext.PerformClick();
            nextCount++;
            Assert.AreEqual(nextCount, tcNext.Controls.Count);
            Assert.AreEqual(nextCount - 1, tcNext.SelectedIndex);
            Assert.AreEqual(next1Name + nextCount, tcNext.SelectedTab.Text);

            //add four nexts
            tbAddNext.PerformClick();
            nextCount++;
            tbAddNext.PerformClick();
            nextCount++;
            tbAddNext.PerformClick();
            nextCount++;
            tbAddNext.PerformClick();
            nextCount++;
            Assert.AreEqual(nextCount, tcNext.Controls.Count);
            Assert.AreEqual(nextCount - 1, tcNext.SelectedIndex);
            Assert.AreEqual(next1Name + nextCount, tcNext.SelectedTab.Text);

            //delete one next(the last one)
            tbDelNext.PerformClick();
            nextCount--;
            Assert.AreEqual(nextCount, tcNext.Controls.Count);
            Assert.AreEqual(0, tcNext.SelectedIndex);

            //delete one next(the first one, failure)
            tbDelNext.PerformClick();
            tbDelNext.PerformClick();
            tbDelNext.PerformClick();
            tbDelNext.PerformClick();
            tbDelNext.PerformClick();
            Assert.AreEqual(nextCount, tcNext.Controls.Count);
            Assert.AreEqual(0, tcNext.SelectedIndex);

            //select a next, and delete
            tcNext.SelectedIndex = 1;
            tbDelNext.PerformClick();
            nextCount--;
            Assert.AreEqual(nextCount, tcNext.Controls.Count);
            Assert.AreEqual(0, tcNext.SelectedIndex);

            //delete all nexts other and the first one
            for (int i = 1; i < nextCount; i++)
            {
                tcNext.SelectedIndex = tcNext.Controls.Count - 1;
                tbDelNext.PerformClick();
            }
            nextCount = 1;
            Assert.AreEqual(nextCount, tcNext.Controls.Count);
            Assert.AreEqual(0, tcNext.SelectedIndex);
        }

        [TestMethod]
        public void PageConfigTests_JsonObj_get()
        {
            JsonObject expectedJObj = CTool.testPageConfig_JsonObj();

            PageConfig pageConfig = new PageConfig();
            pageConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.Width *= 2;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(pageConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            TextBox? txtPageID =
                GetControl(pageConfig, "txtPageID") as TextBox;
            Assert.IsNotNull(txtPageID);
            txtPageID.Text = expectedJObj[JCfgName.cfgid].GetValue<string>();

            TextBox? txtCoding =
                GetControl(pageConfig, "txtCoding") as TextBox;
            Assert.IsNotNull(txtCoding);
            txtCoding.Text = expectedJObj[JCfgName.encoding].GetValue<string>();

            CheckBox? pathsCb =
                GetControl(pageConfig, "pathsCb") as CheckBox;
            Assert.IsNotNull(pathsCb);

            if (expectedJObj.ContainsKey(JCfgName.paths))
            {
                pathsCb.Checked = true;

                ToolStrip? toolStrip1 =
                    GetControl(pageConfig, "toolStrip1") as ToolStrip;
                Assert.IsNotNull(toolStrip1);
                ToolStripButton? tbAddPath =
                    toolStrip1.Items["tbAddPath"] as ToolStripButton;
                Assert.IsNotNull(tbAddPath);
                TabControl? tcPath =
                    GetControl(pageConfig, "tcPath") as TabControl;
                Assert.IsNotNull(tcPath);


                JsonArray paths
                    = expectedJObj[JCfgName.paths].AsArray();
                foreach (JsonObject item in paths)
                {
                    int index = paths.IndexOf(item);
                    if (index > 0)
                    {
                        tbAddPath.PerformClick();
                    }
                    SearchConfig? searchConfig
                        = tcPath.TabPages[index].Controls[0] as SearchConfig;
                    Assert.IsNotNull(searchConfig);
                    searchConfig.JsonObj = item;
                }
            }
            else
            {
                pathsCb.Checked = false;
            }

            if (expectedJObj.ContainsKey(JCfgName.files))
            {
                pathsCb.Checked = true;

                ToolStrip? toolStrip1 =
                    GetControl(pageConfig, "toolStrip2") as ToolStrip;
                Assert.IsNotNull(toolStrip1);
                ToolStripButton? tbAddFile =
                    toolStrip1.Items["tbAddFile"] as ToolStripButton;
                Assert.IsNotNull(tbAddFile);
                TabControl? tcFile =
                    GetControl(pageConfig, "tcFile") as TabControl;
                Assert.IsNotNull(tcFile);


                JsonArray files
                    = expectedJObj[JCfgName.files].AsArray();
                foreach (JsonObject item in files)
                {
                    int index = files.IndexOf(item);
                    if (index > 0)
                    {
                        tbAddFile.PerformClick();
                    }
                    SearchConfig? searchConfig
                        = tcFile.TabPages[index].Controls[0] as SearchConfig;
                    Assert.IsNotNull(searchConfig);
                    searchConfig.JsonObj = item;
                }
            }
            else
            {
                pathsCb.Checked = false;
            }

            if (expectedJObj.ContainsKey(JCfgName.nexts))
            {
                pathsCb.Checked = true;

                ToolStrip? toolStrip1 =
                    GetControl(pageConfig, "toolStrip3") as ToolStrip;
                Assert.IsNotNull(toolStrip1);
                ToolStripButton? tbAddNext =
                    toolStrip1.Items["tbAddNext"] as ToolStripButton;
                Assert.IsNotNull(tbAddNext);
                TabControl? tcNext =
                    GetControl(pageConfig, "tcNext") as TabControl;
                Assert.IsNotNull(tcNext);


                JsonArray nexts
                    = expectedJObj[JCfgName.nexts].AsArray();
                foreach (JsonObject item in nexts)
                {
                    int index = nexts.IndexOf(item);
                    if (index > 0)
                    {
                        tbAddNext.PerformClick();
                    }
                    SearchConfig? searchConfig
                        = tcNext.TabPages[index].Controls[0] as SearchConfig;
                    Assert.IsNotNull(searchConfig);
                    searchConfig.JsonObj = item[JCfgName.searchs].AsObject();

                    TextBox? nextTarget
                        = tcNext.TabPages[index].Controls[1].Controls[1] as TextBox;
                    Assert.IsNotNull(nextTarget);
                    nextTarget.Text = item[JCfgName.cfgid].GetValue<string>();
                }
            }
            else
            {
                pathsCb.Checked = false;
            }

            Assert.AreEqual(expectedJObj.ToJsonString(),
                pageConfig.JsonObj.ToJsonString());
        }

        [TestMethod]
        public void PageConfigTests_JsonObj_set()
        {
            JsonObject expectedJObj = CTool.testPageConfig_JsonObj();

            PageConfig pageConfig = new PageConfig();
            pageConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            pageConfig.JsonObj = expectedJObj;

            Form testForm = new Form();
            testForm.Width *= 2;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(pageConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            TextBox? txtPageID =
                GetControl(pageConfig, "txtPageID") as TextBox;
            Assert.IsNotNull(txtPageID);
            Assert.AreEqual(expectedJObj[JCfgName.cfgid].GetValue<string>(),
                txtPageID.Text);

            TextBox? txtCoding =
                GetControl(pageConfig, "txtCoding") as TextBox;
            Assert.IsNotNull(txtCoding);
            Assert.AreEqual(expectedJObj[JCfgName.encoding].GetValue<string>(),
                txtCoding.Text);

            CheckBox? pathsCb =
                GetControl(pageConfig, "pathsCb") as CheckBox;
            Assert.IsNotNull(pathsCb);
            Assert.AreEqual(expectedJObj.ContainsKey(JCfgName.paths), 
                pathsCb.Checked);
            if (pathsCb.Checked)
            {
                ToolStrip? toolStrip1 =
                    GetControl(pageConfig, "toolStrip1") as ToolStrip;
                Assert.IsNotNull(toolStrip1);
                ToolStripButton? tbAddPath =
                    toolStrip1.Items["tbAddPath"] as ToolStripButton;
                Assert.IsNotNull(tbAddPath);
                TabControl? tcPath =
                    GetControl(pageConfig, "tcPath") as TabControl;
                Assert.IsNotNull(tcPath);

                JsonArray paths
                    = expectedJObj[JCfgName.paths].AsArray();
                foreach (JsonObject item in paths)
                {
                    int index = paths.IndexOf(item);
                    SearchConfig? searchConfig
                        = tcPath.TabPages[index].Controls[0] as SearchConfig;
                    Assert.IsNotNull(searchConfig);
                    Assert.AreEqual(item.ToJsonString(),
                        searchConfig.JsonObj.ToJsonString());
                }
            }

            CheckBox? filesCb =
                GetControl(pageConfig, "filesCb") as CheckBox;
            Assert.IsNotNull(filesCb);
            Assert.AreEqual(expectedJObj.ContainsKey(JCfgName.files),
                filesCb.Checked);
            if (filesCb.Checked)
            {
                ToolStrip? toolStrip1 =
                    GetControl(pageConfig, "toolStrip2") as ToolStrip;
                Assert.IsNotNull(toolStrip1);
                ToolStripButton? tbAddFile =
                    toolStrip1.Items["tbAddFile"] as ToolStripButton;
                Assert.IsNotNull(tbAddFile);
                TabControl? tcFile =
                    GetControl(pageConfig, "tcFile") as TabControl;
                Assert.IsNotNull(tcFile);

                JsonArray files
                    = expectedJObj[JCfgName.files].AsArray();
                foreach (JsonObject item in files)
                {
                    int index = files.IndexOf(item);
                    SearchConfig? searchConfig
                        = tcFile.TabPages[index].Controls[0] as SearchConfig;
                    Assert.IsNotNull(searchConfig);
                    Assert.AreEqual(item.ToJsonString(),
                        searchConfig.JsonObj.ToJsonString());
                }
            }

            CheckBox? nextsCb =
                GetControl(pageConfig, "nextsCb") as CheckBox;
            Assert.IsNotNull(nextsCb);
            Assert.AreEqual(expectedJObj.ContainsKey(JCfgName.nexts),
                nextsCb.Checked);
            if (nextsCb.Checked)
            {
                ToolStrip? toolStrip1 =
                    GetControl(pageConfig, "toolStrip3") as ToolStrip;
                Assert.IsNotNull(toolStrip1);
                ToolStripButton? tbAddNext =
                    toolStrip1.Items["tbAddNext"] as ToolStripButton;
                Assert.IsNotNull(tbAddNext);
                TabControl? tcNext =
                    GetControl(pageConfig, "tcNext") as TabControl;
                Assert.IsNotNull(tcNext);

                JsonArray nexts
                    = expectedJObj[JCfgName.nexts].AsArray();
                foreach (JsonObject item in nexts)
                {
                    int index = nexts.IndexOf(item);
                    SearchConfig? searchConfig
                        = tcNext.TabPages[index].Controls[0] as SearchConfig;
                    Assert.IsNotNull(searchConfig);
                    Assert.AreEqual(item[JCfgName.searchs].AsObject().ToJsonString(),
                        searchConfig.JsonObj.ToJsonString());

                    TextBox? nextTarget
                        = tcNext.TabPages[index].Controls[1].Controls[1] as TextBox;
                    Assert.IsNotNull(nextTarget);
                    Assert.AreEqual(item[JCfgName.cfgid].GetValue<string>(),
                        nextTarget.Text);
                }
            }
        }
    }
}
