using Knjiznica.Core.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services.Queries.Currency
{
    public class GetRatesForDateQuery : IRequest<List<ValutaModel>>
    {
        public readonly string? Valuta;
        public readonly string? Date;

        public GetRatesForDateQuery(string? valuta, string? date)
        {
            Valuta = valuta;
            Date = date;
        }
    }

}
