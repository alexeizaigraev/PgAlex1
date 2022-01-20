using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Eventing.Reader;

namespace PgAlex1
{
    class PostAll : PeoplePapa
    {
        protected static string postOutPath = File.ReadAllLines(dataConfigPath + "ConfigPostPath.txt")[0];


        protected static List<string[]> MkOtpuskAll() { return FileToArr(dataInPath + "all_otpuska.csv"); }
        protected static List<string[]> KassAll = mkKassAllShort();
        protected static List<string[]> OtpAll = MkOtpuskAll();

        public static void MainPostAll()
        {
            fierd = mkFierd();
            MkAgentKass("justin", "justin", true, "OutPostAll.csv", "OutPostOtpuskaJust.csv");
            MkAgentKass("allo", "allo", false, "OutPostAllAllo.csv", "OutPostOtpuskaAllo.csv");
            MkAgentKass("satua", "sat", true, "OutPostAllSat.csv", "OutPostOtpuskaSat.csv");

            postOutPath = Path.Combine(dataOutPath, "DOC");
            MkAgentKass("justin", "justin", true, "OutPostAll.csv", "OutPostOtpuskaJust.csv");
        }



        private static void MkAgentKass(string agent, string folder, bool mkOtpuska, string fNameKass, string fNameOtp)
        {
            string outText = "login;status;fio;terminal;agent\n";
            List<string> kass = new List<string>();
            foreach (string[] line in KassAll)
            {
                if (line[4].IndexOf(agent) > -1)
                {
                    outText += string.Join(";", line) + "\n";
                    kass.Add(line[0]);
                }
            }
            string ofName = Path.Combine(postOutPath, folder);
            ofName = Path.Combine(ofName, fNameKass);
            TextToFile(ofName, outText);

            if (!mkOtpuska) return;

            outText = "login;otpusk_start;otpusk_finish;data_uvolneniya\n";
            foreach (string[] line in OtpAll)
            {
                string lolo = line[0];
                if (kass.IndexOf(lolo) > -1)
                    //outText += String.Join(";", line).Replace("null", "") + "\n";
                    outText += $"{line[0]};{line[1]};{line[2]};{line[3]}\n";
            }
            ofName = Path.Combine(postOutPath, folder);
            ofName = Path.Combine(ofName, fNameOtp);
            TextToFile(ofName, outText);
        }

    }
}
