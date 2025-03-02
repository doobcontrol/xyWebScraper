using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using xy.scraper.configControl;

namespace configControlTest
{
    [TestClass]
    public class SearchConfigTests
    {
        public Control? GetControl(Control control, string name)
        {
            Control? retControl = null;
            foreach (Control c in control.Controls)
            {
                if (c.Name == name)
                {
                    retControl = c;
                    break;
                }
                else
                {
                    Control? subControl = GetControl(c, name);
                    if (subControl != null)
                    {
                        retControl = subControl;
                        break;
                    }
                }
            }
            return retControl;
        }

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
                GetControl(searchConfig, "toolStrip1") as ToolStrip;

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
                    GetControl(searchConfig, "panel1") as Panel;
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

            Color selectedBackColor = Color.LightBlue;

            Form testForm = new Form();
            testForm.SuspendLayout();
            testForm.Controls.Add(searchConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            ToolStrip? toolStrip2 =
                GetControl(searchConfig, "toolStrip2") as ToolStrip;

            Assert.IsNotNull(toolStrip2);

            TabPage? tpFinalHandle = 
                GetControl(searchConfig, "tpFinalHandle") as TabPage;
            Assert.IsNotNull(tpFinalHandle);
            ToolStripButton? tbAddReplace =
                toolStrip2.Items["tbAddReplace"] as ToolStripButton;
            Assert.IsNotNull(tbAddReplace);
            ToolStripButton? tbDelReplace =
                toolStrip2.Items["tbDelReplace"] as ToolStripButton;
            Assert.IsNotNull(tbDelReplace);
            TextBox? txtAddReplace =
                GetControl(searchConfig, "txtAddReplace") as TextBox;
            Assert.IsNotNull(txtAddReplace);

            TabControl? tabControl = (tpFinalHandle.Parent as TabControl);
            Assert.IsNotNull(tabControl);
            tabControl.SelectedTab = tpFinalHandle;

            string testStr = "testabc";
            txtAddReplace.Text = testStr;
            tbAddReplace.PerformClick();

            ListBox? lbReplaceList =
                GetControl(searchConfig, "lbReplaceList") as ListBox;
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
    }
}
