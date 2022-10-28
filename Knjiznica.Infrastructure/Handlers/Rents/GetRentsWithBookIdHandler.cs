using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Rents;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handlers.Rents
{
    public class GetRentsWithBookIdHandler : IQueryHandler<GetRentsWithBookIdQuery, IQueryable<Rent>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetRentsWithBookIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<IQueryable<Rent>> HandleAsync(GetRentsWithBookIdQuery request
            ) => _knjiznicaContext.Rents.Where(x => x.BookId == request.Id);
    }
}
