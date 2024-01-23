using Knjiznica.Core.Models;
using System.ComponentModel.DataAnnotations;


namespace Knjiznica.Presentation.Models
{
    public class ValutaListViewModel
    {

        [Display(Name = "Odaberite valutu")]
        public List<string>? ValuteList { get; set; }

        [Display(Name = "Converted value")]
        public decimal? ConvertedValue { get; set; }

        [Display(Name = "Odaberite tečaj")]
        public List<Tecajevi>? Odabrani_tecaj { get ; set; }

        public List<string>? Odabrana_Valuta { get; set; }

        public List<ValutaViewModel> Valuta_ViewModelsList { get; set; }

    }

    public class ValutaViewModel
    {
        public string? Broj_tecajnice { get; set; }

        [Display(Name = "Datum primjene")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string? Datum_primjene { get; set; }

        [Display(Name = "Država")]
        public string? Drzava { get; set; }

        [Display(Name = "Država iso")]
        public string? Drzava_iso { get; set; }

        [Display(Name = "Šifra valute")]
        public int? Sifra_valute { get; set; }

        [Display(Name = "ValutaModel")]
        public string? Valuta { get; set; }

        [Display(Name = "Jedinica")]
        public int? Jedinica { get; set; }

        [Display(Name = "Kupovni tečaj")]
        public decimal? Kupovni_tecaj { get; set; }

        [Display(Name = "Srednji tečaj")]
        public decimal? Srednji_tecaj { get; set; }

        [Display(Name = "Prodajni tečaj")]
        public decimal? Prodajni_tecaj { get; set; }

        public string Odabrani_tecaj { get; set; }

    }

}
