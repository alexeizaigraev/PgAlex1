using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PgAlex1
{
    class Priem : PeoplePapa
    {
        private static string[] agentDataSplit;
        public static void MainPriem()
        {
            ClearInfo();
            string inFileName = "priem.csv";
            string outFileName = "OutPriem.csv";
            string[] lines = FileToVec(dataInPath + inFileName);

            header = "Логин;Пароль;ФИО;Почта;Телефон;Агент;Терминал\n";
            foreach (string line in MkGoodArr(lines))
            {
                workVec = line.Split(';');
                if (workVec.Length != 8) Sos("Bed Size line", line);

                string login = Login(LoginName());
                string agentSign = Terminal().Substring(0, 3);
                agentDataSplit = prichindal(agentSign).Split(';');
                string agent = agentDataSplit[1];
                string mail = MkMail();
                string phone = MkPhone();
                string paspSeria = MkPaspSeria();
                string paspNumber = MkPaspNumber();
                string paspVydan = MkPaspVydan();
                string date = MkDatePriem();

                login += paspNumber.Substring(paspNumber.Length - 4, 4);

                outLine = login + ";" + login + ";" + ShortName() + ";" +
                mail + ";" + phone + ";" + agent + ";" + Terminal() + ";" +
                paspSeria + ";" + paspNumber + ";" + paspVydan + ";" +
                date + "\n";

                outText += outLine;

            }

            info = outText;

            TextToFile(dataOutPath + outFileName, header + outText);
        }

        private static List<string> MkGoodArr(string[] arr)
        {
            List<string> outList = new List<string>();
            foreach (string el in arr)
            {
                if (el.IndexOf('№') > -1)
                    outList.Add(el);
            }
            return outList;
        }


        private static string MkDatePriem()
        {
            string[] dateSplit = new string[3];
            if (
                (workVec[6] != null)
                && (workVec[6] != "")
                && (workVec[6].IndexOf('.') > -1)
                && (workVec[6].LastIndexOf('.') != workVec[6].IndexOf('.'))
                && (workVec[6].Split('.').Length > 2)
                )
                dateSplit = workVec[6].Split('.');
            else
            {
                dateSplit[0] = "01";
                dateSplit[1] = "01";
                dateSplit[2] = "2020";
            }

            string date = dateSplit[2] + "-" +
            dateSplit[1] + "-" + dateSplit[0];
            return date;
        }

        private static string Terminal() { return workVec[1].Split('№')[1].Replace(" ", "") + "1"; }

        private static string LoginName()
        {
            string[] sname = NameFull().Split(' ');
            return sname[1].Substring(0, 2)
                + sname[2].Substring(0, 2)
                + sname[0];
        }

        private static string NameFull()
        {
            string[] nama = workVec[0].Replace("  ", " ").Trim(' ').Split(' ');
            string surname = "";
            string firstName = "";
            string lastName = "";
            //string[3] rez = {"", "", "" };
            string rez = "";
            surname = nama[0].Replace(" ", "");
            firstName = nama[1].Replace(" ", "");
            if (
                    (nama.Length > 2)
                    && (nama[2] != null)
                    && (nama[2] != "")
                    && (nama[2] != " ")
                )
                lastName = nama[2].Replace(" ", "");
            else
                lastName = firstName;

            rez = surname + " " + firstName + " " + lastName;
            return rez;
        }

        private static string ShortName()
        {
            string[] sname = NameFull().Split(' ');
            string surname = sname[0];
            string initiAlOneDot = sname[1].Substring(0, 1) + ".";
            string initiAlTwoDot = sname[2].Substring(0, 1) + ".";
            return surname + " " + initiAlOneDot + " " + initiAlTwoDot;
        }

        private static string Login(string word)
        {
            string myLogin = "";
            string lowWord = word.ToLower();

            Dictionary<char, string> d = new Dictionary<char, string>
            {
                {'а', "a"},
                {'б', "b"},
                {'в', "v"},
                {'г', "g"},
                {'ґ', "gh"},
                {'д', "d"},
                {'е', "e"},
                {'є', "je"},
                {'ж', "zh"},
                {'з', "z"},
                {'и', "i"},
                {'і', "i"},
                {'ї', "ji"},
                {'й', "j"},
                {'к', "k"},
                {'л', "l"},
                {'м', "m"},
                {'н', "n"},
                {'о', "o"},
                {'п', "p"},
                {'р', "r"},
                {'с', "s"},
                {'т', "t"},
                {'у', "u"},
                {'ф', "f"},
                {'х', "h"},
                {'ц', "z"},
                {'ч', "sh"},
                {'ш', "sch"},
                {'щ', "s"},
                {'ь', "j"},
                {'ю', "yu"},
                {'я', "ya"},
                {'`', ""},
                {'-', ""}
            };
            foreach (char cc in lowWord)
            {
                try
                {
                    if (d.ContainsKey(cc))
                        myLogin += d[cc];
                }
                catch { }
            }
            return myLogin;
        }

        private static string prichindal(string znak)
        {
            string[] data = File.ReadAllLines(myDataPath);
            string agentData = "Error agent_data";
            foreach (string line in data)
            {
                string dataZnak = line.Split(';')[0];
                if (dataZnak.IndexOf(znak) == 0)
                {
                    agentData = line;
                    break;
                }
            }
            if (agentData == "Error agent_data")
                Sos("AgentDataError. znak: ", znak);
            return agentData;
        }

        private static string MkMail()
        {
            string mail = "";
            if (
                (workVec[3] != null)
                && (workVec[3] != "")
                && (workVec[3].IndexOf('@') > -1)
                )
                mail = workVec[3];
            else
                mail = agentDataSplit[2];

            return mail;
        }

        private static string MkPhone()
        {
            string phone = "no phone";
            if (
                (workVec[2] != null)
                && (workVec[2] != "")
                && (workVec[2].IndexOf('0') > -1)
                && (workVec[2].Length == 13)
                )
                phone = "38" + workVec[2].Replace("-", "");
            else
                phone = "380999999999";

            return phone;
        }

        private static string MkPaspSeria()
        {
            string paspSeria = "";
            if (
                (workVec[4] != null)
                && (workVec[4] != "")
                && (workVec[4].Length > 1)
                )
                paspSeria = workVec[4];
            else
                paspSeria = "id";

            return paspSeria;
        }

        private static string MkPaspNumber()
        {
            string paspNumber = "";
            if (
                (workVec[5] != null)
                && (workVec[5] != "")
                && (workVec[5].Length > 2)
                )
                paspNumber = workVec[5];
            else
                paspNumber = "9999";

            return paspNumber;
        }

        private static string MkPaspVydan()
        {
            string paspVydan;
            if (
                (workVec[7] != null)
                && (workVec[7] != "")
                && (workVec[7].Length > 1)
                )
                paspVydan = workVec[7];
            else
                paspVydan = "9999";

            return paspVydan;
        }
    }
}
