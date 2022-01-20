using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    /// <summary>
    /// Comon Methods for Priem, Otpusk. Perevod
    /// </summary>
    class PeoplePapa : Papa
    {
        protected static int colFioOne;
        protected static int colFioTwo;
        protected static int colDepOne;
        protected static int colDepTwo;
        protected static int colLoginTwo;

        protected static bool loginOk;
        protected static List<string> fierd;
        protected static List<string[]> myKassAll;
        protected static string[] MkSplitname() { return workVec[colFioOne].Replace("  ", " ").Split(' ', StringSplitOptions.RemoveEmptyEntries); }
        protected static string MkSurname() { return MkSplitname()[0]; }
        protected static string MkFirstName() { return MkSplitname()[1]; }

        /// <summary>
        /// MkLastName
        /// </summary>
        /// <returns></returns>
        protected static string MkLastName()
        {
            #region #MkLastName
            string name;
            string[] splitName = MkSplitname();
            if (splitName.Length < 3) name = splitName[1];
            else name = splitName[2];
            return name;
            #endregion
        }

        protected static string MkInitialOneDot() { return MkFirstName().Substring(0, 1) + "."; }
        protected static string MkInitialTwoDot() { return MkLastName().Substring(0, 1) + "."; }

        #region #MkDepartment
        protected static string MkDepartment()
        {
            string dep = workVec[colDepOne];
            if (dep.IndexOf('№') > -1) dep = dep.Replace("  ", " ").Split('№')[1].Replace(" ", "");
            return dep;
            #endregion
        }

        /// <summary>
        /// LoginSearchDeep
        /// </summary>
        /// <param name="parDep"></param>
        /// <param name="nama"></param>
        /// <returns></returns>
        protected static List<string> LoginSearchDeep(string parDep, string nama)
        {
            #region #LoginSearchDeep
            loginOk = false;
            parDep = parDep.Substring(0, 3);
            List<string> logins = new List<string>();
            int count = 0;
            foreach (string[] kassAllLine in myKassAll)
            {
                string signDepKass = "";
                try { signDepKass = kassAllLine[colDepTwo].Substring(1, 4); }
                catch { continue; }

                if ((signDepKass.IndexOf(parDep) > -1) && (kassAllLine[colFioTwo].IndexOf(nama) > -1))
                {
                    logins.Add(kassAllLine[colLoginTwo]);
                    count += 1;
                    if (count > 1) break;
                }
            }

            if (count == 1)
            {
                loginOk = true;
                return logins;
            }
            return null;
            #endregion
        }

        /// <summary>
        /// LoginSearch
        /// </summary>
        /// <param name="parDep"></param>
        /// <returns></returns>
        protected static List<string> LoginSearch(string parDep)
        {
            #region
            loginOk = false;
            string nama = MkFioWhite(workVec[colFioOne]);
            List<string> logins = new List<string>();
            foreach (string[] kassAllLine in myKassAll)
            {
                if ((kassAllLine[colDepTwo].IndexOf(parDep) > -1) && (kassAllLine[colFioTwo].IndexOf(nama) > -1))
                {
                    loginOk = true;
                    logins.Add(kassAllLine[colLoginTwo]);
                }

            }
            if (loginOk)
                return logins;

            return LoginSearchDeep(parDep, nama);
            #endregion
        }

        /// <summary>
        /// LoginSearchHas Garbige
        /// </summary>
        /// <returns></returns>
        #region #Garboge
        /*		
                protected static string LoginSearchHashDeep(string nama, string parDep)
                {
                    loginOk = false;
                    string key = nama + parDep;
                    string login = "";
                    int count = 0;
                    foreach( string kk in kassAllHash.Keys )
                    {
                        if( 0 == kk.IndexOf(key) )
                        {
                            login = kassAllHash[kk];
                            count += 1;
                        }
                    }

                    if (1 == count)
                    {
                        loginOk = true;
                        return login;
                    }
                    else
                        loginOk = false;
                    return "";
                }



                protected static string LoginSearchHash()
                {
                    string parDep = MkDepartment();
                    loginOk = false;
                    string login;
                    string nama = MkFioWhite(workVec[colFioOne]);
                    string key = nama + parDep;
                    List<string> logins = new List<string>();
                    if(kassAllHash.ContainsKey(key))
                    {
                        login = kassAllHash[key];
                        loginOk = true;
                        return login;
                    }
                    parDep = parDep.Substring(0, 3);
                    return LoginSearchHashDeep(nama, parDep);
                }

        */
        #endregion


        protected static List<string> mkFierd()
        {
            #region #mkFierd
            List<string[]> allOtpuska = FileToArr(dataInPath + "all_otpuska.csv");
            List<string> arr = new List<string>();
            foreach (string[] line in allOtpuska)
            {
                if ((line.Length < 4)) continue;
                if (line[3].ToLower().IndexOf("nul") < 0)
                {
                    try { arr.Add(line[0]); }
                    catch { }
                }
            }
            return arr;
            #endregion
        }



        /// <summary>
        /// mkKassAllShort
        /// </summary>
        /// <param name="parDep"></param>
        /// <returns></returns>
        protected static List<string[]> mkKassAllShort()
        {
            #region #mkKassAllShort
            List<string[]> kassAll = FileToArr(dataInPath + "kass_all.csv");
            List<string[]> kassAllShort = new List<string[]>();
            foreach (string[] line in kassAll)
            {
                if (fierd.IndexOf(line[0]) > -1) continue;
                if ((line[1].ToLower().IndexOf("true") > -1) && (line.Length > 4))
                {
                    try
                    {
                        line[colFioTwo] = MkFioWhite(line[colFioTwo]);
                        kassAllShort.Add(line);
                    }
                    catch { }
                }
            }
            return kassAllShort;
            #endregion
        }


        protected static List<string[]> mkKassAllFull()
        {
            #region #mkKassAllFull
            List<string[]> kassAll = FileToArr(dataInPath + "kass_all.csv");
            List<string[]> kassAllFull = new List<string[]>();
            foreach (string[] line in kassAll)
            {
                //if (fierd.IndexOf(line[0]) > -1) continue;
                if ((line[1].ToLower().IndexOf("true") > -1) && (line.Length > 4))
                {
                    try
                    {
                        line[colFioTwo] = MkFioWhite(line[colFioTwo]);
                        kassAllFull.Add(line);
                    }
                    catch { }
                }
            }
            return kassAllFull;
            #endregion
        }



        /// <summary>
        /// MkFioWhite
        /// </summary>
        /// <param name="fff"></param>
        /// <returns></returns>
        protected static string MkFioWhite(string fff)
        {
            #region #MkFioWhite
            string[] fs = fff.Split(' ');
            string surn = fs[0];
            string ps = "";
            for (int i = 1; i < fs.Length; i++)
            {
                ps += fs[i];
            }

            string white = surn;
            string other = ps;
            foreach (char cha in other)
            {
                if (char.IsLetter(cha) && char.IsUpper(cha))
                {
                    char[] c = { cha };
                    string ss = new string(c);
                    white += ss;
                }
            }
            return white;
            #endregion
        }

        #region #Old
        /* 
                // NEW
                protected static List<string[]> mkKassAllShort0()
                {
                    List<string[]> kassAll = FileToArr(dataInPath + "kass_all.csv");
                    List<string[]> kassAllShort = new List<string[]>();
                    foreach (string[] line in kassAll)
                    {
                        if ((line[1].IndexOf("true") > -1) && (line.Length > 3))
                        {
                            try
                            {
                                line[colFioTwo] = MkFioWhite(line[colFioTwo]);
                            }
                            catch { }

                            kassAllShort.Add(line);
                        }
                    }
                    return kassAllShort;
                }

        */

        /*
                // NEW
                protected static List<string> MkClearDeps(string deps)
                {
                    List<string> outDeps = new List<string>();
                    try
                    {
                        string dd = deps.Replace("[", "").Replace("]", "").Replace(" ", "");
                        if (dd.IndexOf(",") < 0)
                            outDeps.Add(dd.Substring(0, 7));
                        else
                        {
                            string[] units = dd.Split(',');
                            foreach (string unit in units)
                            {
                                outDeps.Add(unit.Substring(0, 7));
                            }
                        }
                    }
                    catch
                    {
                        outDeps.Add("0000000");
                    }
                    return outDeps;
                }


                // NEW
                protected static Dictionary<string, string> MkKassAllHash()
                {
                    List<string[]> kassAll = FileToArr(dataInPath + "kass_all.csv");
                    Dictionary<string, string> kassHash = new Dictionary<string, string>();

                    foreach (string[] line in kassAll)
                    {
                        if ((line[1].IndexOf("true") > -1) && (line.Length > 3))
                        {
                            try
                            {
                                string fio = MkFioWhite(line[2]);
                                foreach (string dep in MkClearDeps(line[3]))
                                {
                                    string key = fio + dep;
                                    kassHash[key] = line[0];
                                }
                            }
                            catch { }
                        }
                    }
                    return kassHash;
                }
        */
        #endregion
    }
}
