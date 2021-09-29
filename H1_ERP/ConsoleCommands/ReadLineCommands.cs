using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP.Models;

namespace H1_ERP.ConsoleCommands
{
    class ReadLineCommands
    {
        /// <summary>
        /// Laver readline input string om til int. 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Laver readline input string om til double. 
        /// </summary>
        /// <returns></returns>
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
        public static double GetDoubleOrNothingInput()
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    return 0;
                }
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
        
        /// <summary>
        /// Returnere string fra console.
        /// </summary>
        /// <returns></returns>
        public static string GetStringInput()
        {
            return Console.ReadLine();
        }
        /// <summary>
        /// Returnere et tal fra readline som er mellem 1000 og 9999.
        /// </summary>
        /// <param name="originalInput"></param>
        /// <returns></returns>
        public static int GetPostNummer(int originalInput = 0)
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) && originalInput != 0)
                {
                    return originalInput;
                }
                try
                {
                    int i = Convert.ToInt32(input);
                    if(i > 1000 && i < 9999)
                    {
                        return i;
                    }
                    else
                    {
                        throw new Exception();
                    }
                    
                    
                }
                catch
                {
                    Console.WriteLine("Indtast et tal, som er 4 tal langt.");
                }
            }
        }
        /// <summary>
        /// Returnere original string hvis input er tomt. Returnere den skrevne string hvis input ikke er tomt.
        /// </summary>
        /// <param name="originalInput"></param>
        /// <returns></returns>
        public static string GetKundeInfo(string originalInput)
        {
            string s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s))
            {
                s = originalInput;
            }
            return s;
        }
        /// <summary>
        /// Returnere original int hvis input er tomt. Returnere den skrevne int hvis input ikke er tomt.
        /// </summary>
        /// <param name="originalInput"></param>
        /// <returns></returns>
        public static int GetKundeInfo(int originalInput)
        {
            string input;
            while (true)
            {
                
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return originalInput;
                }
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
    }
}
