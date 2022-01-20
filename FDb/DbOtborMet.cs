using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    internal class DbOtborMet : Papa
    {

        internal static void RefreshOtborFromFile()
        {
            #region
            DeleteAllOtbor();
            Say("otbor add:\n");
            var data = FileToArr(Path.Combine(dataInPath, "otbor.csv"));
            var sum = 0;
            using (var context = new postgresContext())
            {
                int countLines = -1;
                foreach (var dataLine in data)
                {
                    countLines += 1;
                    if (countLines > 0)
                    {
                        var terminal = dataLine[0];
                        var department = dataLine[1];

                        if (terminal == "" || department == "") continue;

                        var otbor = new Otbor
                        {
                            Term = terminal,
                            Dep = department
                        };
                        context.Otbors.Add(otbor);
                        Say($"{terminal}");
                        sum += 1;
                    }
                }
                context.SaveChanges();
            }
            SayGreen($"\notbor + {sum}\n");
            #endregion
        }


        internal static void AddOtborSome(List<string> terms)
        {
            #region
            DeleteAllOtbor();
            using (var context = new postgresContext())
            {
                foreach (var term in terms)
                {
                    var otbor = new Otbor
                    {
                        Term = term,
                        Dep = term.Substring(0, 7)
                    };
                    context.Otbors.Add(otbor);
                    Say($"{otbor.Term}");
                }
                context.SaveChanges();
            }
            #endregion
        }



        internal static void AddManyOtbor(List<string[]> arr)
        {
            #region
            DeleteAllOtbor();
            using (var context = new postgresContext())
            {
                //int countLines = -1;
                foreach (var otborLine in arr)
                {
                    //countLines += 1;
                    //if (countLines < 1) continue;
                    var otbor = new Otbor
                    {
                        Term = otborLine[0],
                        Dep = otborLine[1]
                    };
                    context.Otbors.Add(otbor);
                }
                context.SaveChanges();
            }
            #endregion
        }


        private static void AddOtbor(string terminal, string department)
        {
            #region
            using (var context = new postgresContext())
            {

                var otbor = new Otbor
                {
                    Term = terminal,
                    Dep = department
                };

                context.Otbors.Add(otbor);
                context.SaveChanges();

            }
            #endregion
        }


        internal static void DeleteAllOtbor()
        {
            #region
            using (var context = new postgresContext())
            {
                var otbor = context.Otbors;

                try
                {
                    context.Otbors.RemoveRange(otbor);
                    context.SaveChanges();
                }
                catch { }
            }
            #endregion
        }

        private static void ShowtKeysOtbor()
        {
            #region
            using (var context = new postgresContext())
            {

                var otbor = context.Otbors;
                var w = otbor.ToList();
                foreach (var line in w)
                {
                    Console.WriteLine(line.Term);
                }

            }
            #endregion
        }

        internal static string ShowtOtbor()
        {
            #region
            var rez = "\n";
            using (var context = new postgresContext())
            {

                var otbor = context.Otbors;
                var w = otbor.ToList();
                foreach (var line in w)
                {
                    //Console.WriteLine(line.DepOtbor + " " + line.TermOtbor);
                    rez += $"{line.Dep} {line.Term}\n";
                }

            }
            #endregion
            return rez;
        }

        public static List<string[]> GetAllbor()
        {
            #region
            var outList = new List<string[]>();
            using (var context = new postgresContext())
            {
                var otbor = context.Otbors;
                var w = otbor.ToList();
                foreach (var line in w)
                {
                    var lineVec = new string[] { line.Term, line.Dep };
                    outList.Add(lineVec);
                }

            }
            return outList;
            #endregion
        }

        public static List<string> GetListDepOtbor()
        {
            #region
            var outList = new List<string>();
            using (var context = new postgresContext())
            {
                var otbor = context.Otbors;
                var w = otbor.ToList();
                foreach (var line in w)
                {
                    outList.Add(line.Dep);
                }
            }
            return outList;
            #endregion
        }

    }
}
