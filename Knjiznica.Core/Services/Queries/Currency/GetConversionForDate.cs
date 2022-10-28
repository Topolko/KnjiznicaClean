using Knjiznica.Core.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services.Queries.Currency
{
    public class GetRatesForDateQuery : IRequest<ValutaModel>
    {
        public readonly string Valuta;
        public readonly DateTime Date;

        public GetRatesForDateQuery(string valuta, DateTime date)
        {
            Valuta = valuta;
            Date = date;
        }
    }

}
