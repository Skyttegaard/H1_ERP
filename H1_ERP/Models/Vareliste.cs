using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Models
{
    class Vareliste
    {
        public static List<Item> Varer = new();

        public static void CreateItem(string name, double buyPrice, double salesPrice, int ID)
        {
            Varer.Add(new Item(name, salesPrice, buyPrice, ID));
        }
        public static void RemoveItem(Item item)
        {
            Varer.Remove(item);
        }
    }
}
