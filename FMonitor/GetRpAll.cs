using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    class GetRpAll : Papa
    {
        internal static void MainGetRpAll()
        {
            var knigiPath = "R:/DRM/Ключи/КНИГИ";
            //Papa.ClearFolderPdf(knigiPath);
            var folderHash = MkComonHash(3);
            List<string> agFolders = new List<string>();
            foreach (var folder in folderHash.Values)
            {
                agFolders.Add(folder);
            }
            //var agFolder = SomePapa.MenuColKeySimpl(agFolders);
            var myFolder = Path.Combine(gDrivePath, selectedFolder);
            string[] dirs = Directory.GetDirectories(myFolder);
            foreach (string fold in dirs)
            {
                SayGreen("\t" + fold);
                CoperRp(fold, knigiPath);
            }


            /*
            var otbor = AccBase.AccGetDepsOtbor();

            foreach (var dep in otbor)
            {
                var agSign = dep.Substring(0, 3);
                var folderAgent = folderHash[agSign];
                var folder = Path.Combine(gDrivePath, folderAgent);
                var folderFull = Path.Combine(folder, dep);
                try
                {
                    CoperRp(folderFull, knigiPath);
                }
                catch (Exception ex)
                {
                    Alarm("Ошибка папки", folderFull);
                }

            
                //SayBlue(agSign);
            }
            */
        }
    }
}
