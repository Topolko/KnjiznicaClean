using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services
{
    public interface IMultipleQueryHandler<in TDQ,in TVQ, TResult>
    {
        Task<TResult> HandleAsync(TDQ dateQuery,TVQ valuteQuery);
    }
}
