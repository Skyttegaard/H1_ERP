﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP.Models;

namespace H1_ERP.ConsoleCommands
{
    class ReadLineCommands
    {
        public static int GetIntInput()
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                try
                {
                    return Convert.ToInt32(input);
                }
                catch
                {
                    Console.WriteLine("Indtast et tal");
                }
            }
        }
        public static double GetDoubleInput()
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                try
                {
                    return Convert.ToDouble(input);
                }
                catch
                {
                    Console.WriteLine("Indtast et tal");
                }
            }
        }
        

        public static string GetStringInput()
        {
            return Console.ReadLine();
        }
    }
}
