using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H1_ERP.ConsoleCommands;
using H1_ERP.Factories;

namespace H1_ERP.Models
{
    class Vareliste
    {
        private static List<Item> _varer = DatabaseFactory.AddEverythingToVareLists();

        public static void CreateItem(string name, double buyPrice, double salesPrice, int ID)
        {
            _varer.Add(new Item(name, salesPrice, buyPrice, ID));
        }
        public static void RemoveItem(Item item)
        {
            if(item.Quantity <= 1)
            {
                _varer.Remove(item);

            }
            else
            {
                item.Quantity--;
            }
        }
        /// <summary>
        /// Tilføjer item til list hvis ID ikke er brugt. Hvis ID og navn er ens med eksisterende item går quantity op. Hvis ID samme id men forskelligt navn == error.
        /// </summary>
        /// <param name="item"></param>
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
        /// <summary>
        /// Tilføjer quantity til eksisterende ting i varelisten ved hjælp af en liste med item objekt.
        /// </summary>
        /// <param name="list"></param>
        public static void AddMultipleItemsToList(List<Item> list)
        {
            Item itemtest;
            foreach(Item item in list)
            {
                itemtest = _varer.FirstOrDefault(it => it.ItemID == item.ItemID);
                itemtest.Quantity += item.Quantity;
            }
        }
        public static void test(List<Item> list)
        {
            _varer.AddRange(list);
        }
        /// <summary>
        /// Tilføjer en enkelt item's quantity til varelisten.
        /// </summary>
        /// <param name="item"></param>
        public static void AddSingleItemToList(Item item)
        {
            Item vare = _varer.FirstOrDefault(it => it.ItemID == item.ItemID);
            vare.Quantity += item.Quantity;
        }
        /// <summary>
        /// Returnere item med tilsvarende ID fra varelisten.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Item ReturnItemFromID(int id)
        {
            return _varer.FirstOrDefault(it => it.ItemID == id);
        }
        /// <summary>
        /// Finder alle varer som matcher det som der er søgt på i varelisten og returnere mængden som int.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
