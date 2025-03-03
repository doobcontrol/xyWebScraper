using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using xy.scraper.configControl;
using xy.scraper.page.parserConfig;

namespace configControlTest
{
    [TestClass]
    public class SearchLayerTests
    {
        [TestMethod]
        public void SearchLayer_MakeControlSelectable()
        {
            SearchLayer searchLayer1 = new SearchLayer();
            searchLayer1.Dock = System.Windows.Forms.DockStyle.Top;
            searchLayer1.Start = "startString";
            searchLayer1.End = "endString";

            Button button = new Button();
            button.Dock = System.Windows.Forms.DockStyle.Bottom;
            button.Text = "testButton";

            Color selectedBackColor = Color.LightBlue;

            Form testForm = new Form();
            testForm.Controls.Add(searchLayer1);
            testForm.Controls.Add(button);
            testForm.Show();

            Panel? panel1 = searchLayer1.Controls["panel1"] as Panel;
            if (panel1 != null)
            {
                TextBox? txtStart1 = searchLayer1.Controls["txtStart"] as TextBox;
                if (txtStart1 != null)
                {
                    Assert.IsTrue(txtStart1.Focused);
                    Assert.AreEqual(searchLayer1.BorderStyle, BorderStyle.Fixed3D);
                    Assert.AreEqual(panel1.BackColor, selectedBackColor);

                    Thread.Sleep(1);
                    button.Focus();

                    Assert.IsFalse(txtStart1.Focused);
                    Assert.AreNotEqual(searchLayer1.BorderStyle, BorderStyle.Fixed3D);
                    Assert.AreNotEqual(panel1.BackColor, selectedBackColor);

                    Thread.Sleep(1);
                    searchLayer1.Focus();

                    Assert.IsTrue(txtStart1.Focused);
                    Assert.AreEqual(searchLayer1.BorderStyle, BorderStyle.Fixed3D);
                    Assert.AreEqual(panel1.BackColor, selectedBackColor);

                    Thread.Sleep(1);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SearchLayer_JsonObj_get()
        {
            JsonObject expectedJObj = CTool.testSearchLayer_JsonObj();

            SearchLayer searchLayer1 = new SearchLayer();
            searchLayer1.Dock = System.Windows.Forms.DockStyle.Top;
            string initStart = CTool.randomString();
            string initEnd = CTool.randomString();
            searchLayer1.Start = initStart;
            searchLayer1.End = initEnd;

            Form testForm = new Form();
            testForm.Controls.Add(searchLayer1);
            testForm.Show();

            TextBox? txtStart = searchLayer1.Controls["txtStart"] as TextBox;
            Assert.IsNotNull(txtStart);
            TextBox? txtEnd = searchLayer1.Controls["txtEnd"] as TextBox;
            Assert.IsNotNull(txtEnd);
            //Assert perporties(Start, End) set
            Assert.AreEqual(initStart, txtStart.Text);
            Assert.AreEqual(initEnd, txtEnd.Text);

            txtStart.Text = expectedJObj[JCfgName.start].GetValue<string>();
            txtEnd.Text = expectedJObj[JCfgName.end].GetValue<string>();

            //Assert perporties(Start, End) get
            Assert.AreEqual(expectedJObj[JCfgName.start].GetValue<string>(),
                searchLayer1.Start);
            Assert.AreEqual(expectedJObj[JCfgName.end].GetValue<string>(),
                searchLayer1.End);

            //Assert perportie JsonObj get
            JsonObject actualJObj = searchLayer1.JsonObj;
            Assert.IsNotNull(actualJObj);
            Assert.AreEqual(expectedJObj.ToJsonString(), actualJObj.ToJsonString());
        }

        [TestMethod]
        public void SearchLayer_JsonObj_set()
        {
            JsonObject jObj = CTool.testSearchLayer_JsonObj();

            SearchLayer searchLayer1 = new SearchLayer();
            searchLayer1.JsonObj = jObj;
            searchLayer1.Dock = System.Windows.Forms.DockStyle.Top;

            Form testForm = new Form();
            testForm.Controls.Add(searchLayer1);
            testForm.Show();


            TextBox? txtStart = searchLayer1.Controls["txtStart"] as TextBox;
            if (txtStart != null)
            {
                Assert.AreEqual(txtStart.Text, 
                    jObj[JCfgName.start].GetValue<string>());
            }
            else
            {
                Assert.Fail();
            }
            TextBox? txtEnd = searchLayer1.Controls["txtEnd"] as TextBox;
            if (txtEnd != null)
            {
                Assert.AreEqual(txtEnd.Text, 
                    jObj[JCfgName.end].GetValue<string>());
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
