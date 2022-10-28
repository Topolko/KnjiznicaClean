using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Core.Services.Commands.Members;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handleres.Members
{
    public class DeleteMemberHandler : ICommandHandler<DeleteMemberCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public DeleteMemberHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task HandleAsync(DeleteMemberCommand request)
        {
            _knjiznicaContext.Members.Remove(request.Member);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
