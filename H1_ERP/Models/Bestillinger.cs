using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Models
{
    public class Bestillinger
    {
        public string ItemName;
        public string Lokation;
        public int KundeID;
        public int Antal;
        public int VareID;
        public int OrderID;
        public DateTime Dato;
        
        public Bestillinger(string itemName, int kundeID, string lokation, int antal, int vareID, int orderID, DateTime dato)
        {
            ItemName = itemName;
            KundeID = kundeID;
            Lokation = lokation;
            Antal = antal;
            VareID = vareID;
            OrderID = orderID;
            Dato = dato;
        }
        public double GetPrisXAntal()
        {
            double pris = Vareliste.GetList().FirstOrDefault(it => it.ItemID == VareID).ItemSalesPrice;
            return pris * Antal;
        }
    }
}
