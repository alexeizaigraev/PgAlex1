using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{


    internal class HrDep : Papa
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
                              join otb in otbor on dep.Department1 equals otb.Dep
                              where otb.Dep == dep.Department1
                              select
                              new
                              {
                                  dep = dep.Department1,
                                  reg = dep.Region,
                                  distrReg = dep.DistrictRegion,
                                  postIndex = dep.PostIndex,
                                  cityType = dep.CityType,
                                  city = dep.City,
                                  distrCity = dep.DistrictCity,
                                  streetType = dep.StreetType,
                                  street = dep.Street,
                                  hous = dep.Hous,
                                  adres = dep.Address,
                                  koatu = dep.Koatu,
                                  koatu2 = dep.Koatu2
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.dep);
                    lineList.Add(line.reg);
                    lineList.Add(line.distrReg);
                    lineList.Add(line.postIndex);
                    lineList.Add(line.cityType);
                    lineList.Add(line.distrCity);
                    lineList.Add(line.streetType);
                    lineList.Add(line.street);
                    lineList.Add(line.hous);
                    lineList.Add(line.adres);
                    lineList.Add(line.koatu);
                    lineList.Add(line.koatu2);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }

        public static void MainHrDep()
        {
            info = "";
            //var x = "";
            //string myKey = "partner";
            string outText = "№ п/п;\"№ Відділення ТОВ \"\"ЕПС\"\"\";Область;Район в обл.;Індекс;Тип населеного пункту;Населений пункт;Район в місті;Тип вулиці;Адреса;Номер будинку;Дата признчення керівника;модель РРО;Заводський № РРО;2;koatu1;koatu2\n";
            var data = GetData();
            //var sizeLine = data[0].Count();


            int count = 0;
            foreach (var u in data)
            {
                try
                {

                    count++;
                    string outLine = "";

                    outLine = String.Format("{0}", count) + ";"
                            + u[0] + ";"
                            + u[1] + ";"
                            + u[2] + ";"
                            + u[3] + ";"
                            + u[4] + ";"
                            + u[5] + ";"
                            + u[6] + ";"
                            + u[7] + ";"
                            + u[8] + ";"
                            + u[9] + ";"
                            + "" + ";"
                            + "" + ";"
                            + "" + ";"
                            + u[10] + ";"
                            + u[11] + ";";

                    string sity = u[5];
                    string districtSity = u[6];
                    string koatuOld = u[11];

                    //string koatuNew = MkKoatuNew(outLine, koatuOld, sity, districtSity);
                    string koatuNew = "";
                    try { koatuNew = Koatu2.MkKoatuNew(sity, districtSity, koatuOld); }
                    catch { }
                    outLine += koatuNew;
                    outText += outLine + "\n";

                    info += u[0] + "\n";

                }
                catch { }
            }
            TextToFile(dataOutPath + "hr_new_deps.csv", outText);
        }

        

    }
}
