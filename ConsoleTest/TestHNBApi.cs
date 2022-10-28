using Knjiznica.Core.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    internal class TestHNBApi
    {
        public static async Task Run()
        {
            var t = new TestHNBApi();
            await t.TestGetTecajnaLista();
        }

        async Task TestGetTecajnaLista()
        {
            HttpClient client = new HttpClient();

            var settings = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                FloatParseHandling = FloatParseHandling.Double,
                Culture = new System.Globalization.CultureInfo("hr-HR")
            };

            string valuta = "DKK";

            var getCurrency = await client.GetAsync("https://api.hnb.hr/tecajn/v2?valuta=" + valuta);
            var gertCurrencyResp = await getCurrency.Content.ReadAsStringAsync();
            var Valuta = JsonConvert.DeserializeObject<List<ValutaModel>>(gertCurrencyResp, settings);




        }

    }
}
