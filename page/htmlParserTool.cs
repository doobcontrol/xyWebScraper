using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace xy.scraper.page
{
    public class htmlParserTool
    {
        //find first one
        static public (int, int) findIndexBetween(
            string strForFind,
            string startStr,
            string endStr,
            int startFindIndex = 0)
        {
            int start_index = startFindIndex;
            int end_index = strForFind.Length;

            if (end_index <= start_index)
            {
                if (end_index == -1)
                {
                    return (-1, -1);
                }
            }

            if (startStr.Length != 0)
            {
                start_index = strForFind.IndexOf(startStr, startFindIndex);
                if (start_index == -1)
                {
                    return (-1, -1);
                }
                start_index += startStr.Length;
            }
            if (endStr.Length != 0)
            {
                end_index = strForFind.IndexOf(endStr, start_index);
                if (end_index == -1)
                {
                    return (-1, -1);
                }
            }

            if (end_index <= start_index)
            {
                return (-1, -1);
            }

            // check if substring between start_index and end_index contains startStr
            if (startStr.Length != 0 && end_index > start_index)
            {
                int start_indexIn =
                    strForFind.LastIndexOf(startStr, end_index - endStr.Length, end_index - start_index);
                if (start_indexIn != -1)
                {
                    start_index = start_indexIn + startStr.Length;
                }
            }

            return (start_index, end_index);
        }

        //find first one
        static public string findBetween(
            string strForFind,
            string startStr,
            string endStr,
            int startFindIndex = 0)
        {
            (int, int) indexTuple = findIndexBetween(
                strForFind,
                startStr,
                endStr,
                startFindIndex);

            int startIndex = indexTuple.Item1;
            int endIndex = indexTuple.Item2;
            string retStr = null;
            if (startIndex != -1)
            {
                retStr = strForFind.Substring(startIndex, endIndex - startIndex);
            }
            return retStr;
        }

        //find all
        static public List<string> findAllBetween(
            string strForFind,
            string startStr,
            string endStr,
            int startFindIndex = 0)
        {
            List<string> retList = new List<string>();

            int beginIndex = startFindIndex;
            int endStrLen = endStr.Length;

            while (true)
            {
                (int, int) indexTuple = findIndexBetween(
                    strForFind,
                    startStr,
                    endStr,
                    beginIndex);

                int retStartIndex = indexTuple.Item1;
                int retEndIndex = indexTuple.Item2;

                if (retStartIndex == -1)
                {
                    break;
                }
                else
                {
                    retList.Add(
                        strForFind.Substring(retStartIndex, retEndIndex - retStartIndex)

                        );
                    beginIndex = retEndIndex + endStrLen;
                }
            }

            return retList;
        }

        static List<string> illegalChrs = new List<string>{
            "&nbsp;", 
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
            "@", 
            "+", 
            "`", 
            "|", 
            "=",
            "amp;" };

        static public string washPathStr(string pathStr)
        {
            string retStr = pathStr;

            foreach(string illegalChr in illegalChrs)
            {
                retStr = retStr.Replace(illegalChr, "");
            }

            return retStr;
        }

    }
}
