using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP.ConsoleCommands;
using H1_ERP.Models;

namespace H1_ERP.Menuer
{
    class OpretVare
    {
        /// <summary>
        /// Tager alle inputs der er nødvendige for at lave en ny vare.
        /// </summary>
        public static void CreateItems()
        {
            
            WriteLineCommands.WriteLineMessage("Please enter an item name");
            string itemname = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Please enter item sales price");
            double salesPrice = ReadLineCommands.GetDoubleInput();
            WriteLineCommands.WriteLineMessage("Please enter item buy price");
            double buyPrice = ReadLineCommands.GetDoubleInput();
            WriteLineCommands.WriteLineMessage("Please enter the storage capacity for this item");
            int storageCapacity = ReadLineCommands.GetIntInput();
            
            Vareliste.AddItemToList(new Item(itemname, salesPrice, buyPrice, storageCapacity));
            
            
        }
    }
}
