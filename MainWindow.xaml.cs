using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace PgAlex1
{
    public partial class MainWindow : Window
    {
        private static bool DepInTextBoxChoised = true;

        delegate void delegateMemu();

        void WorkMenu(delegateMemu parDelegate)
        {
            #region
            ClearMe();
            try
            {
                parDelegate();
                textBox1.Text = Papa.info;
                textBoxFnames.Text = Papa.infoFname;
                textBoxErr.Text = Papa.infoErr;
            }
            catch (Exception ex) { textBox1.Text = ex.Message; }

            #endregion
        }

        public MainWindow()
        {
            #region #Init
            InitializeComponent();
            Papa.MainKind = "terminals";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var items = new List<string>() { "item1", "item2" };
            listBox1.ItemsSource = items;

            textBox1.Text = "";
            try
            {
                listBox1.ItemsSource = DbTermMeth.GetLisTerm();
            }
            catch (Exception ex) { textBox1.Text += ex.Message + "\n"; }
            labeInfo.Content = Papa.MainKind;
            #endregion
        }

        private void ClearMe()
        {
            #region
            textBox1.Text = "";
            textBoxFnames.Text = "";
            textBoxErr.Text = "";
            Papa.info = "";
            Papa.infoFname = "";
            Papa.infoErr = "";
            #endregion
        }

        private void ButtonChoise_Click(object sender, RoutedEventArgs e)
        {
            string txt = "";
            if (Papa.MainKind == "terminals")
            {
                #region
                try
                {
                    ClearMe();
                    Papa.selectedItems = new List<string[]>();
                    var terms = listBox1.SelectedItems;
                    foreach (var item in terms)
                    {
                        var term = item.ToString();
                        var dep = term.Substring(0, 7);
                        var line = $"{term};{dep}".Split(';'); 

                        Papa.selectedItems.Add(line);
                        txt += $"{term} {dep}\n";
                    }
                    DbOtborMet.AddManyOtbor(Papa.selectedItems);
                    textBox1.Text = txt;
                    listBox1.SelectedItems.Clear();
                    labeInfo.Content = Papa.MainKind;
                }
                catch (Exception ex) { textBox1.Text = ex.Message + "\n"; }
                #endregion
            }
            else if (Papa.MainKind == "partners")
            {
                #region
                try
                {
                    Papa.selectedItem = listBox1.SelectedItem.ToString();
                    textBox1.Text = Papa.selectedItem;
                    Papa.partnerChoised = Papa.selectedItem;
                    /*
                    listBox1.ItemsSource = PgBase.GetListTermPartner();
                    Papa.MainKind = "terminals";
                    */

                    listBox1.ItemsSource = DbBase.GetTermsPartner(Papa.partnerChoised);
                    Papa.MainKind = "departments";

                    labeInfo.Content = Papa.MainKind;
                }
                catch (Exception ex) { textBox1.Text = ex.Message + "\n"; }
                listBox1.SelectedItems.Clear();
                #endregion
            }
            else if (Papa.MainKind == "departments")
            {
                #region
                try
                {
                    ClearMe();
                    Papa.selectedItems = new List<string[]>();
                    var deps = listBox1.SelectedItems;
                    List<string> outArr = new List<string>();
                    foreach (var item in deps)
                    {
                        var dep = item.ToString();
                        var term = $"{dep}1";
                        var line = $"{term};{dep}".Split(';');
                        outArr.Add(dep);
                        Papa.selectedItems.Add(line);
                        //textBox1.Text += dep + "\n";
                    }
                    DbOtborMet.AddManyOtbor(Papa.selectedItems);
                    //textBox1.Text = Papa.info;
                    textBox1.Text = String.Join(" ", outArr);
                    listBox1.SelectedItems.Clear();
                    labeInfo.Content = Papa.MainKind;
                }
                catch (Exception ex) { textBox1.Text = ex.Message + "\n"; }
                #endregion
            }

        }

        private void Col1Terminals_Click(object sender, RoutedEventArgs e)
        {
            Papa.MainKind = "terminals";
            listBox1.ItemsSource = DbTermMeth.GetLisTerm();
            labeInfo.Content = Papa.MainKind;
        }

        private void Col1Departments_Click(object sender, RoutedEventArgs e)
        {
            Papa.MainKind = "departments";
            listBox1.ItemsSource = DbDepMeth.GetListDep();
            labeInfo.Content = Papa.MainKind;
        }

        private void Col1Partners_Click(object sender, RoutedEventArgs e)
        {
            Papa.MainKind = "partners";
            listBox1.ItemsSource = DbDepMeth.GetLisPartner();
            labeInfo.Content = Papa.MainKind;
        }

        private void Col1Folders_Click(object sender, RoutedEventArgs e)
        {
            Papa.MainKind = "folders";
            listBox1.ItemsSource = Papa.MkGdriveFolders();
            labeInfo.Content = Papa.MainKind;
        }

        

        private void Priem_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Priem.MainPriem;
            WorkMenu(myDelegatemenu);
            #endregion
        }


        //people__________________
        private void Otpusk_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Otpusk.MainOtpusk;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Perevod_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Perevod.MainPerevod;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void PostAll_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = PostAll.MainPostAll;
            WorkMenu(myDelegatemenu);
            #endregion
        }


        //some______________________________

        private void Term_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Term.MainTerm;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void HrOtbor_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = HrDep.MainHrDep;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void SummuryAb_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = HrDepAb.MainHrDepAb;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Natasha_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Natasha.MainNatasha;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Site_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = SiteNew.MainSite;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        

        //monitor________________________________
        private void Monitor_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Monitor.MainMonitor;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Rasklad_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            try
            {
                Rasklad.MainRasklad();
                textBox1.Text = String.Join(" ", Rasklad.arr);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try { textBox1.Text = String.Join(" ", Rasklad.arr); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try { listBox1.ItemsSource = Rasklad.arr; }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            #endregion
        }

        private void AccBack_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = AccBack.MainAccBack;
            //myDelegatemenu = AccessBackUp.MainAccessBackUp;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void GnetzCopy_Click(object sender, RoutedEventArgs e)
        {
            #region
            Papa.gnetzKind = "copy";
            delegateMemu myDelegatemenu;
            myDelegatemenu = Gnetz.MainGnetz;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void GnetzMovy_Click(object sender, RoutedEventArgs e)
        {
            #region
            Papa.gnetzKind = "movy";
            delegateMemu myDelegatemenu;
            myDelegatemenu = Gnetz.MainGnetz;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void GdriveBackUp_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = GdriveBackUp.MainGdriveBackUp;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Rp_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            try
            {
                GetRp.MainGetRp();
            }
            catch { Papa.info += " no-no :)"; }
            textBox1.Text = Papa.info;
            #endregion
        }


        //kabinet_______________________________
        /*
        private void Rro_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Rro.MainRro;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Pereezd_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Pereezd.MainPereezd;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Otmena.MainOtmena;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Prro_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Prro.MainPrro;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        private void Knigi_Click(object sender, RoutedEventArgs e)
        {
            #region
            delegateMemu myDelegatemenu;
            myDelegatemenu = Knigi.MainKnigi;
            WorkMenu(myDelegatemenu);
            #endregion
        }

        */

        //show & windows ______________________________________

        
        private void ButtonOtborShow_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            textBox1.Text += "\n" + DbOtborMet.ShowtOtbor();
            #endregion
        }

        //otbor_____________________________

        private void ButtonOtborInputDep_Click(object sender, RoutedEventArgs e)
        {
            #region
            //ClearMe();
            //Papa.selectedItems = new List<string[]>();
            var inputDeps = textBox1.Text;
            OtborDb.MainOtborDb(inputDeps);
            //ClearMe();
            textBox1.Text = Papa.info;
            //listBox1.SelectedItems.Clear();

            #endregion
        }

        private void ButtonOtborInputDepList_Click(object sender, RoutedEventArgs e)
        {
            #region
            var inputDeps = textBox1.Text;
            OtborHardInputDep.MainOtborHardDep(inputDeps);
            //ClearMe();
            textBox1.Text = Papa.info;
            //listBox1.SelectedItems.Clear();

            #endregion
        }

        private void ButtonOtborInputTermHard_Click(object sender, RoutedEventArgs e)
        {
            #region
            //ClearMe();
            //Papa.selectedItems = new List<string[]>();
            var inputDeps = textBox1.Text;
            OtborHardInput.MainOtborHard(inputDeps);
            //ClearMe();
            textBox1.Text = Papa.info;
            //listBox1.SelectedItems.Clear();
            #endregion
        }

        


        private void ButtonOtborFromFile_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            //OtborFromFile.MainOtborFromFile();
            DbOtborMet.RefreshOtborFromFile();
            textBox1.Text = Papa.info;
            #endregion
        }

        //actual_______________________________________



        

       
        //refresh__________________________________________________

        private void RefreshListBox1_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            try
            {
                listBox1.ItemsSource = DbTermMeth.GetLisTerm();
            }
            catch (Exception ex) { textBox1.Text += ex.Message + "\n"; }
        }

        


        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            #region
            ClearMe();
            Papa.info = "";
            #endregion
        }

        
        private void RefresFromAccess_Click(object sender, RoutedEventArgs e)
        {
            string txt = "";
            try
            {
                Process proc = new Process();

                proc.StartInfo.FileName = @"C:\SharpForPy\SharpForPy.exe"; proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex) { txt += $"\n{ex.Message}\n\n"; }

            DbOtborMet.RefreshOtborFromFile();
            txt += "otbor refreshed\n";
            
            DbTermMeth.RefreshTerminal();
            txt += "term refreshed\n";
            DbDepMeth.RefreshDepartment();
            txt += "dep refreshed\n\n\tall refreshed";
            textBox1.Text = txt;
        }
    }
}
