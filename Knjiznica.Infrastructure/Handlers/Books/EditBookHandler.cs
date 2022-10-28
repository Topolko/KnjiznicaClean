using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Books;
using Knjiznica.Infrastructure.DBContext;
using MediatR;

namespace Knjiznica.Infrastructure.Handleres.Books
{
    public class EditBookHandler : ICommandHandler<EditBookCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public EditBookHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }
        public async Task HandleAsync(EditBookCommand request)
        {
            _knjiznicaContext.Books.Update(request.Book);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
