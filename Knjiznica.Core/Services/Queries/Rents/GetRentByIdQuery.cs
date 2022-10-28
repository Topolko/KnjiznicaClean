using Knjiznica.Core.Models.Models;
using MediatR;


namespace Knjiznica.Core.Services.Queries.Rents
{
    public record GetRentByIdQuery(int Id) : IRequest<Rent>;
}
