using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services.Queries.Currency;
using Knjiznica.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knjiznica.Infrastructure.Common;

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


            var gertCurrency = await client.GetAsync(HnbAPI.RatesForCurrency + query.Valuta);
            var gertCurrencyResp = await gertCurrency.Content.ReadAsStringAsync();
            var Valuta = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyResp, settings);

            decimal convertTo;

            switch (query.Tecaj)
            {
                case "Kupovni":
                    convertTo = (decimal)Valuta.Select(x => x.Kupovni_tecaj).FirstOrDefault();
                    break;
                case "Prodajni":
                    convertTo = (decimal)Valuta.Select(x => x.Prodajni_tecaj).FirstOrDefault();
                    break;
                default:
                    convertTo = (decimal)Valuta.Select(x => x.Srednji_tecaj).FirstOrDefault();
                    break;
            }

            decimal convertedValue;

            if (query.ToConvert <= 0 || query.ToConvert == null)
            {
                convertedValue = (decimal)(1 / convertTo);
            }
            else
            {
                convertedValue = (decimal)(query.ToConvert / convertTo);
            }


            return convertedValue;
        }
    }
}
