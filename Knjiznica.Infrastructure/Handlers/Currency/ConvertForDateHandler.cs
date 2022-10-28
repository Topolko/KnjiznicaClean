using Knjiznica.Core.Services.Queries.Currency;
using Knjiznica.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knjiznica.Core.Models.Models;

namespace Knjiznica.Infrastructure.Handlers.Currency
{
    public class ConvertForDateHandler : IQueryHandler<ConvertForDateQuery, decimal>
    {
        readonly IQueryHandler<GetRatesForDateQuery, ValutaModel> _getRatesForDate;

        public ConvertForDateHandler(IQueryHandler<GetRatesForDateQuery, ValutaModel> getConversionForDate)
        {
            _getRatesForDate = getConversionForDate;

        }

        public async Task<decimal> HandleAsync(ConvertForDateQuery query)
        {

            var getRatesForDate = new GetRatesForDateQuery(query.Valuta , query.Date);
            var valuta = await _getRatesForDate.HandleAsync(getRatesForDate);
            var tecaj = valuta.Srednji_tecaj;
            var converted = query.Iznos*tecaj;

            return converted;
        }
    }
}
