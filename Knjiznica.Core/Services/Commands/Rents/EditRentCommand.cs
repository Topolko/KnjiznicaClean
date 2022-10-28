using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Commands.Rents
{
    public record EditRentCommand(Rent Rent) : IRequest;
}
