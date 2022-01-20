using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    class DbBase
    {

        public static List<string> GetTermsPartner(string partner)
        {
            #region
            List<string> outList = new List<string>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                var terminal = context.Terminals;
                var w = department.ToList();

                var lingVar = from dep in department
                              where (dep.Partner == partner)
                              join term in terminal on dep.Department1 equals term.Department

                              select
                              new
                              {
                                  term = term.Termial
                              };

                foreach (var line in lingVar)
                {
                    outList.Add(line.term);
                }

            }
            return outList;
            #endregion
        }





        /*
        public static List<string> GetListTerms()
        {
            #region
            List<string> outList = new List<string>();
            using (var context = new postgresContext())
            {
                var terminal = context.terminals;
                var w = terminal.ToList();
                foreach (var line in w)
                {
                    if (outList.IndexOf(line.TerminalTerm) < 0) outList.Add(line.TerminalTerm);
                }

            }
            return outList;
            #endregion
        }


        public static List<string> GetListPartners()
        {
            #region
            List<string> outList = new List<string>();
            using (var context = new postgresContext())
            {
                var department = context.departments;
                var w = department.ToList();
                foreach (var line in w)
                {
                    if (outList.IndexOf(line.PartnerDep) < 0
                        && line.PartnerDep != "") outList.Add(line.PartnerDep);
                }

            }
            return outList;
            #endregion
        }

        


        public static void RefreshAll()
        {
            ExternProcess.RefresFromAccessAll();
            DbTerminalMethods.RefreshTerminal();
            DbDepartmentMethods.RefreshDepartment();
            DbOtborMethods.RefreshOtborFromFile();

        }

        */
    }
}
