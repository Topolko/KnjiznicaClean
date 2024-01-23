using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Knjiznica.Core.Services.Commands.Rents;
using Knjiznica.Core.Services.Queries.Rents;
using Knjiznica.Core.Services.Queries.Currency;
using Knjiznica.Core.Models.ViewModels;
using Knjiznica.Presentation.Models;
using static System.Reflection.Metadata.BlobBuilder;
using Knjiznica.Core.Models;

namespace Knjiznica.Presentation.Controllers
{
    public class ValuteController : Controller
    {

        readonly IQueryHandler<GetAllRatesQuery, List<ValutaModel>> _getAllRates;
        readonly IQueryHandler<GetRatesByDateQuery, List<ValutaModel>> _getRatesByDay;
        readonly IQueryHandler<ConvertQuery, decimal> _convert;
        readonly IQueryHandler<GetRatesForDateQuery, List<ValutaModel>> _getRatesForDate;

        public ValuteController(
           IQueryHandler<GetAllRatesQuery, List<ValutaModel>> getAllRates,
           IQueryHandler<GetRatesByDateQuery, List<ValutaModel>> getRatesByDate,
           IQueryHandler<ConvertQuery, decimal> convert,
           IQueryHandler<GetRatesForDateQuery, List<ValutaModel>> getRatesForDate)
        {
            _getAllRates = getAllRates;
            _getRatesByDay = getRatesByDate;
            _convert = convert;
            _getRatesForDate = getRatesForDate;
        }

        public async Task <IActionResult> View()
        {
            var RQ = await _getAllRates.HandleAsync(new GetAllRatesQuery());

            var RQViewModel = (from b in RQ

                                 select new ValutaViewModel()
                                 {
                                     Broj_tecajnice = b.Broj_tecajnice,
                                     Datum_primjene = b.Datum_primjene,
                                     Drzava = b.Drzava,
                                     Drzava_iso = b.Drzava_iso,
                                     Sifra_valute = b.Sifra_valute,
                                     Valuta = b.Valuta,
                                     Jedinica = b.Jedinica,
                                     Kupovni_tecaj = b.Kupovni_tecaj,
                                     Srednji_tecaj = b.Srednji_tecaj,
                                     Prodajni_tecaj = b.Prodajni_tecaj

                                 });

            return View(RQViewModel);
        }

        public async Task<IActionResult> Converter(string ValuteList, decimal toConvert, string Odabrani_tecaj )
        {

            string odabranaValuta = ValuteList;
             
            var allRates = await _getAllRates.HandleAsync(new GetAllRatesQuery());
            var ListValuta = allRates.ToList().Select(x => x.Valuta).ToList();

            var converted = await _convert.HandleAsync(new ConvertQuery(odabranaValuta, Odabrani_tecaj, toConvert));

            ValutaListViewModel valuteListViewModel = new ValutaListViewModel();
            valuteListViewModel.ValuteList = ListValuta;
            valuteListViewModel.ConvertedValue = converted;
            valuteListViewModel.Odabrani_tecaj = new List<Tecajevi> ((Tecajevi[])Enum.GetValues(typeof(Tecajevi))).ToList();
            return  View(valuteListViewModel);
        }

        public async Task<IActionResult> CurrencyByDate(DateTime srcDate)
        {
            string date = null;
            if (srcDate != default)
            {
                date = srcDate.ToString("yyyy-MM-dd");
            }
            var DQ = await _getRatesByDay.HandleAsync(new GetRatesByDateQuery(date));

            var DQViewModel = (from b in DQ

                               select new ValutaViewModel()
                               {
                                   Broj_tecajnice = b.Broj_tecajnice,
                                   Datum_primjene = b.Datum_primjene,
                                   Drzava = b.Drzava,
                                   Drzava_iso = b.Drzava_iso,
                                   Sifra_valute = b.Sifra_valute,
                                   Valuta = b.Valuta,
                                   Jedinica = b.Jedinica,
                                   Kupovni_tecaj = b.Kupovni_tecaj,
                                   Srednji_tecaj = b.Srednji_tecaj,
                                   Prodajni_tecaj = b.Prodajni_tecaj

                               });

            return  View(DQViewModel);
        }
        public async Task<IActionResult> RateByDateAndCurrency(DateTime srcDate, string ValuteList)
        {
            string odabranaValuta = ValuteList;

            string date = null;
            if (srcDate != default)
            {
                date = srcDate.ToString("yyyy-MM-dd");
            }
            var RDC = await _getRatesForDate.HandleAsync(new GetRatesForDateQuery(odabranaValuta, date));

            var allRates = await _getAllRates.HandleAsync(new GetAllRatesQuery());
            var ListValuta = allRates.ToList().Select(x => x.Valuta).ToList();

            List<ValutaViewModel> RDCViewModel = (from b in RDC

                               select new ValutaViewModel()
                               {
                                   Broj_tecajnice = b.Broj_tecajnice,
                                   Datum_primjene = b.Datum_primjene,
                                   Drzava = b.Drzava,
                                   Drzava_iso = b.Drzava_iso,
                                   Sifra_valute = b.Sifra_valute,
                                   Valuta = b.Valuta,
                                   Jedinica = b.Jedinica,
                                   Kupovni_tecaj = b.Kupovni_tecaj,
                                   Srednji_tecaj = b.Srednji_tecaj,
                                   Prodajni_tecaj = b.Prodajni_tecaj

                               }).ToList();

            ValutaListViewModel valuteListViewModel = new ValutaListViewModel();
            valuteListViewModel.ValuteList = ListValuta;
            valuteListViewModel.Valuta_ViewModelsList = RDCViewModel;

            return View(valuteListViewModel);
        }

    }
}
