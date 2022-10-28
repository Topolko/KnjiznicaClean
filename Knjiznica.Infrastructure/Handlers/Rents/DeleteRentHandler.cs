using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Members;
using Knjiznica.Core.Services.Commands.Rents;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Infrastructure.Handleres.Rents
{
    public class DeleteRentHandler : ICommandHandler<DeleteRentCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public DeleteRentHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task HandleAsync(DeleteRentCommand request)
        {
            _knjiznicaContext.Rents.Remove(request.Rent);
            await _knjiznicaContext.SaveChangesAsync();

        }
    }
}
