using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Infrastructure.Common
{
    public static class HnbAPI
    {
       public const string GetRates = "https://api.hnb.hr/tecajn-eur/v3";

       public const string RatesForCurrency = "https://api.hnb.hr/tecajn-eur/v3?valuta=";

       public const string RatesForDate = "https://api.hnb.hr/tecajn-eur/v3?datum-primjene=";
    }
}
