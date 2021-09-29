using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Models
{
    public class Item
    {
        public string ItemName { get; private set; }
        public double ItemSalesPrice { get; private set; }
        public int ItemID { get; private set; }
        public double ItemBuyPrice { get; private set; }
        public int Quantity { get; set; }

        public Item(string itemName, double itemSalesPrice, double itemBuyPrice, int itemID = 0, int quantity = 0)
        {
            ItemName = itemName;
            ItemSalesPrice = itemSalesPrice;
            ItemBuyPrice = itemBuyPrice;
            ItemID = itemID;
            Quantity = quantity;
        }
        public static void EditItem(Item item, string itemName, double itemSalesPrice, double itemBuyPrice)
        {
            item.ItemName = itemName;
            item.ItemSalesPrice = itemSalesPrice;
            item.ItemBuyPrice = itemBuyPrice;
        }
    }
}
