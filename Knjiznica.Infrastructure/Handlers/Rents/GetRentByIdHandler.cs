using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Members;
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
    public class GetRentByIdHandler : IQueryHandler<GetRentByIdQuery, Rent>
    {
        private readonly KnjiznicaContext _knjiznicaContext;
        public GetRentByIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task<Rent> HandleAsync(GetRentByIdQuery request) =>
            _knjiznicaContext.Rents.Single(p => p.RentId == request.Id);
    }
}
