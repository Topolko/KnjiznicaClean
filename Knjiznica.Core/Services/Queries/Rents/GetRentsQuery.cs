using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Models.ViewModels;
using MediatR;
using System.Linq;

namespace Knjiznica.Core.Services.Queries.Rents
{
    public record GetRentsQuery() : IRequest<IQueryable<Rent>>;
}
