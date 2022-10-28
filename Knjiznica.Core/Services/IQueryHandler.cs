using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services
{
    public interface IQueryHandler<in TQ, TResult>
    {
        Task<TResult> HandleAsync(TQ query);
    }
}
