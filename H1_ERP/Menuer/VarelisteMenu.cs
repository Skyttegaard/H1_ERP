using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Menuer
{
    public static class VarelisteMenu
    {
        public static List<string> MenuOptions = new();
        static VarelisteMenu()
        {
            InitializeList();
        }
        private static void InitializeList()
        {
            MenuOptions.Add("Opret vare");
            MenuOptions.Add("Ændre vare");
            MenuOptions.Add("Søg vare");
            MenuOptions.Add("Fjern vare");
        }
    }
}
