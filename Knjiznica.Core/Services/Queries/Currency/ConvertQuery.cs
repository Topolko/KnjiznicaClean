using Knjiznica.Core.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services.Queries.Currency
{
    ////public record ConvertQuery(string valuta, string tecaj, decimal toConvert) : IRequest<decimal>;

    public class ConvertQuery : IRequest<decimal>
    {
        public readonly string Valuta;
        public readonly string Tecaj;
        public readonly decimal ToConvert;

        public ConvertQuery(string valuta, string tecaj, decimal toConvert)
        {
            Valuta = valuta;
            Tecaj = tecaj;
            ToConvert = toConvert;
        }
    }
}
