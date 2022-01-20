using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    class GetRp : Papa
    {
        internal static void MainGetRp()
        {
            var knigiPath = "R:/DRM/Ключи/КНИГИ";
            //Papa.ClearFolderPdf(knigiPath);
            var folderHash = MkComonHash(3);
            var otbor = DbOtborMet.GetListDepOtbor();
            foreach (var unit in otbor)
            {

                var dep = unit;
                var agSign = dep.Substring(0, 3);
                var folderAgent = folderHash[agSign];
                var folder = Path.Combine(gDrivePath, folderAgent);
                var folderFull = Path.Combine(folder, dep);
                CoperRp(folderFull, knigiPath);

                //SayBlue(agSign);
            }
        }
    }
}
