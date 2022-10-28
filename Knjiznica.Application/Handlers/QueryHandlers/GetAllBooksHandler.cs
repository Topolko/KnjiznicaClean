using Knjiznica.Application.Queries;
using Knjiznica.Core.Repositories;
using Knjiznica.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Knjiznica.Application.Handlers.QueryHandlers
{
    public class GetAllEmployeeHandler : IRequestHandler<GetAllBooksQuery, List<Knjiznica.Core.Models.Models.Book>>
    {
        private readonly IBookRepository _bookRepo;

        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            _bookRepo = bookRepository;
        }
        public async Task<List<Core.Models.Models.Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return (List<Core.Models.Models.Book>)await _bookRepo.GetAllAsync();
        }
    }
}
