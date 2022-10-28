using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Commands.Books
{
    public record AddBookCommand(Book Book) : IRequest;
}
