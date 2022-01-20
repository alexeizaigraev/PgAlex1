using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    internal class DbLogi : Papa
    {
        internal static List<List<string>> GetLogData()
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
                              join term in terminal on dep.Department1 equals term.Department
                              join otb in otbor on term.Termial equals otb.Term
                              select
                              new
                              {
                                  department = dep.Department1,
                                  term = term.Termial,
                                  serial = term.SerialNumber,
                                  adr = dep.Address
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.department);
                    lineList.Add(line.term);
                    lineList.Add(line.serial);
                    lineList.Add(line.adr);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }


        private static void AddLogOne(List<string> vec, string kind)
        {
            #region
            using (var context = new postgresContext())
            {

                var log = new Logi
                {
                    Department = vec[0],
                    Termial = vec[1],
                    SerialNumber = vec[2],
                    Address = vec[3],
                    Datalog = DateTime.Today.ToString("dd.MM.yyyy"),
                    Kind = kind,
                };

                context.Logis.Add(log);
                context.SaveChanges();

            }
            #endregion
        }




        public static void Loger(string kind)
        {
            #region
            Papa.info += "\nLoged " + kind + "\n";
            var arr = GetLogData();
            foreach (var vec in arr)
            {
                try
                {
                    AddLogOne(vec, kind);
                    Papa.info += $" loged {vec[0]}\n";
                }
                catch (Exception ex) { SayRed(ex.Message); }
            }


            string nau = DateTime.Today.ToString("yyyy.MM.dd");
            string fNameOld = "R:/DRM/Access/db2_be.accdb";
            string fNameNew = $"R:/DRM/BackupAccess/{nau}_db2_be.accdb";
            try
            {
                CopyOneFile(fNameOld, fNameNew);
                SayGreen(fNameNew);
            }
            catch (Exception ex) { SayRed(ex.Message + "\n"); }
            #endregion
        }

    }
}
