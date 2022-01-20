using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    internal class OtborHardInputDep : Papa
    {
        public static void MainOtborHardDep(string choise)
        {
            // PgBase.OtborCl();

            List<string[]> arr = new List<string[]>();


            string outText = "term;dep\n";
            string[] choiseSplit = choise.Split(' ');
            if (choise.IndexOf(' ') < 0)
            {
                outLine = $"{choise}1;{choise}";
                outText += outLine + "\n";
                arr.Add(outLine.Split(';'));
                goto LabelMe;
            }

            string nos = choiseSplit[0].Substring(0, 4);
            foreach (string depdep in choiseSplit)
            {
                string myDep = depdep;
                if (depdep.Length < 7)
                {
                    myDep = nos + depdep;
                }
                string term = myDep + "1";
                outLine = term + ";" + myDep;
                outText += outLine + "\n";
                arr.Add(outLine.Split(';'));

                //pBlue(outLine);
            }

        LabelMe:


            //TextToFile(Path.Combine(dataInPath, "otbor.csv"), outText);

            DbOtborMet.AddManyOtbor(arr);
            info = outText;
        }
    }
}
