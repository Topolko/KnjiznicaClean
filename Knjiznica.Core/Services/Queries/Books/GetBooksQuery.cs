using Knjiznica.Core.Models.Models;
using MediatR;

namespace Knjiznica.Core.Services.Queries.Books
{
    public record GetBooksQuery() : IRequest<IQueryable<Book>>;
}
