using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services.Commands.Rents
{
    public record DeleteRentCommand(Rent Rent) : IRequest;
}
