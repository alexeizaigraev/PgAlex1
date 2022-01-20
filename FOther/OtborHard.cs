using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PgAlex1
{
    class OtborHard : Papa
    {
        public static void MainOtborHard()
        {
            // PgBase.OtborCl();

            List<string[]> arr = new List<string[]>();


            bool flag = true;
            string outText = "term;dep\n";

            foreach (string[] vec in selectedItems)
            {
                outText += $"{vec[0]};{vec[1]}\n";
            }
            TextToFile(Path.Combine(dataInPath, "otbor.csv"), outText);

            DbOtborMet.RefreshOtborFromFile();
            info = outText;
        }

    }
}
