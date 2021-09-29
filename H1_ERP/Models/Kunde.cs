using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP.Models
{
    class Kunde : Person
    {
        public int Kundenummer { get; set; }
        public int SenesteOrderID { get; set; }
        public DateTime SenesteOrdreDato { get; set; }

        public Kunde(string fornavn, string efternavn, int personId, string email, int telefonNummer, string tekst, string vejNavn, string husNummer, int postNummer, string by,
            int kundeNummer=0)
        {
            Fornavn = fornavn;
            Efternavn = efternavn;
            Kundenummer = kundeNummer;
            Kontakt.Email = email;
            Kontakt.TelefonNummer = telefonNummer;
            Kontakt.Tekst = tekst;
            Adresse.Vejnavn = vejNavn;
            Adresse.HusNummer = husNummer;
            Adresse.PostNummer = postNummer;
            Adresse.By = by;
            PersonId = personId;
            
        }
    }
}
