using System;
using System.Linq;
using System.Collections.Generic;
using H1_ERP.ConsoleCommands;
using H1_ERP.Menuer;
using H1_ERP.Models;

namespace H1_ERP
{
    class Program
    {
        private static List<string> _menuListe = new() {"Vareliste", "Varebestilling" };
        static void Main(string[] args)
        {
            Vareliste.AddItemToList(new Item("Testing", 8000, 10000, 99));
            ChooseStarterMenu(WriteLineCommands.Menu(_menuListe, "Luk programmet"));
        }

        private static void ChooseStarterMenu(int input)
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
                    WriteLineCommands.WriteLineMessage("Varebestilling");
                    WriteLineCommands.RunVareListe(Varebestilling.OrderList);
                    ChooseBestillingMenu(WriteLineCommands.Menu(Varebestilling.MenuOptions, "Tilbage til mainmenu"));
                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    WriteLineCommands.WriteLineMessage("ERROR");
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
                    //Bestil enkelte vare
                    break;
                case 2:
                    OrderAllItems();
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
            WriteLineCommands.WriteLineMessage($"Er du sikker på at du vil fjerne {item.ItemName}?\nTast 'y' hvis ja.");
            string answer = ReadLineCommands.GetStringInput();
            if (answer == "y")
            {
                Vareliste.RemoveItem(item);
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
            WriteLineCommands.WriteLineMessage("Indtast nyt navn på vare");
            string itemName = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Indtast ny salgs pris");
            double salesPrice = ReadLineCommands.GetDoubleInput();
            WriteLineCommands.WriteLineMessage("Indtast ny købspris");
            double buyPrice = ReadLineCommands.GetDoubleInput();
            Item.EditItem(item, itemName, salesPrice, buyPrice);
            WriteLineCommands.WriteLineMessage("Ønsker du at tilføje denne vare til bestillingslisten?\nTast 'y' hvis ja.");
            string answer = ReadLineCommands.GetStringInput();
            if (answer.ToLower() == "y")
            {
                WriteLineCommands.WriteLineMessage("Hvor mange?");
                Varebestilling.AddItemToList(item, ReadLineCommands.GetIntInput());
            }
            

        }
        private static void RunVareliste()
        {
            WriteLineCommands.RunVareListe(Vareliste.GetList());
            ChooseVarelisteMenu(WriteLineCommands.Menu(VarelisteMenu.MenuOptions, "Tilbage til mainmenu"));
            
        }
        private static void OrderAllItems()
        {
            Vareliste.AddMultipleItemsToList(Varebestilling.OrderList);
            Varebestilling.OrderList.Clear();
        }
        private static void OrderSingleItems()
        {

        }
        

        
    }
}
