using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PgAlex1
{
    internal class OtborDb : Papa
    {
        public static void MainOtborDb(string inputDeps)
        {
            //info = "";
            // PgBase.OtborCl();
            var choise = inputDeps;
            List<string[]> arr = new List<string[]>();
            string outText = "term;dep\n";
            //SayBlue("\n Dep: From To:");
            //string choise = Console.ReadLine();
            //pBlue("\n__________\n");
            int start = 0;
            int finish = 0;
            if (choise.IndexOf(" ") > -1)
            {
                string[] choiseSplit = choise.Split(' ');
                if (choiseSplit[0].Length != 7 || choiseSplit[1].Length != 7) Sos("Bed dep", choise);
                start = Convert.ToInt32(choiseSplit[0]);
                finish = Convert.ToInt32(choiseSplit[1]);
            }
            else
            {
                if (choise.Length != 7) Sos("Bed dep", choise);
                start = Convert.ToInt32(choise);
                finish = start;
            }
            //int dep = 0;
            //int term = 0;
            string depStr = "";
            string termStr = "";
            string outLine = "";
            for (int x = start; x <= finish; x++)
            {
                depStr = x.ToString();
                termStr = depStr + "1";
                outLine = termStr + ";" + depStr;
                arr.Add(outLine.Split(';'));

                //pBlue(outLine);
                outText += outLine + "\n";
            }

            DbOtborMet.AddManyOtbor(arr);
            //info = outText;
            info = $"\n{outText}\n";
        }

    }
}
