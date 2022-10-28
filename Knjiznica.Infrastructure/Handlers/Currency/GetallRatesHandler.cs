using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services.Queries.Currency;
using Knjiznica.Core.Services;
using Newtonsoft.Json;

namespace Knjiznica.Infrastructure.Handlers.Currency
{
    public class GetAllRatesHandler : IQueryHandler<GetAllRatesQuery, List<ValutaModel>>
    {


        public async Task<List<ValutaModel>> HandleAsync(GetAllRatesQuery query)
        {
            HttpClient client = new HttpClient();

            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Double,
                Culture = new System.Globalization.CultureInfo("hr-HR")
            };


            var gertCurrency = await client.GetAsync("https://api.hnb.hr/tecajn/v2");
            var gertCurrencyresp = await gertCurrency.Content.ReadAsStringAsync();
            var repositories = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyresp, settings);



            return repositories;
        }
    }
}
