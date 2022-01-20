using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Npgsql;

namespace PgAlex1
{
    internal class SiteNew : Papa
    {
        private static Dictionary<string, string> regimesGroupDict = new Dictionary<string, string>();
        //private static List<string> natasha = MkNatasha();

        private static List<List<string>> GetData()
        {
            List<List<string>> outList = new List<List<string>>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                var w = department.ToList();
                foreach (var line in w)
                {
                    List<string> lineList = new List<string>();
                    lineList.Add(line.Department1);
                    lineList.Add(line.Edrpou);
                    lineList.Add(line.Address);
                    lineList.Add(line.Register);
                    lineList.Add(line.Partner);

                    outList.Add(lineList);
                }

            }
            return outList;
        }




            public static void MainSite()
        {
            MkRegimeGroupDict();
            var data = GetData();
            string outFileName = dataOutPath + "OutSite.csv";
            string outFileNamePhp = dataOutPath + "page-departments.php";
            var natasha = MkNatasha();
            //Dictionary<string, string> regimes = FileToDict(4);
            Dictionary<string, string> regimes = MkComonHash(4);

            var access = data;

            string header = "Найменування відокремленного підрозділу та ПНФП;Адреса;Дата та номер рішення про створення;ЄДРПОУ;Режим роботи;Платежі приймаються в Платіжній системі;Платежі виплачуються  в Платіжній системі\n";
            string outTextClear = header;
            string outTextPhp = "";
            int count = 0;
            foreach (var accessLine in access)
            {
                try
                {
                    //if (accessLine[0] == "2") continue;

                    string dep = "";
                    if (accessLine[0].IndexOf("№") > -1) { dep = accessLine[0].Split('№')[1]; }
                    else { dep = accessLine[0]; }

                    string regime = "не працює";
                    //if ((dep.Length > 2) && (natasha.IndexOf(dep) > -1))
                    if (dep.Length > 2)
                    {
                        try
                        {
                            string agSign = dep.Substring(0, 3);
                            regime = regimes[agSign];
                        }
                        catch { }

                        try
                        {
                            regime = regimesGroupDict[dep];
                        }
                        catch { }
                    }


                    if ("1" == accessLine[0])
                        regime = "ПН-ПТ 09:00-18:00";

                    if (regime == "не працює" && natasha.IndexOf(accessLine[0]) > -1)
                    {
                        try
                        {
                            string agSign = dep.Substring(0, 3);
                            regime = regimes[agSign];
                        }
                        catch { }
                    }
                    count++;
                    if (accessLine[1] != "")
                    {
                        outTextPhp += "<tr><td>ВІДДІЛЕННЯ №" + accessLine[0] + @"</td><td>" + accessLine[2] + @"</td><td>" + accessLine[3] + @"</td><td>" + accessLine[1] + @"</td><td>" + regime + @"</td><td>ВПС ЕЛЕКТРУМ, ВПС FLASHPAY</td><td>ВПС ЕЛЕКТРУМ</td></tr>" + "\n";
                        outTextClear += "ВІДДІЛЕННЯ №" + accessLine[0] + ";" + accessLine[2] + ";" + accessLine[3] + ";" + accessLine[1] + ";" + regime + ";ВПС ЕЛЕКТРУМ, ВПС FLASHPAY;ВПС ЕЛЕКТРУМ\n";
                    }
                    else
                    {
                        outTextPhp += "<tr><td>ПНФП ВІДДІЛЕННЯ №" + accessLine[0] + @"</td><td>" + accessLine[2] + @"</td><td>" + accessLine[3] + @"</td><td>" + accessLine[1] + @"</td><td>" + regime + @"</td><td>ВПС ЕЛЕКТРУМ, ВПС FLASHPAY</td><td>ВПС ЕЛЕКТРУМ</td></tr>" + "\n";
                        outTextClear += "ПНФП ВІДДІЛЕННЯ №" + accessLine[0] + ";" + accessLine[2] + ";" + accessLine[3] + ";" + accessLine[1] + ";" + regime + ";ВПС ЕЛЕКТРУМ, ВПС FLASHPAY;ВПС ЕЛЕКТРУМ\n";
                    }

                }
                catch { }
            }

            var siteOld = FileToText(dataOutPath + "OutSite.csv");
            if (siteOld == outTextClear) Say("\n\n\tno change\n");
            //infoSmall = "@ no change\n" + infoSmall;



            string textPhp = outTextPhp;
            string text1 = FileToText("Config/SiteText1.txt");
            string text2 = FileToText("Config/SiteText2.txt");
            string fullTextPhp = text1 + textPhp + text2;
            TextToFile(outFileNamePhp, fullTextPhp);

            TextToFile(outFileName, outTextClear);

            info += $"\n{count}\n";

        }



        public static void MkRegimeGroupDict()
        {
            #region #Mover
            var regemesPath = Path.Combine(dataPath, "Regimes");
            DirectoryInfo d = new DirectoryInfo(regemesPath);
            FileInfo[] infos = d.GetFiles("*.*");
            foreach (FileInfo f in infos)
            {
                string fName = f.FullName;
                var data = FileToArr(fName);
                foreach (var line in data)
                {
                    regimesGroupDict[line[0]] = line[1];
                }
            }
            #endregion
        }
    }
}