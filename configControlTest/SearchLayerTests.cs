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
            SearchLayer searchLayer1 = new SearchLayer();
            searchLayer1.Dock = System.Windows.Forms.DockStyle.Top;
            searchLayer1.Start = "startString";
            searchLayer1.End = "endString";

            Form testForm = new Form();
            testForm.Controls.Add(searchLayer1);
            testForm.Show();

            JsonObject jObj = searchLayer1.JsonObj;
            Assert.IsNotNull(jObj);
            Assert.AreEqual(jObj["start"].ToString(), "startString");
            Assert.AreEqual(jObj["end"].ToString(), "endString");
            Assert.AreEqual(jObj.ToJsonString(), 
                "{\"start\":\"startString\",\"end\":\"endString\"}");

        }

        [TestMethod]
        public void SearchLayer_JsonObj_set()
        {
            JsonObject jObj =
                JsonSerializer.Deserialize<JsonObject>
                ("{\"start\":\"startString\",\"end\":\"endString\"}");

            SearchLayer searchLayer1 = new SearchLayer();
            searchLayer1.JsonObj = jObj;
            searchLayer1.Dock = System.Windows.Forms.DockStyle.Top;

            Form testForm = new Form();
            testForm.Controls.Add(searchLayer1);
            testForm.Show();


            TextBox? txtStart = searchLayer1.Controls["txtStart"] as TextBox;
            if (txtStart != null)
            {
                Assert.AreEqual(txtStart.Text, "startString");
            }
            else
            {
                Assert.Fail();
            }
            TextBox? txtEnd = searchLayer1.Controls["txtEnd"] as TextBox;
            if (txtEnd != null)
            {
                Assert.AreEqual(txtEnd.Text, "endString");
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
