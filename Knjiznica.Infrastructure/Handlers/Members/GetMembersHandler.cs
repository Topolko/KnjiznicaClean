using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Core.Services.Queries.Members;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System.Data.Entity;

namespace Knjiznica.Infrastructure.Handleres.Members
{
    public class GetMembersHandler : IQueryHandler<GetMembersQuery, IQueryable<Member>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetMembersHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task <IQueryable<Member>> HandleAsync(GetMembersQuery request)
            => _knjiznicaContext.Members;
    }
}
