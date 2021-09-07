﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP.Models;

namespace H1_ERP.ConsoleCommands
{
    class WriteLineCommands
    {
        public static int Menu(List<string> menulist, string exitText)
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
        private static int MenuItemPicker(List<string> list)
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

        public static void NewLine()
        {
            Console.WriteLine();
        }

        public static void WriteLineMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static void WriteBars(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("-");
            }
        }
        public static void RunVareListe(List<Item> list)
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
            WriteBars(100);
            NewLine();
        }
        public static void WritelineWaitForKeyPress(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}