using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Authors;
using Knjiznica.Core.Services.Commands.Books;
using Knjiznica.Infrastructure.DBContext;
using MediatR;


namespace Knjiznica.Infrastructure.Handlers.Authors
{
    public class AddAuthorHandler : ICommandHandler<AddAuthorCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public AddAuthorHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync(AddAuthorCommand request)
        {
            _knjiznicaContext.Autors.Add(request.Autor);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
