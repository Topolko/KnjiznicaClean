using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services.Queries.Currency;
using Knjiznica.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Knjiznica.Infrastructure.Common;

namespace Knjiznica.Infrastructure.Handlers.Currency
{
    public class GetRatesForDateHandler : IQueryHandler<GetRatesForDateQuery, List<ValutaModel>>
    {
        readonly IQueryHandler<GetRatesByDateQuery, List<ValutaModel>> _getByDate;
        readonly IQueryHandler<GetAllRatesQuery, List<ValutaModel>> _getAll;

        public GetRatesForDateHandler(IQueryHandler<GetRatesByDateQuery, List<ValutaModel>> getByDate,
            IQueryHandler<GetAllRatesQuery, List<ValutaModel>> getAll)
        {
            _getByDate = getByDate;
            _getAll = getAll;   

        }
        public async Task<List<ValutaModel>> HandleAsync(GetRatesForDateQuery query)
        {


            HttpClient client = new HttpClient();

            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Double,
                Culture = new System.Globalization.CultureInfo("hr-HR")
            };
            List<ValutaModel> response = new List<ValutaModel>();
            if (query.Valuta == null && query.Date != null )
            {
                var getRatesByDate = new GetRatesByDateQuery(query.Date);
                var resp = await _getByDate.HandleAsync(getRatesByDate);

                return resp;
            }
            else if (query.Valuta != null && query.Date == null)
            {
                var gertCurrency = await client.GetAsync(HnbAPI.RatesForCurrency + query.Valuta);
                ///var gertCurrency = await client.GetAsync("https://api.hnb.hr/tecajn-eur/v3?valuta=" + query.Valuta);
                var gertCurrencyResp = await gertCurrency.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyResp, settings);
                return response;
            }
            else if (query.Valuta != null && query.Date != null)
            {
                var gertCurrency = await client.GetAsync(HnbAPI.RatesForCurrency + query.Valuta+"&datum="+query.Date);
                var gertCurrencyResp = await gertCurrency.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyResp, settings);
                return response;
            }
            else
            {
                var allRates = new GetAllRatesQuery();
                response = await _getAll.HandleAsync(allRates);
                return response;
            }
        }
    }
}
