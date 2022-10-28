using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services
{
    public interface ICommandHandler<TC> 
    {
        Task HandleAsync(TC command);
    }
}
