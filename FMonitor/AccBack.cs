using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    class AccBack : Papa
    {
        public static void MainAccBack()
        {
            #region
            string nau = DateTime.Today.ToString("yyyy.MM.dd");
            string fNameOld = @"R:\DRM\Access\db2_be.accdb";
            string fNameNew = @"R:\DRM\BackupAccess\db2_be_" + nau + "_.accdb";
            //string fNameNew = @"R:\DRM\BackupAccess\db2_be_" + nau + "_" + kind + ".accdb";
            CopyOneFile(fNameOld, fNameNew);

            /*
            string accessConfigDir = Path.Combine(dataPath, "ConfigDir");
            string dirOut = "";
            try { dirOut = File.ReadAllLines(Path.Combine(accessConfigDir, "PathBackUpAccessOut.txt"))[0]; }
            catch { Alarm("Не могу прочитать", dataConfigDirPath + "PathBackUpAccessOut.txt"); }
            string[] storedDirs = File.ReadAllLines(Path.Combine(accessConfigDir, "PathBackUpAccessDirList.txt"));

            foreach (string dir in storedDirs)
            {
                ClonAccess(dir, dirOut);
            }
            return;
            */
            #endregion
        }
    }
}

