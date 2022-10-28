using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services.Queries.Currency;
using Knjiznica.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Infrastructure.Handlers.Currency
{
    public class ConvertHandler : IQueryHandler<ConvertQuery, decimal>
    {


        public async Task<decimal> HandleAsync(ConvertQuery query)
        {
            HttpClient client = new HttpClient();

            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Double,
                Culture = new System.Globalization.CultureInfo("hr-HR")
            };


            var gertCurrency = await client.GetAsync("https://api.hnb.hr/tecajn/v2?valuta=" + query.valuta);
            var gertCurrencyResp = await gertCurrency.Content.ReadAsStringAsync();
            var Valuta = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyResp, settings);

            decimal convertTo;

            switch (query.tecaj)
            {
                case "Kupovni":
                    convertTo = Valuta.Select(x => x.Kupovni_tecaj).FirstOrDefault();
                    break;
                case "Prodajni":
                    convertTo = Valuta.Select(x => x.Prodajni_tecaj).FirstOrDefault();
                    break;
                default:
                    convertTo = Valuta.Select(x => x.Srednji_tecaj).FirstOrDefault();
                    break;
            }

            decimal convertedValue;

            if (query.toConvert <= 0 || query.toConvert == null)
            {
                convertedValue = (decimal)(1 / convertTo);
            }
            else
            {
                convertedValue = (decimal)(query.toConvert / convertTo);
            }


            return convertedValue;
        }
    }
}
