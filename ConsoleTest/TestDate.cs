using Knjiznica.Core.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ConsoleTest
{
    internal class TestDate
    {
        public static async Task Run()
        {
            var t = new TestDate();
            await t.GetByDate();
        }
        async Task GetByDate()
        {
            HttpClient client = new HttpClient();

            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Double,
                Culture = new System.Globalization.CultureInfo("hr-HR")
            };

            var date = "2010-10-25";

            var gertCurrency = await client.GetAsync("https://api.hnb.hr/tecajn-eur/v3?datum-primjene=" + date);
            var gertCurrencyresp = await gertCurrency.Content.ReadAsStringAsync();
            var repositories = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyresp, settings);


            foreach (var item in repositories)
            {
                Console.WriteLine(item.Valuta, item.Srednji_tecaj);
            }
        }
    }
}
