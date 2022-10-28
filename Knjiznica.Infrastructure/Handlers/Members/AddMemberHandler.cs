using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Genres;
using Knjiznica.Core.Services.Commands.Members;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handleres.Members
{
    public class AddMemberHandler : ICommandHandler<AddMemberCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;
        
        public AddMemberHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync (AddMemberCommand request)
        {
            _knjiznicaContext.Members.Add(request.Member);
            await _knjiznicaContext.SaveChangesAsync();

        }
    }
}
