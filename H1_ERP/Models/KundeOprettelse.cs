using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H1_ERP.ConsoleCommands;

namespace H1_ERP.Models
{
    class KundeOprettelse
    {
        private static List<string> _menuOptions = new() { "Opret kunde", "Rediger kunde", "Søg kunde" };
        public static IReadOnlyList<string> MenuOptions => _menuOptions.AsReadOnly();
        private static List<Kunde> _kunder = new();
        public static IReadOnlyList<Kunde> GetList() => _kunder.AsReadOnly();

        public static void CreateNewCustomer()
        {
            WriteLineCommands.WriteLineMessage("Indtast fornavn");
            string fornavn = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Indtast efternavn");
            string efternavn = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Indtast by");
            string by = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Indtast postnummer");
            int postNummer = ReadLineCommands.GetPostNummer();
            WriteLineCommands.WriteLineMessage("Indtast vejnavn");
            string vejNavn = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Indtast husnummer");
            string husNummer = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Indtast person id");
            int personId = ReadLineCommands.GetIntInput();
            WriteLineCommands.WriteLineMessage("Indtast email");
            string email = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Indtast telefonnummer");
            int telefonNummer = ReadLineCommands.GetIntInput();
            WriteLineCommands.WriteLineMessage("Indtast en kontakt besked her:");
            string tekst = ReadLineCommands.GetStringInput();
            _kunder.Add(new Kunde(fornavn, efternavn, personId, email, telefonNummer, tekst, vejNavn, husNummer, postNummer, by));

        }
        public static Kunde ReturnFromID(int kundeNummer)
        {
            return _kunder.FirstOrDefault(ku => ku.Kundenummer == kundeNummer);
        } 

        public static void EditCustomer(int kundeNummer)
        {
            Kunde kunde = _kunder.FirstOrDefault(ku => ku.Kundenummer == kundeNummer);
            if(kunde == null)
            {
                WriteLineCommands.WriteLineMessage("Internal error");
                Thread.Sleep(5000);
            }
            WriteLineCommands.WriteLineMessage("Indtast fornavn");
            kunde.Fornavn = ReadLineCommands.GetKundeInfo(kunde.Fornavn);
            WriteLineCommands.WriteLineMessage("Indtast efternavn");
            kunde.Efternavn = ReadLineCommands.GetKundeInfo(kunde.Efternavn);
            WriteLineCommands.WriteLineMessage("Indtast by");
            kunde.Adresse.By = ReadLineCommands.GetKundeInfo(kunde.Adresse.By);
            WriteLineCommands.WriteLineMessage("Indtast postnummer");
            kunde.Adresse.PostNummer = ReadLineCommands.GetPostNummer(kunde.Adresse.PostNummer);
            WriteLineCommands.WriteLineMessage("Indtast vejnavn");
            kunde.Adresse.Vejnavn = ReadLineCommands.GetKundeInfo(kunde.Adresse.Vejnavn);
            WriteLineCommands.WriteLineMessage("Indtast husnummer");
            kunde.Adresse.HusNummer = ReadLineCommands.GetKundeInfo(kunde.Adresse.HusNummer);
            WriteLineCommands.WriteLineMessage("Indtast person id");
            kunde.PersonId = ReadLineCommands.GetKundeInfo(kunde.PersonId);
            WriteLineCommands.WriteLineMessage("Indtast email");
            kunde.Kontakt.Email = ReadLineCommands.GetKundeInfo(kunde.Kontakt.Email);
            WriteLineCommands.WriteLineMessage("Indtast telefonnummer");
            kunde.Kontakt.TelefonNummer = ReadLineCommands.GetKundeInfo(kunde.Kontakt.TelefonNummer);
        }
        /// <summary>
        /// Søger efter kunder og returnere en liste
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<Kunde> SearchCustomer(string input)
        {
            List<Kunde> tempList = new();
            try
            {
                int i = Convert.ToInt32(input);
                foreach(Kunde kunde in _kunder)
                {
                    if (kunde.Kundenummer.ToString().Contains(input))
                    {
                        tempList.Add(kunde);
                    }
                }

            }
            catch
            {
                foreach(Kunde kunde in _kunder)
                {
                    if (kunde.Fornavn.Contains(input, StringComparison.OrdinalIgnoreCase) || kunde.Efternavn.Contains(input, StringComparison.OrdinalIgnoreCase))
                    {
                        tempList.Add(kunde);
                    }
                }
            }
            return tempList;
        }
        
    }
}
