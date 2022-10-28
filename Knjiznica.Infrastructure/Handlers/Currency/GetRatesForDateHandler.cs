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
    public class GetRatesForDateHandler : IQueryHandler<GetRatesForDateQuery, ValutaModel>
    {

        public async Task<ValutaModel> HandleAsync(GetRatesForDateQuery query)
        {
            HttpClient client = new HttpClient();

            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Double,
                Culture = new System.Globalization.CultureInfo("hr-HR")
            };

            var gertCurrency = await client.GetAsync("https://api.hnb.hr/tecajn/v2?datum-primjene=" + query.Date+"&valuta="+query.Valuta);
            var gertCurrencyResp = await gertCurrency.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValutaModel>(gertCurrencyResp, settings);

            return response;
        }
    }
}
