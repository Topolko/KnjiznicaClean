using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Rents;
using Knjiznica.Infrastructure.DBContext;
using MediatR;


namespace Knjiznica.Infrastructure.Handleres.Rents
{
    public class EditRentHandler : ICommandHandler<EditRentCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public EditRentHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync(EditRentCommand request)
        {
             _knjiznicaContext.Rents.Update(request.Rent);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
