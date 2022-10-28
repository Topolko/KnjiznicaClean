using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Application.Queries
{
    public class GetAllBooksQuery : IRequest<List<Knjiznica.Core.Models.Models.Book>>
    {

    }
}
