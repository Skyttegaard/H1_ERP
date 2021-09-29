using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Menuer
{
    public static class VarelisteMenu
    {
        private static List<string> _menuOptions = new()
        {
            "Opret vare",
            "Ændre vare",
            "Søg vare",
            "Fjern vare"
        };
        public static IReadOnlyList<string> MenuOptions => _menuOptions.AsReadOnly();
        
    }
}
