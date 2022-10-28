using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Rents;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Infrastructure.Handleres.Rents
{
    public class GetRentsWithMemberIdHandler : IQueryHandler<GetRentsWithMemberIdQuery, IQueryable<Rent>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetRentsWithMemberIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<IQueryable<Rent>> HandleAsync(GetRentsWithMemberIdQuery request
            ) => _knjiznicaContext.Rents.Where(x=>x.MemberId == request.Id);
    }
}
