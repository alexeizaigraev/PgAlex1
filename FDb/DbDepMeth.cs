using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    class DbDepMeth : Papa
    {
        internal static void RefreshDepartment()
        {
            #region
            Say("# RefreshDepartment");
            int countOk = 0;
            int countLines = -1;
            try
            {
                DeleteAllDepartment();
            }
            catch (Exception ex) { SayRed(ex.Message); }
            var data = FileToArr(Path.Combine(dataInPath, "departments.csv"));
            using (var context = new postgresContext())
            {


                foreach (var dataLine0 in data)
                {
                    var dataLine = GoodVec(dataLine0);
                    countLines += 1;
                    if (countLines > 0)
                    {
                        if (dataLine[0] == "") continue;

                        try
                        {
                            var department = new Department
                            {
                                Department1 = dataLine[0],
                                Region = dataLine[1],
                                DistrictRegion = dataLine[2],
                                DistrictCity = dataLine[3],
                                CityType = dataLine[4],
                                City = dataLine[5],
                                Street = dataLine[6],
                                StreetType = dataLine[7],
                                Hous = dataLine[8],
                                PostIndex = dataLine[9],
                                Partner = dataLine[10],
                                Status = dataLine[11],
                                Register = dataLine[12],
                                Edrpou = dataLine[13],
                                Address = dataLine[14],
                                PartnerName = dataLine[15],
                                IdTerminal = dataLine[16],
                                Koatu = dataLine[17],
                                TaxId = dataLine[18],
                                Koatu2 = dataLine[19],
                            };

                            context.Departments.Add(department);
                            //Say($"+ dep new {department.DepartmentDep}");
                            countOk += 1;
                        }
                        catch (Exception ex) { SayRed($"{dataLine[0]} {ex.Message}"); }
                    }
                }
                context.SaveChanges();
            }
            SayGreen($"\nadd {countOk} from {countLines}\n {countLines - countOk} erroros\n");
            #endregion
        }

        internal static void DeleteAllDepartment()
        {
            try
            {
                #region
                using (var context = new postgresContext())
                {
                    var department = context.Departments;

                    context.Departments.RemoveRange(department);
                    context.SaveChanges();
                }
                #endregion
                Say("\nDeleteAllDepartment");
            }
            catch (Exception ex) { SayRed($"\nDeleteAllDepartment {ex.Message}"); }
        }

        internal static void DeleteOneDepartment(string dep)
        {
            #region
            try
            {
                using (var context = new postgresContext())
                {
                    var singleRec = context.Departments.FirstOrDefault(x => x.Department1 == dep);// object your want to delete
                    context.Departments.Remove(singleRec);
                    context.SaveChanges();
                    Say($"del {dep}");
                }
            }
            catch (Exception ex) { SayRed($"{dep} {ex.Message}"); }
            #endregion
        }


        public static List<List<string>> GetAllDep()
        {
            #region
            var outList = new List<List<string>>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                var w = department.ToList();
                foreach (var line in w)
                {
                    var lineVec = new List<string> {
                                line.Department1,
                                line.Region,
                                line.DistrictRegion,
                                line.DistrictCity,
                                line.CityType,
                                line.City,
                                line.Street,
                                line.StreetType,
                                line.Hous,
                                line.PostIndex,
                                line.Partner,
                                line.Status,
                                line.Register,
                                line.Edrpou,
                                line.Address,
                                line.PartnerName,
                                line.IdTerminal,
                                line.Koatu,
                                line.TaxId,
                                line.Koatu2};

                    outList.Add(lineVec);
                }

            }
            return outList;
            #endregion
        }

        internal static List<string> GetOneDep(string myDep)
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                //var w = department.ToList();

                var lingVar = from dep in department
                              where dep.Department1 == myDep
                              select
                              new
                              {
                                  dep.Department1,
                                  dep.Region,
                                  dep.DistrictRegion,
                                  dep.DistrictCity,
                                  dep.CityType,
                                  dep.City,
                                  dep.Street,
                                  dep.StreetType,
                                  dep.Hous,
                                  dep.PostIndex,
                                  dep.Partner,
                                  dep.Status,
                                  dep.Register,
                                  dep.Edrpou,
                                  dep.Address,
                                  dep.PartnerName,
                                  dep.IdTerminal,
                                  dep.Koatu,
                                  dep.TaxId,
                                  dep.Koatu2
                              };
                foreach (var dep in lingVar)
                {
                    List<string> vec = new List<string>();

                    vec.Add(dep.Department1);
                    vec.Add(dep.Region);
                    vec.Add(dep.DistrictRegion);
                    vec.Add(dep.DistrictCity);
                    vec.Add(dep.CityType);
                    vec.Add(dep.City);
                    vec.Add(dep.Street);
                    vec.Add(dep.StreetType);
                    vec.Add(dep.Hous);
                    vec.Add(dep.PostIndex);
                    vec.Add(dep.Partner);
                    vec.Add(dep.Status);
                    vec.Add(dep.Register);
                    vec.Add(dep.Edrpou);
                    vec.Add(dep.Address);
                    vec.Add(dep.PartnerName);
                    vec.Add(dep.IdTerminal);
                    vec.Add(dep.Koatu);
                    vec.Add(dep.TaxId);
                    vec.Add(dep.Koatu2);

                    outList.Add(vec);
                }
            };
            return outList[0];
            #endregion
        }


        internal static void AddOneDepartment(List<string> vec)
        {
            #region
            string dep = vec[0];
            Say("# AddOneDepartment");
            DeleteOneDepartment(dep);
            using (var context = new postgresContext())
            {
                var department = new Department
                {
                    Department1 = vec[0],
                    Region = vec[1],
                    DistrictRegion = vec[2],
                    DistrictCity = vec[3],
                    CityType = vec[4],
                    City = vec[5],
                    Street = vec[6],
                    StreetType = vec[7],
                    Hous = vec[8],
                    PostIndex = vec[9],
                    Partner = vec[10],
                    Status = vec[11],
                    Register = vec[12],
                    Edrpou = vec[13],
                    Address = vec[14],
                    PartnerName = vec[15],
                    IdTerminal = vec[16],
                    Koatu = vec[17],
                    TaxId = vec[18],
                    Koatu2 = ""
                };

                context.Departments.Add(department);
                context.SaveChanges();
                Say($"+ dep new {department.Department1}");
            }
            #endregion
        }

        internal static List<string> GetListDep()
        {
            #region
            var outList = new List<string>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                var w = department.ToList();
                foreach (var line in w)
                {
                    outList.Add(line.Department1);
                }

            }
            return outList;
            #endregion
        }

        internal static List<string> GetLisPartner()
        {
            #region
            var outList = new List<string>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                var w = department.ToList();
                foreach (var line in w)
                {
                    if (outList.IndexOf(line.Partner) < 0) outList.Add(line.Partner);
                }

            }
            return outList;
            #endregion
        }


        public static string NextDep(string dep)
        {
            string outDep = "";
            var deps = GetListDep();
            int ind = deps.IndexOf(dep);
            if (ind < deps.Count - 1) { outDep = deps[ind + 1]; }
            else { outDep = deps[0]; }
            return outDep;
        }

        public static string PredDep(string dep)
        {
            string outDep = "";
            var deps = GetListDep();
            int ind = deps.IndexOf(dep);
            if (ind > 0) { outDep = deps[ind - 1]; }
            else { outDep = deps[deps.Count - 1]; }
            return outDep;
        }

        internal static string DepGetAddress(string myDep)
        {
            #region
            List<string> outList = new List<string>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                //var w = department.ToList();

                var lingVar = from dep in department
                              where dep.Department1 == myDep
                              select
                              new
                              {
                                  dep.Department1
                              };
                foreach (var dep in lingVar)
                {
                    string rez = "";

                    outList.Add(rez);
                }
            };
            return outList[0];
            #endregion
        }

    }
}
