using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace H1_ERP.Models
{
    class Varebestilling
    {
        public static List<string> _menuOptions = new() { "Bestil enkelte vare", "Bestil alle vare på listen" };
        
        public static IReadOnlyList<string> MenuOptions => _menuOptions.AsReadOnly();
        private static List<Item> _orderList = new();
        /// <summary>
        /// Kopiere item til ny liste for at gemme den i ordrelisten.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public static void AddItemToList(Item item, int quantity)
        {
            
            _orderList.Add(new Item(item.ItemName, item.ItemSalesPrice, item.ItemBuyPrice, item.ItemID, quantity));
        }
        /// <summary>
        /// Bruger Vareliste.AddMultipleItemsToList og fjerner derefter alt i ordrelisten.
        /// </summary>
        public static void OrderAllItems()
        {
            Vareliste.AddMultipleItemsToList(_orderList);
            _orderList.Clear();
        }
        /// <summary>
        /// Bruger Vareliste.AddSingleItemToList og fjerne derefter den enkelte item fra ordrelisten.
        /// </summary>
        /// <param name="id"></param>
        public static void OrderSingleItem(int id)
        {
            Item item = ReturnItemFromID(id);
            Vareliste.AddSingleItemToList(item);
            _orderList.Remove(item);
        }
        /// <summary>
        /// Returnere item som passer med int ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Item ReturnItemFromID(int id)
        {
            return _orderList.FirstOrDefault(it => it.ItemID == id);
        }
        public static IReadOnlyList<Item> GetList() => _orderList.AsReadOnly();
    }
}
