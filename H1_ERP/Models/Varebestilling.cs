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
        private static List<Item> _orderList = new();
        public static void AddItemToList(Item item, int quantity)
        {
            _orderList.Add(new Item(item.ItemName, item.ItemSalesPrice, item.ItemBuyPrice, item.ItemID, quantity));
        }
        public static void OrderAllItems()
        {
            Vareliste.AddMultipleItemsToList(_orderList);
            _orderList.Clear();
        }
        public static void OrderSingleItem(int id)
        {
            Item item = ReturnItemFromID(id);
            Vareliste.AddSingleItemToList(item);
            _orderList.Remove(item);
        }
        public static Item ReturnItemFromID(int id)
        {
            return _orderList.FirstOrDefault(it => it.ItemID == id);
        }
        public static IReadOnlyList<Item> GetList() => _orderList.AsReadOnly();
    }
}
