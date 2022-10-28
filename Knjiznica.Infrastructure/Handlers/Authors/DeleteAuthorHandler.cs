using Knjiznica.Core.Services;
using Knjiznica.Core.Services.Commands.Authors;
using Knjiznica.Core.Services.Commands.Books;
using Knjiznica.Infrastructure.DBContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Infrastructure.Handlers.Authors
{
    public class DeleteAuthorHandler : ICommandHandler<DeleteAuthorCommand>
    {
        private readonly KnjiznicaContext _knjiznicaContext;

        public DeleteAuthorHandler(KnjiznicaContext knjiznicaContext)
        {
            _knjiznicaContext = knjiznicaContext;
        }

        public async Task HandleAsync(DeleteAuthorCommand request)
        {
            _knjiznicaContext.Autors.Remove(request.Autor);
            await _knjiznicaContext.SaveChangesAsync();
        }
    }
}
