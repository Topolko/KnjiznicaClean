using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services.Queries.Currency;
using Knjiznica.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Knjiznica.Infrastructure.Handlers.Currency
{
    public class CurrencyAndDateHandler : IMultipleQueryHandler<GetConversionRateQuery, GetRatesByDateQuery, List<ValutaModel>>
    {


        public async Task<List<ValutaModel>> HandleAsync(GetConversionRateQuery conversionQuery, GetRatesByDateQuery datesQuery)
        {
            HttpClient client = new HttpClient();

            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Double,
                Culture = new System.Globalization.CultureInfo("hr-HR")
            };

            List<ValutaModel> list;

            if (conversionQuery != null && datesQuery == null)
            {
                var gertCurrency = await client.GetAsync("https://api.hnb.hr/tecajn/v2?ValutaModel=" + conversionQuery.valuta);
                var gertCurrencyresp = await gertCurrency.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyresp, settings);
            }
            else if (datesQuery != null && conversionQuery == null)
            {
                var gertCurrency = await client.GetAsync("https://api.hnb.hr/tecajn/v2?datum-primjene=" + datesQuery.date.ToString("yyyy-MM-dd"));
                var gertCurrencyresp = await gertCurrency.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyresp, settings);
            }
            else if (datesQuery != null && conversionQuery != null)
            {

                var gertDate = await client.GetAsync("https://api.hnb.hr/tecajn/v2?datum-primjene=" + datesQuery.date.ToString("yyyy-MM-dd"));
                var gertDateResp = await gertDate.Content.ReadAsStringAsync();
                var listDate = JsonConvert.DeserializeObject<List<ValutaModel>>(gertDateResp, settings);
                list = listDate.Where(x => x.Valuta == conversionQuery.valuta).ToList();
            }
            else
            {
                var getRates = await client.GetAsync("https://api.hnb.hr/tecajn/v2");
                var gerRatesResp = await getRates.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<ValutaModel>>(gerRatesResp, settings);
            }

            return list;
        }
    }
}
