using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PgAlex1
{
    //class Monitor : Papa
    class Monitor : Papa
    {
        public static void MainMonitor()
        {
            string[] monitorDirs = File.ReadAllLines(dataConfigDirPath + "PathMonitor.txt");
            string monitorOut = File.ReadAllLines(dataConfigDirPath + "PathMonitorOut.txt")[0];
            foreach (string dir in monitorDirs)
            {

                Say(dir);
                Mover(dir, monitorOut);
            }
        }

    }
}
