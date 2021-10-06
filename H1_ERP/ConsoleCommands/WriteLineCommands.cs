using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP.Models;
using H1_ERP.Factories;

namespace H1_ERP.ConsoleCommands
{
    class WriteLineCommands
    {
        /// <summary>
        /// Printer en menu med valg fra en liste med strings. Exit tekst til den 9. og sidste menu mulighed som bliver brugt til at gå tilbage. Returnere et tal.
        /// </summary>
        /// <param name="menulist"></param>
        /// <param name="exitText"></param>
        /// <returns></returns>
        public static int Menu(IReadOnlyList<string> menulist, string exitText)
        {
            int i = 1;
            foreach(string s in menulist)
            {
                Console.WriteLine($"{i}. {s}");
                i++;
            }
            Console.WriteLine($"9. {exitText}");
            Console.WriteLine("\nChoose an item from the menu");
            return MenuItemPicker(menulist);
            
        }
        /// <summary>
        /// Returnere et tal som skal passe med længden af listen som bliver brugt i Menu.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static int MenuItemPicker(IReadOnlyList<string> list)
        {
            while (true)
            {
                int input = ReadLineCommands.GetIntInput();
                if(input > list.Count && input != 9 || input < 1 )
                {
                    Console.WriteLine("Vælg et tal fra listen");
                }
                else
                {
                    return input;
                }
            }
        }
        /// <summary>
        /// Skriver en ny linje
        /// </summary>
        public static void NewLine()
        {
            Console.WriteLine();
        }
        /// <summary>
        /// Skriver en writeline i console.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLineMessage(string message)
        {
            Console.WriteLine(message);
        }
        /// <summary>
        /// Skriver en write i console.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteMessage(string message)
        {
            Console.Write(message);
        }
        /// <summary>
        /// Skriver en selvvalgt mængde af '-' i console.
        /// </summary>
        /// <param name="length"></param>
        public static void WriteBars(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("-");
            }
            NewLine();
        }
        /// <summary>
        /// Printer en liste med item objekt i console.
        /// </summary>
        /// <param name="list"></param>
        public static void RunVareListe(IReadOnlyList<Item> list)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Vare navn:");
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("Vare salgs pris:");
            Console.SetCursorPosition(40, 2);
            Console.WriteLine("Vare købs pris:");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine("Vare ID:");
            Console.SetCursorPosition(80, 2);
            Console.WriteLine("Vare mængde:");
            int i = 4;
            foreach (var item in list)
            {
                
                Console.SetCursorPosition(0, i);
                Console.Write(item.ItemName);
                Console.SetCursorPosition(20, i);
                Console.Write(item.ItemSalesPrice);
                Console.SetCursorPosition(40, i);
                Console.Write(item.ItemBuyPrice);
                Console.SetCursorPosition(60, i);
                Console.Write(item.ItemID);
                Console.SetCursorPosition(80, i);
                Console.Write(item.Quantity);
                i++;
            }
            NewLine();
            WriteBars(120);
            NewLine();
        }
        public static void RunBestillingsListe(IReadOnlyList<Bestillinger> list)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Vare navn:");
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("Vare lokation:");
            Console.SetCursorPosition(40, 2);
            Console.WriteLine("Antal bestilte:");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine("OrderID");
            int i = 4;
            foreach (var item in list)
            {

                Console.SetCursorPosition(0, i);
                Console.Write(item.ItemName);
                Console.SetCursorPosition(20, i);
                Console.Write(item.Lokation);
                Console.SetCursorPosition(40, i);
                Console.Write(item.Antal);
                Console.SetCursorPosition(60, i);
                Console.Write(item.OrderID);
                i++;
            }
            NewLine();
            WriteBars(120);
            NewLine();
        }
        /// <summary>
        /// Printer writeline message ud i console og venter på keypress.
        /// </summary>
        /// <param name="message"></param>
        public static void WritelineWaitForKeyPress(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
        /// <summary>
        /// Printer en liste med kunde objekt i console.
        /// </summary>
        /// <param name="list"></param>
        public static void RunKundeListe(IReadOnlyList<Kunde> list)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Kundenummer:");
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("Fornavn:");
            Console.SetCursorPosition(40, 2);
            Console.WriteLine("Efternavn:");
            Console.SetCursorPosition(100, 2);
            Console.WriteLine("Seneste ordredato");
            int i = 4;
            foreach(var kunde in list)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine(kunde.Kundenummer);
                Console.SetCursorPosition(20, i);
                Console.WriteLine(kunde.Fornavn);
                Console.SetCursorPosition(40, i);
                Console.WriteLine(kunde.Efternavn);
                Console.SetCursorPosition(100, i);
                Console.WriteLine(kunde.SenesteOrdreDato);
                i++;
            }
            NewLine();
            WriteBars(120);
            NewLine();
        }
        public static Salgsordre CreateNewOrder()
        {
            Console.WriteLine("Indtast kunde nummer");
            Kunde kunde = KundeOprettelse.ReturnFromID(ReadLineCommands.GetIntInput());
            int ordreid = DatabaseFactory.CreateOrdre(kunde.Kundenummer, kunde.Adresse.By);
            Salgsordre salgsordre = new(kunde, ordreid);
            return salgsordre;
            
        }
        public static Salgsordre RunSalgsOrdreListe(Salgsordre salgsordre)
        {
            Console.Clear();
            Console.WriteLine($"Ordre ID: {salgsordre.Ordreid}");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("KontaktInfo: ");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine("Addresse:");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"{salgsordre.Kunde.Fornavn} {salgsordre.Kunde.Efternavn}");
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(salgsordre.Kunde.Kontakt.Email);
            Console.SetCursorPosition(0, 5);
            Console.WriteLine(salgsordre.Kunde.Kontakt.TelefonNummer);
            Console.SetCursorPosition(60, 3);
            Console.WriteLine(salgsordre.Kunde.Adresse.By);
            Console.SetCursorPosition(60, 4);
            Console.WriteLine($"{salgsordre.Kunde.Adresse.Vejnavn} {salgsordre.Kunde.Adresse.HusNummer}");
            Console.SetCursorPosition(60, 5);
            Console.WriteLine(salgsordre.Kunde.Adresse.PostNummer);
            NewLine();
            WriteBars(120);
            NewLine();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Varenummer:");
            Console.SetCursorPosition(20, 10);
            Console.WriteLine("Varenavn:");
            Console.SetCursorPosition(40, 10);
            Console.WriteLine("Antal:");
            Console.SetCursorPosition(60, 10);
            Console.WriteLine("Pris:");
            int i = 11;
            foreach (Bestillinger bestilling in Varebestilling.GetList().Where(be => be.OrderID == salgsordre.Ordreid))
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine(bestilling.VareID);
                Console.SetCursorPosition(20, i);
                Console.WriteLine(bestilling.ItemName);
                Console.SetCursorPosition(40, i);
                Console.WriteLine(bestilling.Antal);
                Console.SetCursorPosition(60, i);
                Console.WriteLine(bestilling.GetPrisXAntal());
                i++;
            }
            NewLine();
            return salgsordre;
        }
    }
}
