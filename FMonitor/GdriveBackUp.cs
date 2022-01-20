using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PgAlex1
{
    internal class GdriveBackUp : Papa
    {

        //private static string outPath = Path.Combine(FileToVec(Path.Combine(dataConfigPath, "backup_gdrive_path.txt"))[0])  + "_" + DateNowDots();
        private static string outPath = Path.Combine(FileToVec(Path.Combine(dataConfigPath, "backup_gdrive_path.txt"))[0]);

        private static int summa = 0;
        private static SortedDictionary<string, int> rezDict = new SortedDictionary<string, int>();


        private static List<string> agFolders = MkGdriveFolders();


        internal static void MainGdriveBackUp()
        {
            summa = 0;
            outPath = Path.Combine(outPath, DateNowDots());
            DirectoryInfo dirInfo = new DirectoryInfo(outPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            //dirInfo.CreateSubdirectory(subpath);
            //CopyFilesRecursively(gDrivePath, outPath);
            foreach (var agFolder in agFolders)
            {
                var fullInDir = Path.Combine(gDrivePath, agFolder);
                var fullOutDir = Path.Combine(outPath, agFolder);
                info += fullInDir + "\n";
                try
                {
                    CopyDir(fullInDir, fullOutDir);
                }
                catch (Exception ex) { info += ex.Message + "\n"; }

            }

            info += $"all {summa}\n\n\twell";
        }


        private static bool PriznacInName(string fName)
        {
            fName = fName.ToLower();
            bool flag = false;
            foreach (var priznak in agFolders)
            {
                if (fName.IndexOf(priznak) > -1)
                    flag = true;
                break;
            }
            return flag;
        }

        private static void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                try
                {
                    File.Copy(s1, s2, true);
                    summa += 1;
                }
                catch { }

            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                try
                {
                    CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
                }
                catch { }
            }
        }


    }
}
