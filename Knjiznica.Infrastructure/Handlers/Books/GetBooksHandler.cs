using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Infrastructure.Handleres.Books
{
    public class GetBooksHandler : IQueryHandler<GetBooksQuery, IQueryable<Book>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetBooksHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<IQueryable<Book>> HandleAsync(GetBooksQuery query) => _knjiznicaContext.Books;
    }
}
