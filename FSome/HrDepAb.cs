using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    class HrDepAb : Papa
    {

        private static List<List<string>> GetData()
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                var terminal = context.Terminals;
                var otbor = context.Otbors;
                var w = department.ToList();

                var lingVar = from dep in department
                              select
                              new
                              {
                                  dep = dep.Department1,
                                  adres = dep.Address,
                                  partner = dep.Partner
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.dep);
                    lineList.Add(line.adres);
                    lineList.Add(line.partner);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }

        public static void MainHrDepAb()
        {
            #region
            var natasha = MkNatasha();
            //string myKey = "partner";
            //string outText = "№ п/п;\"№ Відділення ТОВ \"\"ЕПС\"\"\";Адреса;koatu1;koatu2;Партнер\n";
            string outText = "№ п/п;\"№ Відділення ТОВ \"\"ЕПС\"\"\";Адреса;Партнер\n";
            var data = GetData();
            int sizeLine = data[0].Count();


            int count = 0;
            foreach (var u in data)
            {
                try
                {
                    if (u[0] == "" || natasha.IndexOf(u[0]) < 0) continue;
                    outText += outLine = String.Format("{0}", count) + ";"
                            + String.Join(";", u) + "\n";
                    count++;
                }
                catch (Exception ex) { info += ex.Message + "\n"; }
            }
            string oFname = dataOutPath + "summury_ab.csv";
            SayGreen($"\n\n\tsumm {count}\n\n");
            TextToFile(oFname, outText);
            //OpenNote(oFname);
            #endregion
        }


    }
}
