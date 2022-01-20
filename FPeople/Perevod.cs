using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PgAlex1
{
    class Perevod : PeoplePapa
    {

        static bool flag = true;
        public static void MainPerevod()
        {
            Short();
            if (!flag)
            {
                SayGray(" Запуск Long...");
                Long();
            }
        }


        public static void Short()
        {
            outLine = "";
            outText = "";
            loginOk = false;

            colFioOne = 0;
            colFioTwo = 2;
            colLoginTwo = 0;
            colDepTwo = 3;
            colDepOne = 1;

            fierd = mkFierd();
            myKassAll = mkKassAllShort();
            //kassAllHash = MkKassAllHash();

            string fOutName = "OutPerevod.csv";
            string[] lines = FileToVec(dataInPath + "perevod.csv");

            string unfind = "";

            foreach (string line in lines)
            {
                workVec = line.Split(';');
                List<string> myLogin = LoginSearch(MkDepartment());
                //string myLogin = LoginSearchHash();
                if (!loginOk)
                {
                    flag = false;
                    return;
                }
                else
                {
                    foreach (var item in myLogin)
                    {
                        outText += item + ";" + workVec[1] + ";" + workVec[2] + "\n";
                    }
                }
            }
            TextToFile(dataOutPath + fOutName, unfind + outText);
            SayBlue("\n\n" + outText + "\n");
            SayYellow("\n\tВсех нашли\n");
        }


        public static void Long()
        {
            outLine = "";
            outText = "";
            loginOk = false;

            colFioOne = 0;
            colFioTwo = 2;
            colLoginTwo = 0;
            colDepTwo = 3;
            colDepOne = 1;

            myKassAll = mkKassAllFull();
            //fierd = mkFierd();
            //myKassAll = mkKassAllShort();
            //kassAllHash = MkKassAllHash();

            string fOutName = "OutPerevod.csv";
            string[] lines = FileToVec(dataInPath + "perevod.csv");

            string unfind = "";

            foreach (string line in lines)
            {
                workVec = line.Split(';');
                List<string> myLogin = LoginSearch(MkDepartment());
                //string myLogin = LoginSearchHash();
                if (!loginOk)
                {
                    unfind += workVec[0] + ";" + workVec[1] + ";" + workVec[2] + "\n";
                    continue;
                }
                else
                {
                    foreach (var item in myLogin)
                    {
                        outText += item + ";" + workVec[1] + ";" + workVec[2] + "\n";
                    }
                }


            }

            TextToFile(dataOutPath + fOutName, unfind + outText);
            SayBlue("\n\n" + outText + "\n");
            SayGreen(unfind + "\n");

            if (unfind != "") SayGreen("\n\tЕсть ненайдненные\n");
            else SayYellow("\n\tВсех нашли\n");
        }




    }
}
