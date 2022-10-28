using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Commands.Genres
{
    public record EditGenreCommand(Genre Genre): IRequest;
}
