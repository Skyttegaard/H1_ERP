using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Models
{
    class Varebestilling
    {
        public static List<string> MenuOptions = new() { "Bestil enkelte vare", "Bestil alle vare på listen" };
        public static List<Item> OrderList = new();
        public static void AddItemToList(Item item, int quantity)
        {
            OrderList.Add(new Item(item.ItemName, item.ItemSalesPrice, item.ItemBuyPrice, item.ItemID, quantity));
        }
    }
}
