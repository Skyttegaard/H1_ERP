using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Console.WriteLine("Please type a number");
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
                    Console.WriteLine("Please type a number");
                }
            }
        }

        public static string GetStringInput()
        {
            return Console.ReadLine();
        }
    }
}
