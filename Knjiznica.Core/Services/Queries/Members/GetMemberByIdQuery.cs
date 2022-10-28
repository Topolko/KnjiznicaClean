using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Queries.Members
{
    public record GetMemberByIdQuery(int Id) : IRequest<Member>;
}
