using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Members;
using Knjiznica.Core.Services.Queries.Rents;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System.Linq;

namespace Knjiznica.Infrastructure.Handleres.Rents
{
    public class GetRentsHandler : IQueryHandler<GetRentsQuery, IQueryable<Rent>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetRentsHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<IQueryable<Rent>> HandleAsync(GetRentsQuery request) =>_knjiznicaContext.Rents;
    }
}
