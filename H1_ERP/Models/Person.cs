using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Models
{
    abstract class Person
    {
        public int PersonId { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public Adresse Adresse { get; set; } = new();
        public KontaktOplysning Kontakt { get; set; } = new();
    }
}
