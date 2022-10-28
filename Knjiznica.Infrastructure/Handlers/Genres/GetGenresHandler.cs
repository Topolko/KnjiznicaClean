using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System.Data.Entity;
using System.Linq;

namespace Knjiznica.Infrastructure.Handleres.Genres
{
    public class GetAllGenresHandler : IQueryHandler<GetAllGenresQuery, IEnumerable<Genre>>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public GetAllGenresHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task <IEnumerable<Genre>> HandleAsync(GetAllGenresQuery query) => _knjiznicaContext.Genres;
    }
}
