using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H1_ERP.ConsoleCommands;

namespace H1_ERP.Models
{
    class Vareliste
    {
        private static List<Item> _varer = new();

        public static void CreateItem(string name, double buyPrice, double salesPrice, int ID)
        {
            _varer.Add(new Item(name, salesPrice, buyPrice, ID));
        }
        public static void RemoveItem(Item item)
        {
            _varer.Remove(item);
        }
        public static void AddItemToList(Item item)
        {
            if(_varer.All(it => it.ItemID != item.ItemID))
            {
                _varer.Add(item);
            }
            if(_varer.Any(it => it.ItemName == item.ItemName && it.ItemID == item.ItemID))
            {
                _varer.FirstOrDefault(it => it.ItemID == item.ItemID).Quantity++;
            }
            else
            {
                WriteLineCommands.WriteLineMessage("ID is already used/wrong name for item is used");
                Thread.Sleep(3000);
            }
        }
        public static void AddMultipleItemsToList(List<Item> list)
        {
            Item itemtest;
            foreach(Item item in list)
            {
                itemtest = _varer.FirstOrDefault(it => it.ItemID == item.ItemID);
                itemtest.Quantity += item.Quantity;
            }
        }
        public static void AddSingleItemToList(Item item)
        {
            Item vare = _varer.FirstOrDefault(it => it.ItemID == item.ItemID);
            vare.Quantity += item.Quantity;
        }
        public static Item ReturnItemFromID(int id)
        {
            return _varer.FirstOrDefault(it => it.ItemID == id);
        }
        public static int PrintSearched(string input)
        {
            int i = 0;
            foreach (Item item in _varer)
            {

                if (item.ItemName.Contains(input, StringComparison.OrdinalIgnoreCase))
                {
                    WriteLineCommands.WriteLineMessage($"Name: {item.ItemName}   ID: {item.ItemID} ");
                    i++;
                }
            }
            
            return i;
        }
        public static IReadOnlyList<Item> GetList() => _varer.AsReadOnly();
        
    }
}
