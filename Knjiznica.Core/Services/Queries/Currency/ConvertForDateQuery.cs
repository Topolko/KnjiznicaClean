using Knjiznica.Core.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services.Queries.Currency
{
    public class ConvertForDateQuery : IRequest<decimal>
    {
        public readonly string Valuta;
        public readonly DateTime Date;
        public readonly decimal Iznos;
        public ConvertForDateQuery(string valuta, DateTime date, decimal iznos)
        {
            Valuta = valuta;
            Date = date;
            Iznos = iznos;
        }
    }
}
