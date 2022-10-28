using Knjiznica.Core.Services;
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
    public class AddRentHandler : ICommandHandler<AddRentCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public AddRentHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync(AddRentCommand request)
        {
            _knjiznicaContext.Rents.Add(request.Rent);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
