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
            JsonObject expectedJObj = CTool.testSearchConfig_JsonObj();

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

            Panel? panel1 =
                CTool.GetControl(searchConfig, "panel1") as Panel;
            Assert.IsNotNull(panel1);
            Assert.AreEqual(panel1.Controls.Count, 1);

            int layerCount = expectedJObj[JCfgName.search].AsArray().Count;
            for (int i = 0; i < layerCount; i++)
            {
                if (i > 0)
                {
                    tbAddSearchLayer.PerformClick();
                    Assert.AreEqual(panel1.Controls.Count, i + 1);
                }

                SearchLayer? searchLayer = panel1.Controls[0] as SearchLayer;
                Assert.IsNotNull(searchLayer);
                searchLayer.JsonObj =
                    expectedJObj[JCfgName.search].AsArray()[i] as JsonObject;
            }
            Assert.AreEqual(panel1.Controls.Count, layerCount);

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


            JsonArray replaces
                = expectedJObj[JCfgName.replaces].AsArray();
            foreach (JsonValue item in replaces)
            {
                txtAddReplace.Text = item.GetValue<string>();
                tbAddReplace.PerformClick();
            }

            ListBox? lbReplaceList =
                CTool.GetControl(searchConfig, "lbReplaceList") as ListBox;
            Assert.IsNotNull(lbReplaceList);

            Assert.AreEqual(replaces.Count, lbReplaceList.Items.Count);
            Assert.AreEqual("", txtAddReplace.Text);

            TextBox? txtAddBefore =
                CTool.GetControl(searchConfig, "txtAddBefore") as TextBox;
            Assert.IsNotNull(txtAddBefore);
            TextBox? txtAddAfter =
                CTool.GetControl(searchConfig, "txtAddAfter") as TextBox;
            Assert.IsNotNull(txtAddAfter);

            txtAddBefore.Text = 
                expectedJObj[JCfgName.AddBefore].GetValue<string>();
            txtAddAfter.Text = 
                expectedJObj[JCfgName.AddAfter].GetValue<string>();

            CheckBox? cbSearchList =
                CTool.GetControl(searchConfig, "cbSearchList") as CheckBox;
            Assert.IsNotNull(cbSearchList);
            cbSearchList.Checked = 
                expectedJObj[JCfgName.SearchList].GetValue<Boolean>();

            Assert.AreEqual(expectedJObj.ToJsonString(), 
                searchConfig.JsonObj.ToJsonString());
        }

        [TestMethod]
        public void SearchConfig_JsonObj_set()
        {
            JsonObject expectedJObj = CTool.testSearchConfig_JsonObj();

            SearchConfig searchConfig = new SearchConfig();
            searchConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            searchConfig.JsonObj = expectedJObj;

            Form testForm = new Form();
            testForm.SuspendLayout();
            testForm.Controls.Add(searchConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            Panel? panel1 =
                CTool.GetControl(searchConfig, "panel1") as Panel;
            Assert.IsNotNull(panel1);
            JsonArray layers = expectedJObj[JCfgName.search].AsArray();
            Assert.AreEqual(panel1.Controls.Count, layers.Count);

            foreach(JsonObject lObj in layers)
            {
                SearchLayer? searchLayer = 
                    panel1.Controls[layers.Count - 1 - layers.IndexOf(lObj)] //reverse order
                    as SearchLayer;
                Assert.IsNotNull(searchLayer);
                Assert.AreEqual(lObj[JCfgName.start].GetValue<string>(), 
                    searchLayer.Start);
                Assert.AreEqual(lObj[JCfgName.end].GetValue<string>(), 
                    searchLayer.End);
            }

            TabPage? tpFinalHandle =
                CTool.GetControl(searchConfig, "tpFinalHandle") as TabPage;
            Assert.IsNotNull(tpFinalHandle);
            TabControl? tabControl = (tpFinalHandle.Parent as TabControl);
            Assert.IsNotNull(tabControl);
            tabControl.SelectedTab = tpFinalHandle;

            ListBox? lbReplaceList =
                CTool.GetControl(searchConfig, "lbReplaceList") as ListBox;
            Assert.IsNotNull(lbReplaceList);

            JsonArray replaces = expectedJObj[JCfgName.replaces].AsArray();
            Assert.AreEqual(replaces.Count, lbReplaceList.Items.Count);
            foreach(JsonValue item in replaces)
            {
                Assert.AreEqual(item.GetValue<string>(), 
                    lbReplaceList.Items[replaces.IndexOf(item)].ToString());
            }

            TextBox? txtAddBefore =
                CTool.GetControl(searchConfig, "txtAddBefore") as TextBox;
            Assert.IsNotNull(txtAddBefore);
            TextBox? txtAddAfter =
                CTool.GetControl(searchConfig, "txtAddAfter") as TextBox;
            Assert.IsNotNull(txtAddAfter);
            Assert.AreEqual(expectedJObj[JCfgName.AddBefore].GetValue<string>(), 
                txtAddBefore.Text);
            Assert.AreEqual(expectedJObj[JCfgName.AddAfter].GetValue<string>(),
                txtAddAfter.Text);

            CheckBox? cbSearchList =
                CTool.GetControl(searchConfig, "cbSearchList") as CheckBox;
            Assert.IsNotNull(cbSearchList);
            Assert.AreEqual(expectedJObj[JCfgName.SearchList].GetValue<Boolean>(),
                cbSearchList.Checked);
        }
    }
}
