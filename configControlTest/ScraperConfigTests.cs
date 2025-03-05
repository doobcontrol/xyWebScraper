using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xy.scraper.configControl;

namespace configControlTest
{
    [TestClass]
    public class ScraperConfigTests
    {
        public static Control? GetControl(Control control, string name)
        {
            List<Type> exclusiveInTypes = new List<Type>() { 
                typeof(PageConfig),
                typeof(SearchTest)
            };
            return CTool.GetControl(control, name, exclusiveInTypes);
        }

        [TestMethod]
        public void ScraperConfig_Page_Add_Del()
        {
            ScraperConfig scraperConfig = new ScraperConfig();
            scraperConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.Width *= 3;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(scraperConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            ToolStrip? toolStrip1 =
                GetControl(scraperConfig, "toolStrip1") as ToolStrip;
            Assert.IsNotNull(toolStrip1);
            ToolStripButton? tbAddPageConfig =
                toolStrip1.Items["tbAddPageConfig"] as ToolStripButton;
            Assert.IsNotNull(tbAddPageConfig);
            ToolStripButton? tbDelPageConfig =
                toolStrip1.Items["tbDelPageConfig"] as ToolStripButton;
            Assert.IsNotNull(tbDelPageConfig);

            TabControl? tabControl1 =
                GetControl(scraperConfig, "tabControl1") as TabControl;
            Assert.IsNotNull(tabControl1);

            int pageCount = 1;
            string page1Name = "pageModel";// tabControl1.Controls[0].Text;

            Assert.IsNotNull(tabControl1);
            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(pageCount - 1, tabControl1.SelectedIndex);
            Assert.AreEqual(page1Name + pageCount, tabControl1.SelectedTab.Text);

            //add one path
            tbAddPageConfig.PerformClick();
            pageCount++;
            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(pageCount - 1, tabControl1.SelectedIndex);
            Assert.AreEqual(page1Name + (pageCount + 1), tabControl1.SelectedTab.Text);

            //add four paths
            tbAddPageConfig.PerformClick();
            pageCount++;
            tbAddPageConfig.PerformClick();
            pageCount++;
            tbAddPageConfig.PerformClick();
            pageCount++;
            tbAddPageConfig.PerformClick();
            pageCount++;
            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(pageCount - 1, tabControl1.SelectedIndex);
            Assert.AreEqual(page1Name + (pageCount + 1), tabControl1.SelectedTab.Text);

            //delete one path(the last one)
            tbDelPageConfig.PerformClick();
            pageCount--;
            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(pageCount - 1, tabControl1.SelectedIndex);

            //delete one path(the first one, failure)
            tabControl1.SelectedIndex = 0;
            tbDelPageConfig.PerformClick();
            tbDelPageConfig.PerformClick();
            tbDelPageConfig.PerformClick();
            tbDelPageConfig.PerformClick();
            tbDelPageConfig.PerformClick();
            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(0, tabControl1.SelectedIndex);

            //select a path, and delete
            tabControl1.SelectedIndex = 1;
            tbDelPageConfig.PerformClick();
            pageCount--;
            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(0, tabControl1.SelectedIndex);

            //delete all paths other and the first one
            for (int i = 1; i < pageCount; i++)
            {
                tabControl1.SelectedIndex = tabControl1.Controls.Count - 1;
                tbDelPageConfig.PerformClick();
            }
            pageCount = 1;
            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(0, tabControl1.SelectedIndex);
        }

        [TestMethod]
        public void ScraperConfig_Page_Copy()
        {
            ScraperConfig scraperConfig = new ScraperConfig();
            scraperConfig.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.Width *= 3;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(scraperConfig);
            testForm.ResumeLayout(false);
            testForm.Show();

            ToolStrip? toolStrip1 =
                GetControl(scraperConfig, "toolStrip1") as ToolStrip;
            Assert.IsNotNull(toolStrip1);
            ToolStripButton? tbCopyPageConfig =
                toolStrip1.Items["tbCopyPageConfig"] as ToolStripButton;
            Assert.IsNotNull(tbCopyPageConfig);

            TabControl? tabControl1 =
                GetControl(scraperConfig, "tabControl1") as TabControl;
            Assert.IsNotNull(tabControl1);

            int pageCount = 1;
            string page1Name = "pageModel";// tabControl1.Controls[0].Text;

            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(pageCount - 1, tabControl1.SelectedIndex);
            Assert.AreEqual(page1Name + pageCount, tabControl1.SelectedTab.Text);

            PageConfig? expectPageConfig =
                tabControl1.SelectedTab.Controls[0] as PageConfig;
            Assert.IsNotNull(expectPageConfig);
            string PageID = CTool.randomString();
            string Encoding = CTool.randomString();
            expectPageConfig.PageID = PageID;
            expectPageConfig.Encoding = Encoding;
            Assert.AreEqual(PageID, tabControl1.SelectedTab.Text);

            //copy one path
            tbCopyPageConfig.PerformClick();
            pageCount++;
            Assert.AreEqual(pageCount, tabControl1.Controls.Count);
            Assert.AreEqual(pageCount - 1, tabControl1.SelectedIndex);
            Assert.AreEqual(PageID, tabControl1.SelectedTab.Text);

            PageConfig? actualPageConfig =
                tabControl1.TabPages[pageCount - 1].Controls[0] as PageConfig;
            Assert.IsNotNull(actualPageConfig);

            Assert.AreEqual(expectPageConfig.JsonObj.ToJsonString(),
                actualPageConfig.JsonObj.ToJsonString());
        }
    }
}
