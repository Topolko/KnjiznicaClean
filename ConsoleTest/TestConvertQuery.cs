using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services.Queries.Currency;
using Knjiznica.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ConsoleTest
{
    internal class TestConvertQuery
    {
        public static async Task Run()
        {
            var t = new TestConvertQuery();
            await t.Test1();
        }
        async Task Test1()
        {



            decimal tecaj = 10M;
            decimal iznos = 10M;

            var converted = iznos * tecaj;

            Console.WriteLine(converted);

        }
    }
}
