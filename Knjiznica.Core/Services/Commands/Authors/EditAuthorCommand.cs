using Knjiznica.Core.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Core.Services.Commands.Authors
{
    public record EditAuthorCommand(Autor Author) : IRequest;
}
