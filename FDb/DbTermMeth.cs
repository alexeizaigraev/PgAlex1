using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAlex1
{
    class DbTermMeth : Papa
    {
        internal static void RefreshTerminal()
        {
            #region
            Say("# RefreshTerminal");
            int countOk = 0;
            int countLines = -1;
            try
            {
                DeleteAllTerminal();
            }
            catch (Exception ex) { SayRed(ex.Message); }
            var data = FileToArr(Path.Combine(dataInPath, "terminals.csv"));
            using (var context = new postgresContext())
            {


                foreach (var dataLine0 in data)
                {
                    var dataLine = GoodVec(dataLine0);
                    countLines += 1;
                    if (countLines > 0)
                    {
                        if (dataLine[0] == "") continue;

                        try
                        {
                            var terminal = new Terminal
                            {
                                Department = dataLine[0],
                                Termial = dataLine[1],
                                Model = dataLine[2],
                                SerialNumber = dataLine[3],
                                DateManufacture = dataLine[4],
                                Soft = dataLine[5],
                                Producer = dataLine[6],
                                RneRro = dataLine[7],
                                Sealing = dataLine[8],
                                FiscalNumber = dataLine[9],
                                OroSerial = dataLine[10],
                                OroNumber = dataLine[11],
                                TicketSerial = dataLine[12],
                                Ticket1sheet = dataLine[13],
                                TicketNumber = dataLine[14],
                                Sending = dataLine[15],
                                BooksArhiv = dataLine[16],
                                TicketsArhiv = dataLine[17],
                                ToRro = dataLine[18],
                                OwnerRro = dataLine[19],
                                Register = dataLine[20],
                                Finish = dataLine[21],

                            };

                            context.Terminals.Add(terminal);
                            //Say($"+ term new {terminal.TermialTerm}");
                            countOk += 1;
                        }
                        catch (Exception ex) { SayRed($"{dataLine[0]} {ex.Message}"); }
                    }
                }
                context.SaveChanges();
            }
            SayGreen($"\nadd terminals {countOk} from {countLines}\n {countLines - countOk} erroros\n");
            #endregion
        }

        internal static void DeleteAllTerminal()
        {
            try
            {
                #region
                using (var context = new postgresContext())
                {
                    var terminal = context.Terminals;

                    context.Terminals.RemoveRange(terminal);
                    context.SaveChanges();
                }
                #endregion
                Say("\nDeleteAllTerminal");
            }
            catch (Exception ex) { SayRed($"\nDeleteAllTerminal {ex.Message}"); }
        }

        internal static void DeleteOneTerminal(string term)
        {
            #region
            try
            {
                using (var context = new postgresContext())
                {
                    var singleRec = context.Terminals.FirstOrDefault(x => x.Termial == term);// object your want to delete
                    context.Terminals.Remove(singleRec);
                    context.SaveChanges();
                    Say($"del {term}");
                }
            }
            catch (Exception ex) { SayRed($"{term} {ex.Message}"); }
            #endregion
        }


        public static List<List<string>> GetAllTerm()
        {
            #region
            var outList = new List<List<string>>();
            using (var context = new postgresContext())
            {
                var terminal = context.Terminals;
                var w = terminal.ToList();
                foreach (var line in w)
                {
                    var lineVec = new List<string> {
                        line.Department,
                        line.Termial,
                        line.Model,
                        line.SerialNumber,
                        line.DateManufacture,
                        line.Soft,
                        line.Producer,
                        line.RneRro,
                        line.Sealing,
                        line.FiscalNumber,
                        line.OroSerial,
                        line.OroNumber,
                        line.TicketSerial,
                        line.Ticket1sheet,
                        line.TicketNumber,
                        line.Sending,
                        line.BooksArhiv,
                        line.TicketsArhiv,
                        line.ToRro,
                        line.OwnerRro,
                        line.Register,
                        line.Finish
                    };
                    outList.Add(lineVec);
                }

            }
            return outList;
            #endregion
        }

        public static List<string> GetOneTerm(string myTerm)
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new postgresContext())
            {
                var terminal = context.Terminals;
                //var w = department.ToList();

                var lingVar = from term in terminal
                              where term.Termial == myTerm
                              select
                              new
                              {
                                  DepartmentTerm = term.Department,
                                  TermialTerm = term.Termial,
                                  ModelTerm = term.Model,
                                  SerialNumberTerm = term.SerialNumber,
                                  DateManufactureTerm = term.DateManufacture,
                                  SoftTerm = term.Soft,
                                  ProducerTerm = term.Producer,
                                  RneRroTerm = term.RneRro,
                                  SealingTerm = term.Sealing,
                                  FiscalNumberTerm = term.FiscalNumber,
                                  OroSerialTerm = term.OroSerial,
                                  OroNumberTerm = term.OroNumber,
                                  TicketSerialTerm = term.TicketSerial,
                                  Ticket1sheetTerm = term.Ticket1sheet,
                                  TicketNumberTerm = term.TicketNumber,
                                  SendingTerm = term.Sending,
                                  BooksArhivTerm = term.BooksArhiv,
                                  TicketsArhivTerm = term.TicketsArhiv,
                                  ToRroTerm = term.ToRro,
                                  OwnerRroTerm = term.OwnerRro,
                                  RegisterTerm = term.Register,
                                  FinishTerm = term.Finish
                              };
                foreach (var term in lingVar)
                {
                    List<string> vec = new List<string>();

                    vec.Add(term.DepartmentTerm);
                    vec.Add(term.TermialTerm);
                    vec.Add(term.ModelTerm);
                    vec.Add(term.SerialNumberTerm);
                    vec.Add(term.DateManufactureTerm);
                    vec.Add(term.SoftTerm);
                    vec.Add(term.ProducerTerm);
                    vec.Add(term.RneRroTerm);
                    vec.Add(term.SealingTerm);
                    vec.Add(term.FiscalNumberTerm);
                    vec.Add(term.OroSerialTerm);
                    vec.Add(term.OroNumberTerm);
                    vec.Add(term.TicketSerialTerm);
                    vec.Add(term.Ticket1sheetTerm);
                    vec.Add(term.TicketNumberTerm);
                    vec.Add(term.SendingTerm);
                    vec.Add(term.BooksArhivTerm);
                    vec.Add(term.TicketsArhivTerm);
                    vec.Add(term.ToRroTerm);
                    vec.Add(term.OwnerRroTerm);
                    vec.Add(term.RegisterTerm);
                    vec.Add(term.FinishTerm);

                    outList.Add(vec);
                }
            };
            return outList[0];
            #endregion
        }


        internal static void AddOneTerminal(List<string> vec)
        {
            #region
            string dep = vec[0];
            Say("# AddOneTerminal");
            DeleteOneTerminal(dep);
            using (var context = new postgresContext())
            {
                var terminal = new Terminal
                {
                    Department = vec[0],
                    Termial = vec[1],
                    Model = vec[2],
                    SerialNumber = vec[3],
                    DateManufacture = vec[4],
                    Soft = vec[5],
                    Producer = vec[6],
                    RneRro = vec[7],
                    Sealing = vec[8],
                    FiscalNumber = vec[9],
                    OroSerial = vec[10],
                    OroNumber = vec[11],
                    TicketSerial = vec[12],
                    Ticket1sheet = vec[13],
                    TicketNumber = vec[14],
                    Sending = vec[15],
                    BooksArhiv = vec[16],
                    TicketsArhiv = vec[17],
                    ToRro = vec[18],
                    OwnerRro = vec[19],
                    Register = vec[20],
                    Finish = vec[21]
                };

                context.Terminals.Add(terminal);
                context.SaveChanges();
                Say($"{terminal.Termial}");
            }
            #endregion
        }


        internal static List<string> GetLisTerm()
        {
            #region
            var outList = new List<string>();
            using (var context = new postgresContext())
            {
                var terminal = context.Terminals;
                var w = terminal.ToList();
                foreach (var line in w)
                {
                    outList.Add(line.Termial);
                }

            }
            return outList;
            #endregion
        }


        public static string NextTerm(string term)
        {
            #region
            string outTerm = "";
            var terms = GetLisTerm();
            int ind = terms.IndexOf(term);
            if (ind < terms.Count - 1) { outTerm = terms[ind + 1]; }
            else { outTerm = terms[0]; }
            info = outTerm;
            return outTerm;
            #endregion
        }

        public static string PredTerm(string term)
        {
            #region
            string rez = "";
            var vec = GetLisTerm();
            int ind = vec.IndexOf(term);
            if (ind > 0) { rez = vec[ind - 1]; }
            else { rez = vec[vec.Count - 1]; }
            return rez;
            #endregion
        }
    }
}
