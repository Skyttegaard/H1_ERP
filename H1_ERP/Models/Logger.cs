using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace H1_ERP.Models
{
    class Logger
    {
        private static void logThis(string type, string text)
        {
            string message;
            message = string.Format("[{0}] [{1}] [{2}]\n",
            DateTime.Now.ToString(),
            type,
            text);
            File.AppendAllText(".\\program.log", message);
        }
        public static void Info(string text)
        {
            logThis("INFO", text); 
        }
        public static void Error(string errorMessage)
        {
            logThis("ERROR", errorMessage);
        }
        
    }
}
