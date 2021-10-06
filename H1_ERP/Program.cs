using System;
using System.Linq;
using System.Collections.Generic;
using H1_ERP.ConsoleCommands;
using H1_ERP.Menuer;
using H1_ERP.Models;
using H1_ERP.Factories;
namespace H1_ERP
{
    class Program
    {
        
        private static List<string> _menuListe = new() {"Vareliste", "Varebestilling", "Kundeliste", "Salgsordre" };
        static void Main(string[] args)
        {
            Logger.Info("Programmet er startet op");
            KundeOprettelse.LoadList();
            Varebestilling.loadlist();
            
            ChooseStarterMenu(WriteLineCommands.Menu(_menuListe, "Luk programmet"));
        }

        private static void ChooseStarterMenu(int input, Salgsordre salgsordre = null)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    Console.Clear();
                    WriteLineCommands.WriteLineMessage("Vareliste:");
                    RunVareliste();
                    break;
                case 2:
                    Console.Clear();
                    WriteLineCommands.WriteLineMessage("Varebestilling");
                    WriteLineCommands.RunBestillingsListe(Varebestilling.GetList());
                    ChooseBestillingMenu(WriteLineCommands.Menu(Varebestilling.MenuOptions, "Tilbage til mainmenu"));
                    break;
                case 3:
                    WriteLineCommands.WriteLineMessage("Kundeliste");
                    WriteLineCommands.RunKundeListe(KundeOprettelse.GetList());
                    ChooseKundeMenu(WriteLineCommands.Menu(KundeOprettelse.MenuOptions, "Tilbage til mainmenu"));
                    break;
                case 4:
                    Console.Clear();
                    if (salgsordre == null)
                    {
                        salgsordre = WriteLineCommands.RunSalgsOrdreListe(WriteLineCommands.CreateNewOrder());
                    }
                    else
                    {
                        WriteLineCommands.RunSalgsOrdreListe(salgsordre);
                    }
                    WriteLineCommands.WriteLineMessage("Salgsordre");
                    ChooseSalgsOrdreMenu(WriteLineCommands.Menu(Salgsordre.SalgsOrdreMenuOptions, "Tilbage til mainmenu"), salgsordre);
                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    WriteLineCommands.WriteLineMessage("ERROR");
                    break;
            }
        }
        private static void ChooseSalgsOrdreMenu(int input, Salgsordre salgsordre = null)
        {
            

            switch (input)
            {
                case 1:
                    ReadLineCommands.OrderVare(salgsordre.Kunde.Kundenummer,salgsordre.Ordreid);
                    ChooseStarterMenu(4, WriteLineCommands.RunSalgsOrdreListe(salgsordre));
                    break;
                case 9:
                    Console.Clear();
                    ChooseStarterMenu(WriteLineCommands.Menu(_menuListe, "Luk programmet"));
                    break;
            }
        }
        private static void ChooseVarelisteMenu(int input)
        {
            switch (input)
            {
                case 1:
                    Console.Clear();
                    WriteLineCommands.WriteLineMessage("Opret vare");
                    OpretVare.CreateItems();
                    ChooseStarterMenu(1);
                    break;
                case 2:
                    Console.Clear();
                    WriteLineCommands.WriteLineMessage("Ændre vare");
                    ChangeVare();
                    ChooseStarterMenu(1);
                    break;
                case 3:
                    Console.Clear();
                    WriteLineCommands.WriteLineMessage("Søg vare");
                    SearchVare();
                    ChooseStarterMenu(1);
                    break;
                case 4:
                    Console.Clear();
                    WriteLineCommands.WriteLineMessage("Fjern vare");
                    RemoveVare();
                    ChooseStarterMenu(1);
                    break;
                case 9:
                    Console.Clear();
                    ChooseStarterMenu(WriteLineCommands.Menu(_menuListe, "Luk programmet"));
                    break;
                default:
                    WriteLineCommands.WriteLineMessage("ERROR");
                    break;
            }
        }
        private static void ChooseBestillingMenu(int input)
        {
            switch (input)
            {
                case 1:
                    OrderSingleItems();
                    ChooseStarterMenu(2); 
                    break;
                case 2:
                    Varebestilling.DeliverAllItems();
                    ChooseStarterMenu(2);
                    break;
                case 9:
                    Console.Clear();
                    ChooseStarterMenu(WriteLineCommands.Menu(_menuListe, "Luk programmet"));
                    break;
                default:
                    WriteLineCommands.WriteLineMessage("ERROR");
                    break;
            }
        }
        private static void ChooseKundeMenu(int input)
        {
            switch (input)
            {
                case 1:
                    Console.Clear();
                    CreateKunde();
                    ChooseStarterMenu(3);
                    break;
                case 2:
                    Console.Clear();
                    EditKunde();
                    ChooseStarterMenu(3);
                    break;
                case 3:
                    Console.Clear();
                    SearchKunde();
                    ChooseStarterMenu(3);
                    break;
                case 9:
                    Console.Clear();
                    ChooseStarterMenu(WriteLineCommands.Menu(_menuListe, "Luk programmet"));
                    break;
                default:
                    WriteLineCommands.WriteLineMessage("ERROR");
                    break;

            }
        }
        private static void RemoveVare()
        {
            WriteLineCommands.WriteLineMessage("Indtast vare ID");
            int ID;
            while (true)
            {
                ID = ReadLineCommands.GetIntInput();
                if (Vareliste.ReturnItemFromID(ID) == null)
                {
                    WriteLineCommands.WriteLineMessage("Indtast et gyldigt ID");
                }
                else
                {
                    break;
                }
            }
            Item item = Vareliste.ReturnItemFromID(ID);
            WriteLineCommands.WriteLineMessage($"Er du sikker på at du vil fjerne {item.ItemName} fra listen?\nTast 'y' hvis ja.");
            string answer = ReadLineCommands.GetStringInput();
            if (answer == "y")
            {
                WriteLineCommands.WriteLineMessage($"Hvor mange vil du fjerne fra listen?");
                int amount = ReadLineCommands.GetIntInput();
                Vareliste.RemoveItem(item, amount);
            }
            else
            {
                return;
            }
            
        }
        private static void SearchVare()
        {
            WriteLineCommands.WriteLineMessage("Indtast søgning");
            string input = ReadLineCommands.GetStringInput();
            int i = Vareliste.PrintSearched(input);
            WriteLineCommands.WriteLineMessage($"{i} resultater");
            WriteLineCommands.WritelineWaitForKeyPress("\nTryk enter for at returnere til varelisten.");
        }
        private static void ChangeVare()
        {
            WriteLineCommands.WriteLineMessage("Indtast vare ID");
            int ID;
            while (true)
            {
                ID = ReadLineCommands.GetIntInput();
                if (Vareliste.ReturnItemFromID(ID) == null)
                {
                    WriteLineCommands.WriteLineMessage("Indtast et gyldigt ID");
                }
                else
                {
                    break;
                }
            }
            Item item = Vareliste.ReturnItemFromID(ID);
            WriteLineCommands.WriteLineMessage($"Indtast nyt navn på vare [Tryk enter for at bruge '{item.ItemName}']");
            string itemName = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage($"Indtast ny salgs pris [Tryk enter for at bruge '{item.ItemSalesPrice}']");
            double salesPrice = ReadLineCommands.GetDoubleOrNothingInput();
            WriteLineCommands.WriteLineMessage($"Indtast ny købspris [Tryk enter for at bruge '{item.ItemBuyPrice}']");
            double buyPrice = ReadLineCommands.GetDoubleOrNothingInput();
            if (string.IsNullOrEmpty(itemName))
            {
                itemName = item.ItemName;
            }
            if(salesPrice == 0)
            {
                salesPrice = item.ItemSalesPrice;
            }
            if(buyPrice == 0)
            {
                buyPrice = item.ItemBuyPrice;
            }
            Item.EditItem(item, itemName, salesPrice, buyPrice);
            Vareliste.UpdateVare(item);
            WriteLineCommands.WriteLineMessage("Ønsker du at tilføje denne vare til bestillingslisten?\nTast 'y' hvis ja.");
            string answer = ReadLineCommands.GetStringInput();
            if (answer.ToLower() == "y")
            {
                Varebestilling.AddItemToBestillingList(item);
            }
            

        }
        private static void RunVareliste()
        {
            WriteLineCommands.RunVareListe(Vareliste.GetList());
            ChooseVarelisteMenu(WriteLineCommands.Menu(VarelisteMenu.MenuOptions, "Tilbage til mainmenu"));
            
        }
        
        private static void OrderSingleItems()
        {
            WriteLineCommands.WriteLineMessage("Skriv ID på ordren der er bestilt hjem.");
            int ID;
            while (true)
            {
                ID = ReadLineCommands.GetIntInput();
                if (Varebestilling.ReturnItemFromID(ID) == null)
                {
                    WriteLineCommands.WriteLineMessage("Indtast et gyldigt ID");
                }
                else
                {
                    break;
                }
            }
            Varebestilling.DeliverSingleItem(ID);

        }
        
        private static void CreateKunde()
        {
            KundeOprettelse.CreateNewCustomer();
        }
        private static void EditKunde()
        {
            int ID;
            WriteLineCommands.WriteLineMessage("Indtast kunde nummeret på den kunde du vil redigere");
            while (true)
            {
                ID = ReadLineCommands.GetIntInput();
                if (KundeOprettelse.ReturnFromID(ID) == null)
                {
                    WriteLineCommands.WriteLineMessage("Indtast et gyldigt ID");
                }
                else
                {
                    break;
                }
            }
            KundeOprettelse.EditCustomer(ID);
        }
        private static void SearchKunde()
        {
            WriteLineCommands.WriteLineMessage("Indtast søgning: ");
            WriteLineCommands.RunKundeListe(KundeOprettelse.SearchCustomer(ReadLineCommands.GetStringInput()));
            WriteLineCommands.WritelineWaitForKeyPress("Tryk på en tast for at fortsætte til menuen igen.");
        }

        
    }
}
