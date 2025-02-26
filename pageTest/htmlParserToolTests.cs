using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xy.scraper.page;

namespace pageTest
{
    [TestClass]
    public class htmlParserToolTests
    {
        #region findIndexBetween
        [TestMethod]
        public void findIndexBetween_xSrEx()
        {
            string strForFind = "otherStartResultEedother";
            string startStr = "Start";
            string endStr = "Eed";
            (int, int) expected = (10, 16);
            (int, int) actual = htmlParserTool.findIndexBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_xSrEx_indexPreS()
        {
            string strForFind = "otherStartResultEedother";
            string startStr = "Start";
            string endStr = "Eed";
            int startFindIndex = 3;
            (int, int) expected = (10, 16);
            (int, int) actual = htmlParserTool.findIndexBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected, actual);

            startFindIndex = 5;
            actual = htmlParserTool.findIndexBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_xSrEx_indexPasS()
        {
            string strForFind = "otherStartResultEedother";
            string startStr = "Start";
            string endStr = "Eed";
            int startFindIndex = 6;
            (int, int) expected = (-1, -1);
            (int, int) actual = htmlParserTool.findIndexBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected, actual);

            startFindIndex = 10;
            actual = htmlParserTool.findIndexBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_rEx()
        {
            string strForFind = "ResultEedother";
            string startStr = "";
            string endStr = "Eed";
            (int, int) expected = (0, 6);
            (int, int) actual = htmlParserTool.findIndexBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_xSr()
        {
            string strForFind = "otherStartResult";
            string startStr = "Start";
            string endStr = "";
            (int, int) expected = (10, 16);
            (int, int) actual = htmlParserTool.findIndexBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_xSSSrEx_multiStart()
        {
            string strForFind = "otherStartStartStartResultEedother";
            string startStr = "Start";
            string endStr = "Eed";
            (int, int) expected = (20, 26);
            (int, int) actual = htmlParserTool.findIndexBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_xSrExSrEx_findFisrt()
        {
            string strForFind = "otherStartResultEedotherStartResultEed";
            string startStr = "Start";
            string endStr = "Eed";
            (int, int) expected = (10, 16);
            (int, int) actual = htmlParserTool.findIndexBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_xErSx()
        {
            string strForFind = "otherEedResultStartother";
            string startStr = "Start";
            string endStr = "Eed";
            (int, int) expected = (-1, -1);
            (int, int) actual = htmlParserTool.findIndexBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_0LengthString()
        {
            string strForFind = "";
            string startStr = "Start";
            string endStr = "Eed";
            (int, int) expected = (-1, -1);
            (int, int) actual = htmlParserTool.findIndexBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findIndexBetween_null()
        {
            string? strForFind = null;
            string startStr = "Start";
            string endStr = "Eed";
            Assert.ThrowsException<NullReferenceException>(
                () => htmlParserTool.findIndexBetween(strForFind, startStr, endStr)
                );
        }
        #endregion

        #region findBetween
        [TestMethod]
        public void findBetween_xSrEx()
        {
            string strForFind = "otherStartResultEedother";
            string startStr = "Start";
            string endStr = "Eed";
            string expected = "Result";
            string actual = htmlParserTool.findBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_xSrEx_indexPreS()
        {
            string strForFind = "otherStartResultEedother";
            string startStr = "Start";
            string endStr = "Eed";
            int startFindIndex = 3;
            string expected = "Result";
            string actual = htmlParserTool.findBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected, actual);

            startFindIndex = 5;
            actual = htmlParserTool.findBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_xSrEx_indexPasS()
        {
            string strForFind = "otherStartResultEedother";
            string startStr = "Start";
            string endStr = "Eed";
            int startFindIndex = 6;
            string? expected = null;
            string actual = htmlParserTool.findBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected, actual);

            startFindIndex = 10;
            actual = htmlParserTool.findBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_rEx()
        {
            string strForFind = "ResultEedother";
            string startStr = "";
            string endStr = "Eed";
            string expected = "Result";
            string actual = htmlParserTool.findBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_xSr()
        {
            string strForFind = "otherStartResult";
            string startStr = "Start";
            string endStr = "";
            string expected = "Result";
            string actual = htmlParserTool.findBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_xSSSrEx_multiStart()
        {
            string strForFind = "otherStartStartStartResultEedother";
            string startStr = "Start";
            string endStr = "Eed";
            string expected = "Result";
            string actual = htmlParserTool.findBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_xSrExSrEx_findFisrt()
        {
            string strForFind = "otherStartResultEedotherStartResultEed";
            string startStr = "Start";
            string endStr = "Eed";
            string expected = "Result";
            string actual = htmlParserTool.findBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_xErSx()
        {
            string strForFind = "otherEedResultStartother";
            string startStr = "Start";
            string endStr = "Eed";
            string? expected = null;
            string actual = htmlParserTool.findBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_0LengthString()
        {
            string strForFind = "";
            string startStr = "Start";
            string endStr = "Eed";
            string? expected = null;
            string actual = htmlParserTool.findBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void findBetween_null()
        {
            string? strForFind = null;
            string startStr = "Start";
            string endStr = "Eed";
            Assert.ThrowsException<NullReferenceException>(
                () => htmlParserTool.findBetween(strForFind, startStr, endStr)
                );
        }
        #endregion

        #region findAllBetween
        [TestMethod]
        public void findAllBetween_xSrExSrExSrEx()
        {
            string strForFind = "otherStartResult1EedotherStartResult2EedotherStartResult3Eedother";
            string startStr = "Start";
            string endStr = "Eed";
            List<string> expected = new List<string>() { "Result1", "Result2", "Result3" };
            List<string> actual = htmlParserTool.findAllBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void findAllBetween_xSrExSrExSrEx_indexPreS()
        {
            string strForFind = "otherStartResult1EedotherStartResult2EedotherStartResult3Eedother";
            string startStr = "Start";
            string endStr = "Eed";
            int startFindIndex = 3;
            List<string> expected = new List<string>() { "Result1", "Result2", "Result3" };
            List<string> actual = htmlParserTool.findAllBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }

            startFindIndex = 5;
            actual = htmlParserTool.findAllBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void findAllBetween_xSrExSrExSrEx_indexPasS()
        {
            string strForFind = "otherStartResult1EedotherStartResult2EedotherStartResult3Eedother";
            string startStr = "Start";
            string endStr = "Eed";
            int startFindIndex = 6;
            List<string> expected = new List<string>() { "Result2", "Result3" };
            List<string> actual = htmlParserTool.findAllBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }

            startFindIndex = 10;
            actual = htmlParserTool.findAllBetween(
                strForFind, startStr, endStr, startFindIndex);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void findAllBetween_xSrExSrExSr()
        {
            string strForFind = "otherStartResult1EedotherStartResult2EedotherStartResult3";
            string startStr = "Start";
            string endStr = "Eed";
            List<string> expected = new List<string>() { "Result1", "Result2" };
            List<string> actual = htmlParserTool.findAllBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void findAllBetween_rExSrExSrEx()
        {
            string strForFind = "Result1EedotherStartResult2EedotherStartResult3Eedother";
            string startStr = "Start";
            string endStr = "Eed";
            List<string> expected = new List<string>() { "Result2", "Result3" };
            List<string> actual = htmlParserTool.findAllBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void findAllBetween_rExSrExSr()
        {
            string strForFind = "Result1EedotherStartResult2EedotherStartResult3";
            string startStr = "Start";
            string endStr = "Eed";
            List<string> expected = new List<string>() { "Result2" };
            List<string> actual = htmlParserTool.findAllBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void findAllBetween_0LengthString()
        {
            string strForFind = "";
            string startStr = "Start";
            string endStr = "Eed";
            List<string> expected = new List<string>();
            List<string> actual = htmlParserTool.findAllBetween(strForFind, startStr, endStr);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void findAllBetween_null()
        {
            string? strForFind = null;
            string startStr = "Start";
            string endStr = "Eed";
            Assert.ThrowsException<NullReferenceException>(
                () => htmlParserTool.findAllBetween(strForFind, startStr, endStr)
                );
        }
        #endregion

        #region washPathStr
        [TestMethod]
        public void washPathStr_xSrEx()
        {
            string PathStr = "";
            string expected = "";
            List<string> illegalChrs = new List<string>{
            "&nbsp;",
            "amp;",
            "#",
            "%",
            "&",
            "{",
            "}",
            "\\",
            "<",
            ">",
            "*",
            "?",
            "/",
            "$",
            "!",
            "\"",
            ":",
            ";",
            "@",
            "+",
            "`",
            "|",
            "=",
            " " };

            foreach (string illegalChr in illegalChrs)
            {
                PathStr += "abc" + illegalChr + "def";
                expected += "abcdef";
            }

            string actual = htmlParserTool.washPathStr(PathStr);
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
