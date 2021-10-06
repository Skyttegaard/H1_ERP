using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP.Factories;
using H1_ERP.ConsoleCommands;


namespace H1_ERP.Models
{
    class Varebestilling
    {
        public static List<string> _menuOptions = new() { "Bestil enkelte vare", "Bestil alle vare på listen" };
        public static void loadlist()
        {
            _orderLists = DatabaseFactory.AddEverythingToBestillingList();
        }
        
        public static IReadOnlyList<string> MenuOptions => _menuOptions.AsReadOnly();
        private static List<Bestillinger> _orderLists;
        public static void AddItemToBestillingList(Item item)
        {
            WriteLineCommands.WriteLineMessage("Venligst skriv dit kunde ID");
            int kundeID = ReadLineCommands.GetIntInput();
            WriteLineCommands.WriteLineMessage("Indtast lokation");
            string lokation = ReadLineCommands.GetStringInput();
            WriteLineCommands.WriteLineMessage("Hvor mange?");
            int antal = ReadLineCommands.GetIntInput();
            WriteLineCommands.WriteLineMessage("Indtast ordreID (Tryk enter for at lave en ny ordre)");
            int ordreID = ReadLineCommands.GetKundeInfo(0);
            DatabaseFactory.AddVareToBestillingList(item.ItemID, antal, kundeID, ordreID, lokation);
            _orderLists = DatabaseFactory.AddEverythingToBestillingList();
        }
        

        public static void DeliverSingleItem(int id)
        {
            Bestillinger bestilling = _orderLists.FirstOrDefault(ol => ol.OrderID == id);
            DatabaseFactory.DeliverItems(bestilling);
            WriteLineCommands.WritelineWaitForKeyPress($"{bestilling.ItemName} has been delivered!");
            Vareliste.ReloadList();
            _orderLists = DatabaseFactory.AddEverythingToBestillingList();
        }
        public static void DeliverAllItems()
        {
            foreach(Bestillinger bestilling in _orderLists)
            {
                DatabaseFactory.DeliverItems(bestilling);
            }
            WriteLineCommands.WritelineWaitForKeyPress("All items has been delivered");
            Vareliste.ReloadList();
            _orderLists = DatabaseFactory.AddEverythingToBestillingList();
        }

        /// <summary>
        /// Kopiere item til ny liste for at gemme den i ordrelisten.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        //public static void AddItemToList(Item item, int quantity)
        //{
            
        //    _orderList.Add(new Item(item.ItemName, item.ItemSalesPrice, item.ItemBuyPrice,item.StorageCapacity, item.ItemID,quantity));
        //}
        /// <summary>
        /// Bruger Vareliste.AddMultipleItemsToList og fjerner derefter alt i ordrelisten.
        /// </summary>
        //public static void OrderAllItems()
        //{
        //    Vareliste.AddMultipleItemsToList(_orderList);
        //    _orderList.Clear();
        //}
        
        /// <summary>
        /// Returnere bestilling som passer med int Order ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Bestillinger ReturnItemFromID(int orderid)
        {
            return _orderLists.FirstOrDefault(it => it.OrderID == orderid);
        }
        public static IReadOnlyList<Bestillinger> GetList() => _orderLists.AsReadOnly();
    }
}
