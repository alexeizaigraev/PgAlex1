using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    internal class OtborFromFile : Papa
    {
        internal static void MainOtborFromFile()
        {
            try
            {
                DbOtborMet.RefreshOtborFromFile();
                //info += $"success MainOtborFromFile\n";
            }
            catch (Exception ex) { info += ex.Message + "\n"; }

            /*
            try
            {
                info += $"{OtborToText()}\n";
            }
            catch (Exception ex) { info += ex.Message + "\n"; }
            */
        }
    }
}
