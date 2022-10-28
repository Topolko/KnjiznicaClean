using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Knjiznica.Infrastructure.Handleres.Genres
{
    public class AddGenreCommandHandler : ICommandHandler<AddGenreCommand>
    {
        readonly KnjiznicaContext _knjiznicaContext;

        public AddGenreCommandHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task HandleAsync(AddGenreCommand command)
        {
            _knjiznicaContext.Add(command.Genre);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }





    ////public class AddGenreHandler: IRequestHandler<AddGenreCommand, Unit>
    ////{
    ////    private readonly KnjiznicaContext _knjiznicaContext;

    ////    public AddGenreHandler(KnjiznicaContext knjiznicaContext) => _knjiznicaContext = knjiznicaContext;
    ////    public async Task<Unit> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    ////    {
    ////        _knjiznicaContext.Genres.Add(request.Genre);
    ////        await _knjiznicaContext.SaveChangesAsync();
    ////        return Unit.Value;
    ////    }
    ////}
}
