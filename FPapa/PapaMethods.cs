using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    /// <summary>
    /// Methods for Papa
    /// </summary>
    partial class Papa
    {

        /// <summary>
        /// Vector to Hash. heder - keys
        /// </summary>
        /// <param name="header"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Dictionary<string, string> VecToHash(string[] header, string[] vec)
        {
            #region #VecToHash
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < header.Length; i++)
            {
                try { dict[header[i]] = vec[i]; }
                catch { Alarm("Err VecToHash", String.Format(" column {0}", i)); }
            }
            return dict;
            #endregion
        }

        /// <summary>
        /// Dict to List/ Only Values
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static List<string> DictToList(Dictionary<string, string> dict)
        {
            #region #DictToList
            List<string> rez = new List<string>();
            foreach (string unit in dict.Values)
            {
                rez.Add(unit);
            }
            return rez;
            #endregion
        }

        /// <summary>
        /// Only keys
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static List<string> DictToHead(Dictionary<string, string> dict)
        {
            #region #DictToHead
            List<string> rez = new List<string>();
            foreach (string unit in dict.Keys)
            {
                try { rez.Add(unit); }
                catch { Sos("Err DictToHead", unit); }
            }
            return rez;
            #endregion
        }

        /// <summary>
        /// Array to Hash
        /// </summary>
        /// <param name="head"></param>
        /// <param name="list"></param>
        /// <param name="keyColNum"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> ArrToHash(string[] head, List<string[]> list, int keyColNum)
        {
            #region #ArrToHash
            Dictionary<string, Dictionary<string, string>> hashTab = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> hash = new Dictionary<string, string>();
            foreach (string[] line in list)
            {
                try
                {
                    hash = VecToHash(head, line);
                    string keyLine = head[keyColNum];
                    string keyArr = hash[keyLine];
                    hashTab[keyArr] = hash;
                }
                catch { Sos("Err ArrToHash", line[0]); }
            }
            return hashTab;
            #endregion
        }

        /// <summary>
        /// SubList
        /// </summary>
        /// <param name="list"></param>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public static List<string[]> SubList(List<string[]> list, int start, int finish)
        {
            #region
            List<string[]> myList = new List<string[]>();
            for (int i = start; i <= finish; i++)
            {
                try { myList.Add(list[i]); }
                catch { Sos("Err Sublist", String.Format(" row {0}", i)); }
            }
            return myList;
            #endregion
        }

        /// <summary>
        /// Date for file name - summuru departments- agents
        /// </summary>
        /// <returns></returns>
        public static string DateNowLine() { return (DateTime.Today).ToString("yyyy-MM-dd").Replace(".", ""); }


        public static string DateNowDots() { return (DateTime.Today).ToString("yyyy-MM-dd"); }

        public static string GoodText(string s) { return s.Replace("'", "`"); }

        public static List<string> GoodVec(List<string> v)
        {
            for (int i = 0; i < v.Count; i++)
            {
                v[i] = v[i].Replace("'", "`");
            }
            return v;
        }

        public static string[] GoodVec(string[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = v[i].Replace("'", "`");
            }
            return v;
        }
    }
}
