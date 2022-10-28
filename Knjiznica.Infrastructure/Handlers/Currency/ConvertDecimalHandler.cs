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

    
    public class ConvertDecimalHandler : IQueryHandler<ConvertDecimalQuery, decimal>
    {

        public async Task<decimal> HandleAsync(ConvertDecimalQuery query)
        {

            decimal convertedValue;


            if (query.toConvert <= 0 || query.toConvert == null || query.convertTo <= 0 || query.convertTo == null)
            {
                convertedValue = (decimal)(1 / 1);
            }
            else
            {
                convertedValue = (decimal)(query.toConvert / query.convertTo);
            }


            return convertedValue;
        }
    }
}
