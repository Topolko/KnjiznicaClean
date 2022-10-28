using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Core.Services.Commands.Members;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handleres.Members
{
    public class EditMemberHandler : ICommandHandler<EditMemberCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public EditMemberHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync(EditMemberCommand request )
        {
            _knjiznicaContext.Members.Update(request.Member);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
