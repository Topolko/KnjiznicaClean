using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("UnitTest")]
namespace Knjiznica.Core.Services.Queries.Currency
{
    ////public record ConvertDecimalQuery(decimal convertTo, decimal toConvert) : IRequest<decimal>;
    
    public class ConvertDecimalQuery : IRequest<decimal>
    {
        public readonly decimal ConvertTo;
        public readonly decimal ToConvert;
        
        public ConvertDecimalQuery(decimal convertTo, decimal toConvert)
        {
            ToConvert = toConvert;
            ConvertTo = convertTo;
        } 
    }
}
