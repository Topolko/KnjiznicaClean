using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handleres.Genres
{
    public class EditGenreHandler : ICommandHandler<EditGenreCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public EditGenreHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync(EditGenreCommand command)
        {
            _knjiznicaContext.Genres.Update(command.Genre);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
