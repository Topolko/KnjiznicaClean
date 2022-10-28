using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Authors;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handlers.Authors
{
    public class GetAuthorByIdHandler : IQueryHandler<GetAutorByIdQuery, Autor>
    {
        private readonly KnjiznicaContext _knjiznicaContext;
        public GetAuthorByIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<Autor> HandleAsync(GetAutorByIdQuery request) =>
            _knjiznicaContext.Autors.Single(p => p.AutorId == request.Id);
    }
}
