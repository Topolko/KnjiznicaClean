using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Queries.Genres
{
    public record GetAllGenresQuery() : IRequest<IEnumerable<Genre>>;
}
