using Knjiznica.Core.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services.Queries.Rents
{
    public record GetRentsWithBookIdQuery(int Id) : IRequest<IQueryable<Rent>>;
}
