using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Commands.Rents
{
    public record AddRentCommand(Rent Rent) : IRequest;
}
