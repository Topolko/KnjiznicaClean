using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Queries.Genres
{
    public record GetGenreByIdQuery(int Id) : IRequest<Genre>;
}
