using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    /// <summary>
    /// Files Read Write
    /// Pathes
    /// </summary>
    partial class Papa
    {
        #region #Fields

        internal static string MainKind = "terminals";
        internal static string gnetzKind = "copy";
        internal static string[] inputDeps;


        internal static List<string[]> selectedItems;
        internal static string selectedItem = "";


        internal static List<string[]> selectedSerial;

        internal static string partnerChoised;

        internal static bool Col1Partners = true;
        internal static bool statusExit = false;

        internal static string info = "";
        internal static string infoFname = "";
        internal static string infoErr = "";

        internal static string selectedFolder;
        protected static string dataPath = DataPath();
        protected static string dataConfigPath = Path.Combine(dataPath, "Config") + "\\";

        protected static string dataConfigDirPath = Path.Combine(DataPath(), "ConfigDir") + "\\";
        protected static string dataInPath = Path.Combine(DataPath(), "InData") + "\\";
        protected static string dataOutPath = Path.Combine(DataPath(), "OutData") + "\\";

        protected static string kabinetPath = FileToVec(dataConfigPath + "ConfigKabinetPath.txt")[0];
        protected static string gDrivePath = FileToVec(Path.Combine(dataConfigPath, "ConfigGdrivePath.txt"))[0];

        protected static string myDataPath = dataConfigPath + "comon_data.csv";

        protected static string outLine = "";
        protected static string outText = "";
        protected static string header = "";
        #endregion

        protected static string[] workVec;

        public static void ClearInfo()
        {
            Papa.info = "";
            Papa.infoFname = "";
            Papa.infoErr = "";
            Papa.outLine = "";
            Papa.outText = "";
        }

        public static void StartProcess(string adresProg)
        {
            try
            {
                Process p = Process.Start(adresProg);
                //Say("start " + adresProg);
                p.WaitForExit(); // Ожидаем завершения процесса
                //SayGray("Process exited");
            }
            catch { Alarm("no start", adresProg); }

        }

        public static void Say(string word)
        {
            #region
            //pGray(word);
            info += word + "\n";
            #endregion
        }

        public static void SayRed(string word)
        {
            #region
            //pRed(word);
            info += ">> " + word + "\n";
            #endregion
        }

        public static void SayGreen(string word)
        {
            #region
            //pGreen(word);
            info += word + "\n";
            #endregion
        }

        public static void SayDarkGreen(string word)
        {
            #region
            //pGreen(word);
            info += word + "\n";
            #endregion
        }

        public static void SayYellow(string word)
        {
            #region
            //pYellow(word);
            info += word + "\n";
            #endregion
        }

        public static void SayGray(string word)
        {
            #region
            //pGray(word);
            info += word + "\n";
            #endregion
        }

        public static void SayDarkGray(string word)
        {
            #region
            //pGray(word);
            info += word + "\n";
            #endregion
        }

        public static void SayMagenta(string word)
        {
            #region
            //pMagenta(word);
            info += word + "\n";
            #endregion
        }

        public static void SayBlue(string word)
        {
            #region
            //pBlue(word);
            info += word + "\n";
            #endregion
        }

        public static void SayCyan(string word)
        {
            #region
            //pCyan(word);
            info += word + "\n";
            #endregion
        }

        protected static void OpenNote(string fileName)
        {
            #region #OpenNote
            try { Process.Start("notepad.exe", fileName); }
            catch { Sos("Err Open Notepad", fileName); }
            #endregion
        }

        protected static void Sos(string error, string fact)
        {
            #region #Sos
            Papa.infoErr = $"{error}\n{fact}";
            statusExit = true;
            /*
            SayYellow("\n\t[Enter] MainMenu");
            Console.ReadKey();
            MenuMain();
            */
            #endregion
        }

        public static void Alarm(string error, string fact)
        {
            #region #Alarm
            Papa.infoErr = $"{error}\n{fact}\n";
            /*
            SayGreen("\n\t" + error + " ");
            SayBlue(fact + "\n");
            */
            #endregion
        }

        public static Dictionary<string, string> FileToDict(int colNum)
        {
            #region #FileToDict
            List<string[]> data = FileToArr(myDataPath);
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (string[] line in data)
            {
                try { d[line[0]] = line[colNum]; }
                catch { Sos("Err FileToDict", line[0]); }
            }
            return d;
            #endregion
        }


        public static Dictionary<string, Dictionary<string, string>> FileToHashTab(string fName, int keyColNum)
        {
            #region #FileToHashTab
            List<string[]> arr = FileToArr(fName);
            string[] head = arr[0];
            List<string[]> list = SubList(arr, 1, arr.Count - 1);
            Dictionary<string, Dictionary<string, string>> hashTab = new Dictionary<string, Dictionary<string, string>>();
            try { hashTab = ArrToHash(head, list, keyColNum); }
            catch { Sos("Err FileToHashTab", fName); }
            return hashTab;
            #endregion
        }


        public static string DataPath()
        {
            #region #DataPath
            string path = "";
            try { path = File.ReadAllLines(Path.Combine("Config", "ConfigDataPath.txt"))[0]; }
            catch { Sos("Err read DataPath", Path.Combine("Config", "ConfigDataPath.txt")); }
            return path;
            #endregion
        }

        public static string[] FileToVec(string fName)
        {
            #region #FileToVec
            FileInfo fileInfo = new FileInfo(fName);
            if (!fileInfo.Exists) Sos("No File", fName);
            string[] vec = File.ReadAllLines(fName);
            return vec;
            #endregion
        }

        public static void TextToFile(string fName, string text)
        {
            #region #TextToFile
            try
            {
                File.WriteAllText(fName, text);
                infoFname += $"{fName}\n";
            }
            catch { Sos("Err TextToFile", fName); }
            #endregion
        }

        public static void ArrToFile(string fName, List<string[]> arr)
        {
            #region #ArrToFile
            string text = ArrToText(arr);
            TextToFile(fName, text);
            #endregion
        }

        public static string ArrToText(List<string[]> arr)
        {
            #region #ArrToText
            string text = "";
            foreach (string[] vec in arr)
            {
                text += String.Join(";", vec) + "\n";
            }
            return text;
            #endregion
        }

        public static List<string[]> FileToArr(string fName)
        {
            #region #FileToArr
            string[] vec;
            List<string[]> arr = new List<string[]>();
            try
            {
                vec = File.ReadAllLines(fName);
                foreach (string vecLine in vec)
                {
                    string[] vecLineSplit = vecLine.Split(';');
                    arr.Add(vecLineSplit);
                }
            }
            catch { Sos("Err FileToArr", fName); }
            return arr;
            #endregion
        }

        public static string FileToText(string fName)
        {
            #region #FileToText
            string text = "";
            try { text = File.ReadAllText(fName); }
            catch { Sos("Err RFileToText", fName); }
            return text;
            #endregion
        }

        public static List<string> MkNatasha()
        {
            #region #MkNatasha
            var arr = FileToArr(Path.Combine(dataInPath, "natasha.csv"));
            List<string> vec = new List<string>();
            string sign = "Відділення № ";
            foreach (var line in arr)
            {
                try
                {
                    foreach (var el in line)
                    {
                        if (el.IndexOf(sign) > -1)
                        {
                            string dep = el.Replace(sign, "").Replace(" ", "");
                            if (vec.IndexOf(dep) < 0) vec.Add(dep);
                        }

                    }
                }
                catch { Sos("Err mkNatasha", "natasha.csv"); }

            }
            vec.Add("1");
            return vec;
            #endregion
        }

        public static Dictionary<string, string> MkComonHash(int keyColNum)
        {
            #region #MkComonHash
            Dictionary<string, string> rez = new Dictionary<string, string>();
            List<string[]> data = new List<string[]>();
            try { data = FileToArr(myDataPath); }
            catch { Sos("Err read", myDataPath); }

            try
            {
                foreach (string[] splitLine in data)
                {
                    rez[splitLine[0]] = splitLine[keyColNum];
                }
            }
            catch { Sos("Err MkComonHash", String.Format("KeyColNum {0}", keyColNum)); }
            return rez;
            #endregion
        }


        public static List<string> MkGdriveFolders()
        {
            #region #GdriveFolders
            var rez = new List<string>();
            //List<string> data = MkComonHash(3).Values;
            foreach (string folder in MkComonHash(3).Values)
            {
                if (folder == "nodata" || folder == "") continue;
                rez.Add(folder);
            }
            return rez;
            #endregion
        }


    }
}
