using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Authors;
using Knjiznica.Core.Services.Queries.Books;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Infrastructure.Handlers.Authors
{
    public class GetAuthorsHandler : IQueryHandler<GetAuthorsQuery, IQueryable<Autor>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetAuthorsHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task<IQueryable<Autor>> HandleAsync(GetAuthorsQuery request) => _knjiznicaContext.Autors;
    }
}
