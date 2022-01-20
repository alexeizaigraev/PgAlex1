using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    internal class OtborHardInput : Papa
    {
        public static void MainOtborHard(string inputTerms)
        {
            // PgBase.OtborCl();

            List<string[]> arr = new List<string[]>();


            bool flag = true;
            string outText = "term;dep\n";
            string line = "";

            //SayYellow("\n Terminals cross spaces:\n");
            //string choise = Console.ReadLine();
            string choise = inputTerms;
            string[] choiseSplit = choise.Split(' ');
            if (choise.IndexOf(' ') < 0)
            {
                line = choise + ";" + choise.Substring(0, 7);
                outText += line + "\n";
                arr.Add(line.Split(';'));
                goto LabelMe;
            }

            foreach (string term in choiseSplit)
            {
                string myTerm = term;
                string dep = myTerm.Substring(0, 7);
                line = $"{myTerm};{dep}";
                outText += line + "\n";
                arr.Add(line.Split(';'));
            }




        LabelMe:


            //TextToFile(Path.Combine(dataInPath, "otbor.csv"), outText);

            DbOtborMet.AddManyOtbor(arr);
            info = outText;
        }
    }
}
