using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Authors;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handlers.Authors
{
    public class EditAuthorHandler : ICommandHandler<EditAuthorCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public EditAuthorHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync(EditAuthorCommand request)
        {
            _knjiznicaContext.Autors.Update(request.Author);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
