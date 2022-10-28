using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Core.Services.Queries.Members;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handleres.Members
{
    public class GetMemberByIdHandler : IQueryHandler<GetMemberByIdQuery, Member>
    {
        private readonly KnjiznicaContext _knjiznicaContext;
        public GetMemberByIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<Member?> HandleAsync(GetMemberByIdQuery request)
        {
           return _knjiznicaContext.Members.Single(p => p.MemberId == request.Id);
        }
    }
}
