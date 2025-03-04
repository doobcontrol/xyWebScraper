using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using xy.scraper.configControl;

namespace configControlTest
{
    [TestClass]
    public class SearchTestTests
    {
        [TestMethod]
        public void SearchTest_InitStatus()
        {
            checkInitStatus();
        }
        [TestMethod]
        public void SearchTest_GetHtml()
        {
            GetHtml();
        }
        [TestMethod]
        public void SearchTest_Test()
        {
            (SearchTest searchTest,
            (string start, string end, string searchStr, List<string> resultStrs) 
            searchTestData)
            searchTestDataSet = GetHtml();
            SearchTest searchTest = searchTestDataSet.searchTest;
            searchTest.SearchJsonObj = 
                new SearchTest.SearchJsonObject(() => {
                    return CTool.testSearchConfig_JsonObj(
                        new List<JsonObject>() {
                        CTool.testSearchLayer_JsonObj(
                            searchTestDataSet.searchTestData.start,
                            searchTestDataSet.searchTestData.end
                        )}
                        );
                });

            TabPage? tpTest =
                CTool.GetControl(searchTest, "tpTest") as TabPage;
            Assert.IsNotNull(tpTest); 
            TabControl? tabControl1 =
                CTool.GetControl(searchTest, "tabControl1") as TabControl;
            Assert.IsNotNull(tabControl1);
            tabControl1.SelectedTab = tpTest;
            TextBox? txtShowBox =
                CTool.GetControl(searchTest, "txtShowBox") as TextBox;
            Assert.IsNotNull(txtShowBox);
            Button? btnSearch =
                CTool.GetControl(searchTest, "btnSearch") as Button;
            Assert.IsNotNull(btnSearch);

            btnSearch.PerformClick();
            Assert.AreEqual(
                "found "
                + searchTestDataSet.searchTestData.resultStrs.Count
                + " items:\r\n"
                + string.Join("\r\n", searchTestDataSet.searchTestData.resultStrs)
                ,
                txtShowBox.Text
                );
        }

        private SearchTest checkInitStatus()
        {
            SearchTest searchTest = new SearchTest();
            searchTest.Dock = System.Windows.Forms.DockStyle.Fill;

            Form testForm = new Form();
            testForm.Width *= 2;
            testForm.Height *= 2;
            testForm.SuspendLayout();
            testForm.Controls.Add(searchTest);
            testForm.ResumeLayout(false);
            testForm.Show();

            TabControl? tabControl1 =
                CTool.GetControl(searchTest, "tabControl1") as TabControl;
            Assert.IsNotNull(tabControl1);
            TabPage? tpHtml =
                CTool.GetControl(searchTest, "tpHtml") as TabPage;
            Assert.IsNotNull(tpHtml);
            TextBox? txtHtml =
                CTool.GetControl(searchTest, "txtHtml") as TextBox;
            Assert.IsNotNull(txtHtml);
            TextBox? txtUrl =
                CTool.GetControl(searchTest, "txtUrl") as TextBox;
            Assert.IsNotNull(txtUrl);
            Button? btnGetHtml =
                CTool.GetControl(searchTest, "btnGetHtml") as Button;
            Assert.IsNotNull(btnGetHtml);
            Label? lbGetting =
                CTool.GetControl(searchTest, "lbGetting") as Label;
            Assert.IsNotNull(lbGetting);
            Assert.IsTrue(!lbGetting.Visible);

            TabPage? tpTest =
                CTool.GetControl(searchTest, "tpTest") as TabPage;
            Assert.IsNull(tpTest);
            Button? btnSearchtpTest =
                CTool.GetControl(searchTest, "btnSearchtpTest") as Button;
            Assert.IsNull(btnSearchtpTest);
            TextBox? txtShowBox =
                CTool.GetControl(searchTest, "txtShowBox") as TextBox;
            Assert.IsNull(txtShowBox);
            Button? btnSearch =
                CTool.GetControl(searchTest, "btnSearch") as Button;
            Assert.IsNull(btnSearch);

            return searchTest;
        }
        private (SearchTest searchTest, 
            (string start, string end, string searchStr, List<string> resultStrs) 
            searchTestData)  
            GetHtml()
        {
            SearchTest searchTest = checkInitStatus();
            searchTest.GetHtmlStringHandler =
                new SearchTest.GetHtmlString(GetHtmlString);

            Button? btnGetHtml =
                CTool.GetControl(searchTest, "btnGetHtml") as Button;
            Assert.IsNotNull(btnGetHtml);

            string testUrl = "SearchTest_GetHtml";
            TextBox? txtUrl =
                CTool.GetControl(searchTest, "txtUrl") as TextBox;
            Assert.IsNotNull(txtUrl);
            txtUrl.Text = testUrl;

            (string start, string end, string searchStr, List<string> resultStrs) 
                searchTestData = CTool.generateSearchListTestData();
            testHtmlDic.Add(testUrl, searchTestData.searchStr);
            btnGetHtml.PerformClick();

            TextBox? txtHtml =
                CTool.GetControl(searchTest, "txtHtml") as TextBox;
            Assert.IsNotNull(txtHtml);
            Assert.AreEqual(testHtmlDic[testUrl], txtHtml.Text);

            TabPage? tpTest =
                CTool.GetControl(searchTest, "tpTest") as TabPage;
            Assert.IsNotNull(tpTest);
            Button? btnSearch =
                CTool.GetControl(searchTest, "btnSearch") as Button;
            Assert.IsNotNull(btnSearch);
            TextBox? txtShowBox =
                CTool.GetControl(searchTest, "txtShowBox") as TextBox;
            Assert.IsNotNull(txtShowBox);

            return (searchTest, searchTestData);
        }

        Dictionary<string, string> testHtmlDic = new Dictionary<string, string>();
        private async Task<string> GetHtmlString(string Url)
        {
            Thread.Sleep(1000); //use await Task.delay(1000); wiill never return??
            return testHtmlDic[Url];
        }
    }
}
