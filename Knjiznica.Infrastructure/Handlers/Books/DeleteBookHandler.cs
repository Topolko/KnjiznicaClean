
using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Books;
using Knjiznica.Infrastructure.DBContext;
using MediatR;


namespace Knjiznica.Infrastructure.Handleres.Books
{
    public class DeleteBookHandler : ICommandHandler<DeleteBookCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public DeleteBookHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task HandleAsync(DeleteBookCommand request)
        {
            _knjiznicaContext.Books.Remove(request.Book);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
