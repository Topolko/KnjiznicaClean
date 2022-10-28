using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Models.Models
{

    public class ValuteArray
    {
        public ValutaModel[] ValuteArr { get; set; }
    }

    public class ValutaModel
    {
        public string? Broj_tecajnice { get; set; }
        public string? Datum_primjene { get; set; }
        public string? Drzava { get; set; }
        public string? Drzava_iso { get; set; }
        public int? Sifra_valute { get; set; }
        public string? Valuta { get; set; }
        public int Jedinica { get; set; }
        public decimal Kupovni_tecaj { get; set; }
        public decimal Srednji_tecaj { get; set; }
        public decimal Prodajni_tecaj { get; set; }
    }

}
