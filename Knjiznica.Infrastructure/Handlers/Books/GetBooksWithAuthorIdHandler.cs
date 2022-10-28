using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Infrastructure.DBContext;

namespace Knjiznica.Infrastructure.Handlers.Books
{
    public class GetBooksWithAuthorIdHandler : IQueryHandler<GetBooksWithAuthorIdQuery, IQueryable<Book>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetBooksWithAuthorIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<IQueryable<Book>> HandleAsync(GetBooksWithAuthorIdQuery request
            ) => _knjiznicaContext.Books.Where(x => x.AutorId == request.Id);
    }
}
