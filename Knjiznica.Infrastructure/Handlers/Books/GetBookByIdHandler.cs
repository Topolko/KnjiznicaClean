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
    public class GetBookByIdHandler : IQueryHandler<GetBookByIdQuery, Book>
    {
        private readonly KnjiznicaContext _knjiznicaContext;
        public GetBookByIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<Book> HandleAsync(GetBookByIdQuery request) =>
            _knjiznicaContext.Books.Single(p => p.BookId == request.Id);
    }
}
