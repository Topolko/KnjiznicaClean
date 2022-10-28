using Knjiznica.Core.Models.Models;
using MediatR;


namespace Knjiznica.Core.Services.Commands.Authors
{
    public record DeleteAuthorCommand(Autor Autor) : IRequest;
}
