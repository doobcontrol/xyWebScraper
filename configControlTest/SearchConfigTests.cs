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

            Color selectedBackColor = Color.LightBlue;

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
    }
}
