using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Commands.Members
{
    public record DeleteMemberCommand(Member Member) : IRequest;
}
