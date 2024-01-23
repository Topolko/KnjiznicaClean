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
    public class GetRatesByDateHandler : IQueryHandler<GetRatesByDateQuery, List<ValutaModel>>
    {


        public async Task<List<ValutaModel>> HandleAsync(GetRatesByDateQuery query)
        {
            HttpClient client = new HttpClient();

            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Double,
                Culture = new System.Globalization.CultureInfo("hr-HR")
            };
            var gertCurrency = await client.GetAsync(HnbAPI.RatesForDate + query.Date);
            var gertCurrencyresp = await gertCurrency.Content.ReadAsStringAsync();
            var repositories = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyresp, settings);

            return repositories;
        }
    }
}
