using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Commands.Genres
{
    public record AddGenreCommand(Genre Genre) : IRequest;
}
