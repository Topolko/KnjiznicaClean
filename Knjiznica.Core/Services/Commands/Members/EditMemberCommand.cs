using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Commands.Members
{
    public record EditMemberCommand(Member Member) : IRequest;
}
