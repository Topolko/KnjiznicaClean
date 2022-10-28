using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Core.Services.Queries.Rents;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Infrastructure.Handleres.Books
{
    public class GetBooksWithGenreIdHandler : IQueryHandler<GetBooksWithGenreIdQuery, IQueryable<Book>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetBooksWithGenreIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<IQueryable<Book>> HandleAsync(GetBooksWithGenreIdQuery request
            ) => _knjiznicaContext.Books.Where(x => x.GenreId == request.Id);
    }
}
