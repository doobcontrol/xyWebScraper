using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using xy.scraper.configControl;
using xy.scraper.page.parserConfig;

namespace configControlTest
{
    [TestClass]
    public class SearchConfigTests
    {
        [TestMethod]
        public void SearchConfig_Add_Del_SearchLayer()
        {
            SearchConfig searchConfig = new SearchConfig();
            searchConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.SuspendLayout();
            testForm.Controls.Add(searchConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            ToolStrip? toolStrip1 =
                CTool.GetControl(searchConfig, "toolStrip1") as ToolStrip;

            if (toolStrip1 == null)
            {
                Assert.Fail();
            }

            ToolStripButton? tbAddSearchLayer = 
                toolStrip1.Items["tbAddSearchLayer"] as ToolStripButton;
            ToolStripButton? tbDelSearchLayer = 
                toolStrip1.Items["tbDelSearchLayer"] as ToolStripButton;

            if (tbAddSearchLayer != null && tbDelSearchLayer != null)
            {
                tbAddSearchLayer.PerformClick();

                Panel? panel1 =
                    CTool.GetControl(searchConfig, "panel1") as Panel;
                if (panel1 != null)
                {
                    Assert.AreEqual(panel1.Controls.Count, 2);
                    //the new control be BringToFront, so it is the first control
                    Assert.IsTrue(panel1.Controls[0] is SearchLayer);
                }
                else
                {
                    Assert.Fail();
                }

                panel1.Controls[0].Focus();
                tbDelSearchLayer.PerformClick();

                Assert.AreEqual(1, panel1.Controls.Count);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SearchConfig_Add_Del_Replace()
        {
            SearchConfig searchConfig = new SearchConfig();
            searchConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.SuspendLayout();
            testForm.Controls.Add(searchConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            ToolStrip? toolStrip2 =
                CTool.GetControl(searchConfig, "toolStrip2") as ToolStrip;

            Assert.IsNotNull(toolStrip2);

            TabPage? tpFinalHandle = 
                CTool.GetControl(searchConfig, "tpFinalHandle") as TabPage;
            Assert.IsNotNull(tpFinalHandle);
            ToolStripButton? tbAddReplace =
                toolStrip2.Items["tbAddReplace"] as ToolStripButton;
            Assert.IsNotNull(tbAddReplace);
            ToolStripButton? tbDelReplace =
                toolStrip2.Items["tbDelReplace"] as ToolStripButton;
            Assert.IsNotNull(tbDelReplace);
            TextBox? txtAddReplace =
                CTool.GetControl(searchConfig, "txtAddReplace") as TextBox;
            Assert.IsNotNull(txtAddReplace);

            TabControl? tabControl = (tpFinalHandle.Parent as TabControl);
            Assert.IsNotNull(tabControl);
            tabControl.SelectedTab = tpFinalHandle;

            string testStr = "testabc";
            txtAddReplace.Text = testStr;
            tbAddReplace.PerformClick();

            ListBox? lbReplaceList =
                CTool.GetControl(searchConfig, "lbReplaceList") as ListBox;
            Assert.IsNotNull(lbReplaceList);

            Assert.AreEqual(1, lbReplaceList.Items.Count);
            Assert.AreEqual(testStr, lbReplaceList.Items[0].ToString());
            Assert.AreEqual("", txtAddReplace.Text);

            lbReplaceList.SelectedIndex = 0; ;
            tbDelReplace.PerformClick();

            Assert.AreEqual(0, lbReplaceList.Items.Count);

            for(int i = 0; i < 10; i++)
            {
                txtAddReplace.Text = testStr + i;
                tbAddReplace.PerformClick();

                Assert.AreEqual(i + 1, lbReplaceList.Items.Count);
                Assert.AreEqual(testStr + i, lbReplaceList.Items[i].ToString());
                Assert.AreEqual("", txtAddReplace.Text);
            }

            while(lbReplaceList.Items.Count > 0)
            {
                int count = lbReplaceList.Items.Count;
                lbReplaceList.SelectedIndex = count - 1 ;
                tbDelReplace.PerformClick();
                Assert.AreEqual(count - 1, lbReplaceList.Items.Count);
            }
        }

        [TestMethod]
        public void SearchConfig_JsonObj_get()
        {
            SearchConfig searchConfig = new SearchConfig();
            searchConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.SuspendLayout();
            testForm.Controls.Add(searchConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            ToolStrip? toolStrip1 =
                CTool.GetControl(searchConfig, "toolStrip1") as ToolStrip;
            Assert.IsNotNull(toolStrip1);
            ToolStripButton? tbAddSearchLayer =
                toolStrip1.Items["tbAddSearchLayer"] as ToolStripButton;
            Assert.IsNotNull(tbAddSearchLayer);
            tbAddSearchLayer.PerformClick();

            Panel? panel1 =
                CTool.GetControl(searchConfig, "panel1") as Panel;
            Assert.IsNotNull(panel1);
            Assert.AreEqual(panel1.Controls.Count, 2);

            SearchLayer? searchLayer1 = panel1.Controls[0] as SearchLayer;
            SearchLayer? searchLayer0 = panel1.Controls[1] as SearchLayer;
            Assert.IsNotNull(searchLayer0);
            Assert.IsNotNull(searchLayer1);
            string startString0 = "startString0";
            string endString0 = "endString0";
            string startString1 = "startString1";
            string endString1 = "endString1";
            searchLayer0.Start = startString0;
            searchLayer0.End = endString0;
            searchLayer1.Start = startString1;
            searchLayer1.End = endString1;


            ToolStrip? toolStrip2 =
                CTool.GetControl(searchConfig, "toolStrip2") as ToolStrip;
            Assert.IsNotNull(toolStrip2);
            TabPage? tpFinalHandle =
                CTool.GetControl(searchConfig, "tpFinalHandle") as TabPage;
            Assert.IsNotNull(tpFinalHandle);
            ToolStripButton? tbAddReplace =
                toolStrip2.Items["tbAddReplace"] as ToolStripButton;
            Assert.IsNotNull(tbAddReplace);
            TextBox? txtAddReplace =
                CTool.GetControl(searchConfig, "txtAddReplace") as TextBox;
            Assert.IsNotNull(txtAddReplace);
            TabControl? tabControl = (tpFinalHandle.Parent as TabControl);
            Assert.IsNotNull(tabControl);
            tabControl.SelectedTab = tpFinalHandle;

            string testReplace0 = "testReplace0";
            txtAddReplace.Text = testReplace0;
            tbAddReplace.PerformClick();
            string testReplace1 = "testReplace1";
            txtAddReplace.Text = testReplace1;
            tbAddReplace.PerformClick();

            ListBox? lbReplaceList =
                CTool.GetControl(searchConfig, "lbReplaceList") as ListBox;
            Assert.IsNotNull(lbReplaceList);

            Assert.AreEqual(2, lbReplaceList.Items.Count);
            Assert.AreEqual("", txtAddReplace.Text);

            TextBox? txtAddBefore =
                CTool.GetControl(searchConfig, "txtAddBefore") as TextBox;
            Assert.IsNotNull(txtAddBefore);
            TextBox? txtAddAfter =
                CTool.GetControl(searchConfig, "txtAddAfter") as TextBox;
            Assert.IsNotNull(txtAddAfter);
            string testAddBefore = "testAddBefore";
            string testAddAfter = "testAddAfter";
            txtAddBefore.Text = testAddBefore;
            txtAddAfter.Text = testAddAfter;

            CheckBox? cbSearchList =
                CTool.GetControl(searchConfig, "cbSearchList") as CheckBox;
            Assert.IsNotNull(cbSearchList);
            bool testSearchList = true;
            cbSearchList.Checked = testSearchList;

            string repectStr = "{" 
                + "\"" + JCfgName.search + "\":" + "[" 
                + "{"
                + "\"" + JCfgName.start + "\":\"" + startString0 + "\""
                + ","
                + "\"" + JCfgName.end + "\":\"" + endString0 + "\""
                + "}"
                + ","
                + "{"
                + "\"" + JCfgName.start + "\":\"" + startString1 + "\""
                + ","
                + "\"" + JCfgName.end + "\":\"" + endString1 + "\""
                + "}"
                + "]"
                + ","
                + "\"" + JCfgName.replaces + "\":" + "["
                + "\"" + testReplace0 + "\""
                + ","
                + "\"" + testReplace1 + "\""
                + "]"
                + ","
                + "\"" + JCfgName.AddBefore + "\":\"" + testAddBefore + "\""
                + ","
                + "\"" + JCfgName.AddAfter + "\":\"" + testAddAfter + "\""
                + ","
                + "\"" + JCfgName.SearchList + "\":" 
                + testSearchList.ToString().ToLower()
                + "}";
            Assert.AreEqual(repectStr, searchConfig.JsonObj.ToJsonString());
        }

        [TestMethod]
        public void SearchConfig_JsonObj_set()
        {
            string startString0 = "startString0";
            string endString0 = "endString0";
            string startString1 = "startString1";
            string endString1 = "endString1";
            string testReplace0 = "testReplace0";
            string testReplace1 = "testReplace1";
            string testAddBefore = "testAddBefore";
            string testAddAfter = "testAddAfter";
            bool testSearchList = true;

            string repectStr = "{"
                + "\"" + JCfgName.search + "\":" + "["
                + "{"
                + "\"" + JCfgName.start + "\":\"" + startString0 + "\""
                + ","
                + "\"" + JCfgName.end + "\":\"" + endString0 + "\""
                + "}"
                + ","
                + "{"
                + "\"" + JCfgName.start + "\":\"" + startString1 + "\""
                + ","
                + "\"" + JCfgName.end + "\":\"" + endString1 + "\""
                + "}"
                + "]"
                + ","
                + "\"" + JCfgName.replaces + "\":" + "["
                + "\"" + testReplace0 + "\""
                + ","
                + "\"" + testReplace1 + "\""
                + "]"
                + ","
                + "\"" + JCfgName.AddBefore + "\":\"" + testAddBefore + "\""
                + ","
                + "\"" + JCfgName.AddAfter + "\":\"" + testAddAfter + "\""
                + ","
                + "\"" + JCfgName.SearchList + "\":"
                + testSearchList.ToString().ToLower()
                + "}";

            SearchConfig searchConfig = new SearchConfig();
            searchConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            searchConfig.JsonObj = JsonObject.Parse(repectStr) as JsonObject;

            Form testForm = new Form();
            testForm.SuspendLayout();
            testForm.Controls.Add(searchConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            Panel? panel1 =
                CTool.GetControl(searchConfig, "panel1") as Panel;
            Assert.IsNotNull(panel1);
            Assert.AreEqual(panel1.Controls.Count, 2);

            SearchLayer? searchLayer1 = panel1.Controls[0] as SearchLayer;
            SearchLayer? searchLayer0 = panel1.Controls[1] as SearchLayer;
            Assert.IsNotNull(searchLayer0);
            Assert.IsNotNull(searchLayer1);
            Assert.AreEqual(startString0, searchLayer0.Start);
            Assert.AreEqual(endString0, searchLayer0.End);
            Assert.AreEqual(startString1, searchLayer1.Start);
            Assert.AreEqual(endString1, searchLayer1.End);

            TabPage? tpFinalHandle =
                CTool.GetControl(searchConfig, "tpFinalHandle") as TabPage;
            Assert.IsNotNull(tpFinalHandle);
            TabControl? tabControl = (tpFinalHandle.Parent as TabControl);
            Assert.IsNotNull(tabControl);
            tabControl.SelectedTab = tpFinalHandle;

            ListBox? lbReplaceList =
                CTool.GetControl(searchConfig, "lbReplaceList") as ListBox;
            Assert.IsNotNull(lbReplaceList);

            Assert.AreEqual(2, lbReplaceList.Items.Count);
            Assert.AreEqual(testReplace0, lbReplaceList.Items[0].ToString());
            Assert.AreEqual(testReplace1, lbReplaceList.Items[1].ToString());

            TextBox? txtAddBefore =
                CTool.GetControl(searchConfig, "txtAddBefore") as TextBox;
            Assert.IsNotNull(txtAddBefore);
            TextBox? txtAddAfter =
                CTool.GetControl(searchConfig, "txtAddAfter") as TextBox;
            Assert.IsNotNull(txtAddAfter);
            Assert.AreEqual(testAddBefore, txtAddBefore.Text);
            Assert.AreEqual(testAddAfter, txtAddAfter.Text);

            CheckBox? cbSearchList =
                CTool.GetControl(searchConfig, "cbSearchList") as CheckBox;
            Assert.IsNotNull(cbSearchList);
            Assert.AreEqual(testSearchList, cbSearchList.Checked);
        }
    }
}
