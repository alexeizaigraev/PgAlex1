using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PgAlex1
{
    internal class Gnetz : Papa
    {

        private static string[] priznaks = { "foto", "photo", "фото" };
        private static string outPath = Path.Combine(dataPath, "gnetz");

        private static SortedDictionary<string, int> rezDict = new SortedDictionary<string, int>();

        private static SortedDictionary<string, int> rezDictFold = new SortedDictionary<string, int>();

        private static List<string> allDeps = DbDepMeth.GetListDep();

        private static List<string> senDeps = new List<string>();

        private static List<string> agFolders = MkGdriveFolders();

        private static string globAgFold = "";

        internal static void MainGnetz()
        {
            try
            {
                //Papa.info = "";
                info = gnetzKind + "\n\n";
                foreach (var agFolger in agFolders)
                {
                    rezDictFold[agFolger] = 0;
                    globAgFold = agFolger;
                    var agPath = Path.Combine(gDrivePath, agFolger);
                    WalkDirectoryTree(new DirectoryInfo(agPath));
                }

                /*
                info += "\n\n";

                int sumFold = 0;
                foreach (var fold in rezDictFold.Keys)
                {
                    info += $"{fold};{rezDictFold[fold]}\n";
                    sumFold++;
                }
                info += "\n";
                info += $"sum {sumFold}";
                info += "\n\n";
                */
                var resived = GetFilesFromFolder(outPath);

                var comonHash = MkComonHash(3);
                foreach (var fName in resived)
                {
                    //info += fName + "\n";
                    var sname = fName.Split("\\");

                    try
                    {
                        string agKey = sname[sname.Count() - 1].Substring(0, 3);
                        var fold = comonHash[agKey];
                        if (rezDict.ContainsKey(fold)) rezDict[fold] += 1;
                        else { rezDict[fold] = 1; }
                        //info += $"{fold} {rezDict[fold]}\n";
                    }
                    catch { }
                }

                int sum = 0;
                foreach (var key in rezDict.Keys)
                {
                    info += $"{key};{rezDict[key]}\n";
                    sum += rezDict[key];
                }

                info += $"\nsum {sum}";
                info += "\n\nwell";
            }
            catch (Exception ex)
            { info += ex.Message + "\n"; }

        }


        private static bool PriznacInName(string fName)
        {
            fName = fName.ToLower();
            bool flag = false;
            foreach (var priznak in priznaks)
            {
                if (fName.IndexOf(priznak) > -1)
                    flag = true;
                break;
            }
            return flag;
        }

        public static void WalkDirectoryTree(DirectoryInfo root)
        {
            FileInfo[] files = null;

            try
            {
                files = root.GetFiles();
                foreach (var file in files)
                {
                    var fName = Convert.ToString(file);
                    if (PriznacInName(fName))
                    {
                        try
                        {
                            var fInfoOLld = new FileInfo(fName);
                            var sOldName = fInfoOLld.ToString().Split('\\');
                            var dep = sOldName[sOldName.Length - 2];
                            var agFolder = sOldName[sOldName.Length - 3];
                            if (sOldName.Length > 3 && agFolders.IndexOf(agFolder) > -1)
                            {
                                var fNameNew = Path.Combine(outPath, dep + "_" + Convert.ToString(fInfoOLld.Name));
                                FileInfo fInfoFnameNew = new FileInfo(fNameNew);
                                if (gnetzKind == "copy")
                                {
                                    fInfoOLld.CopyTo(fInfoFnameNew.ToString(), true);
                                    rezDictFold[globAgFold] += 1;
                                }
                                else
                                {
                                    fInfoOLld.MoveTo(fInfoFnameNew.ToString(), true);
                                    rezDictFold[globAgFold] += 1;
                                }
                                //info += fNameNew + "\n";
                            }

                        }
                        catch (Exception ex) { info += ex.Message + "\n"; }

                    }

                }
            }
            catch (Exception ex)
            { info += ex.Message + "\n"; }

            if (files == null) return;

            foreach (var dirInfo in root.GetDirectories()) WalkDirectoryTree(dirInfo);
        }
    }
}
