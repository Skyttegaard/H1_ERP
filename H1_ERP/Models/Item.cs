using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Models
{
    public class Item
    {
        public string ItemName { get; set; }
        public double ItemSalesPrice { get; set; }
        public int ItemID { get; set; }
        public double ItemBuyPrice { get; set; }

        public Item(string itemName, double itemSalesPrice, double itemBuyPrice, int itemID)
        {
            ItemName = itemName;
            ItemSalesPrice = itemSalesPrice;
            ItemBuyPrice = itemBuyPrice;
            ItemID = itemID;
        }
    }
}
