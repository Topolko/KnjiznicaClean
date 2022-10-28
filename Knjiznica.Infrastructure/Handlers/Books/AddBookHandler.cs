using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Books;
using Knjiznica.Infrastructure.DBContext;
using MediatR;


namespace Knjiznica.Infrastructure.Handleres.Books
{
    public class AddBookHandler : ICommandHandler<AddBookCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public AddBookHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync(AddBookCommand request)
        {
            _knjiznicaContext.Books.Add(request.Book);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
