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

        
        public static void RemoveItem(Item item, int amount)
        {
            if(item.Quantity <= 1)
            {
                _varer.Remove(item);
                DatabaseFactory.RemoveVare(item);
            }
            else
            {
                item.Quantity -= amount;
                if (item.Quantity <= 0)
                {
                    DatabaseFactory.RemoveVare(item);
                    return;
                }
                DatabaseFactory.EditVare(item);
            }
        }
        /// <summary>
        /// Tilføjer item til list hvis ID ikke er brugt. Hvis ID og navn er ens med eksisterende item går quantity op. Hvis ID samme id men forskelligt navn == error.
        /// </summary>
        /// <param name="item"></param>
        public static void AddItemToList(Item item)
        {

            DatabaseFactory.AddVare(item);
            _varer = DatabaseFactory.AddEverythingToVareLists();



            //-----------------BLEV BRUGT TIL AT TJEKKE OM VAREN ALLEREDE FANDTES OG TILFØJEDE 1 TIL ANTAL HVIS DEN GJORDE. ELLERS ERROR HVIS MAN PRØVEDE AT GIVE SAMME ID SOM EN ANDEN.
            //if(_varer.All(it => it.ItemID != item.ItemID))
            //{
            //    _varer.Add(item);
            //}
            //if(_varer.Any(it => it.ItemName == item.ItemName && it.ItemID == item.ItemID))
            //{
            //    _varer.FirstOrDefault(it => it.ItemID == item.ItemID).Quantity++;
            //}
            //else
            //{
            //    WriteLineCommands.WriteLineMessage("ID is already used/wrong name for item is used");
            //    Thread.Sleep(3000);
            //}
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
        /// <summary>
        /// Redigere en vare og tiføjer til databasen. Opdatere listen varelisten for at få vare ID.
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateVare(Item item)
        {
            DatabaseFactory.EditVare(item);
            _varer = DatabaseFactory.AddEverythingToVareLists();
        }
        /// <summary>
        /// Får fat i varelisten som readonly
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<Item> GetList() => _varer.AsReadOnly();
        public static void ReloadList()
        {
            _varer = DatabaseFactory.AddEverythingToVareLists();
        }
    }
}
