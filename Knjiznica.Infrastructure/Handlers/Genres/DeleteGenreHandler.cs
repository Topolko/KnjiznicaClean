using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handleres.Genres
{
    public class DeleteGenreHandler : ICommandHandler<DeleteGenreCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public DeleteGenreHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task HandleAsync(DeleteGenreCommand command)
        {
            _knjiznicaContext.Genres.Remove(command.Genre);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
