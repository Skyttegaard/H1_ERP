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
        static List<string> menutest = new();
        static void Main(string[] args)
        {
            Vareliste.CreateItem("Testing", 8000, 10000, 99);
            Initialize();
            ChooseStarterMenu(WriteLineCommands.Menu(menutest, "Luk programmet"));
        }


        private static void Initialize()
        {
            menutest.Add("Vareliste");
            menutest.Add("Varebestilling");
        }
        private static void ChooseStarterMenu(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    //Metode til at køre vareliste herfra.
                    Console.Clear();
                    Console.WriteLine("Vareliste:");
                    RunVareliste();
                    break;
                case 2:
                    //Metode til varebestilling
                    Console.WriteLine("Varebestilling");
                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
        private static void ChooseVarelisteMenu(int input)
        {
            switch (input)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Opret vare");
                    OpretVare.CreateItems();
                    ChooseStarterMenu(1);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Ændre vare");
                    ChangeVare();
                    ChooseStarterMenu(1);
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Søg vare");
                    SearchVare();
                    ChooseStarterMenu(1);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Fjern vare");
                    RemoveVare();
                    ChooseStarterMenu(1);
                    break;
                case 9:
                    Console.Clear();
                    ChooseStarterMenu(WriteLineCommands.Menu(menutest, "Luk programmet"));
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
        private static void RemoveVare()
        {
            Console.WriteLine("Indtast vare ID");
            int ID;
            while (true)
            {
                ID = ReadLineCommands.GetIntInput();
                if (Vareliste.Varer.FirstOrDefault(i => i.ItemID == ID) == null)
                {
                    Console.WriteLine("Enter a valid item ID");
                }
                else
                {
                    break;
                }
            }
            Item item = Vareliste.Varer.First(i => i.ItemID == ID);
            Console.WriteLine($"Er du sikker på at du vil fjerne {item.ItemName}?\nTast 'y' hvis ja.");
            string answer = ReadLineCommands.GetStringInput();
            answer = answer.ToLower();
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
            int i = 0;
            foreach(Item item in Vareliste.Varer)
            {
                
                if (item.ItemName.Contains(input, StringComparison.OrdinalIgnoreCase))
                {
                    WriteLineCommands.WriteLineMessage($"Name: {item.ItemName}   ID: {item.ItemID} ");
                    i++;
                }
            }
            WriteLineCommands.WriteLineMessage($"{i} results");
            WriteLineCommands.WritelineWaitForKeyPress("\nTryk enter for at returnere til varelisten.");
        }
        private static void ChangeVare()
        {
            WriteLineCommands.WriteLineMessage("Indtast vare ID");
            int ID;
            while (true)
            {
                ID = ReadLineCommands.GetIntInput();
                if (Vareliste.Varer.FirstOrDefault(i => i.ItemID == ID) == null)
                {
                    WriteLineCommands.WriteLineMessage("Enter a valid item ID");
                }
                else
                {
                    break;
                }
            }
            Item item = Vareliste.Varer.First(i => i.ItemID == ID);
            WriteLineCommands.WriteLineMessage("Indtast nyt navn på vare");
            item.ItemName = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Indtast ny salgs pris");
            item.ItemSalesPrice = ReadLineCommands.GetDoubleInput();
            WriteLineCommands.WriteLineMessage("Indtast ny købspris");
            item.ItemBuyPrice = ReadLineCommands.GetDoubleInput();
        }
        private static void RunVareliste()
        {
            WriteLineCommands.RunVareListe();
            ChooseVarelisteMenu(WriteLineCommands.Menu(VarelisteMenu.MenuOptions, "Tilbage til mainmenu"));
        }
        

        
    }
}
