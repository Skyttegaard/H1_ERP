using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Models
{
    class Salgsordre
    {
        public static List<string> _menuOptionsForSalgsOrdre = new() { "Ordre vare" };
        public static IReadOnlyList<string> SalgsOrdreMenuOptions => _menuOptionsForSalgsOrdre.AsReadOnly();
        public Kunde Kunde;
        public int Ordreid;
        public Salgsordre(Kunde kunde, int ordreid)
        {
            Kunde = kunde;
            Ordreid = ordreid;
        }
    }
}
