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
    public record ConvertDecimalQuery(decimal convertTo, decimal toConvert) : IRequest<decimal>;
}
