using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Queries.Genres;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Knjiznica.Infrastructure.Handleres.Genres
{
    public class GetGenreByIdHandler : IQueryHandler<GetGenreByIdQuery, Genre?>
    {
        private readonly KnjiznicaContext _knjiznicaContext;
        public GetGenreByIdHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }


        public Task<Genre?> HandleAsync(GetGenreByIdQuery request)
        {
            return _knjiznicaContext.Genres.FirstOrDefaultAsync((g) => g.GenreId == request.Id);
        }

        ////public async Task<Genre> HandleOLD(GetGenreByIdQuery request, CancellationToken cancellationToken) =>
        ////    _knjiznicaContext.Genres.Single(p => p.GenreId == request.Id);
    }
}
