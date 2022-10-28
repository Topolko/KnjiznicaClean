using AutoMapper;
using Knjiznica.Application.Commands;
using Knjiznica.Application.Mappers;
using Knjiznica.Application.Responses;
using Knjiznica.Core.Models.Models;
using Knjiznica.Core.Repositories;
using Knjiznica.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Application.Handlers.CommandHandlers
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, BookResponse>
    {
        private readonly IBookRepository _bookRepo;
        public CreateBookHandler(BookRepository bookRepository)
        {
            _bookRepo = bookRepository;
        }
        public async Task<BookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var bookEntitiy = BookMapper.Mapper.Map<Core.Models.Models.Book>(request);
            if (bookEntitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newBook = await _bookRepo.AddAsync(bookEntitiy);
            var bookResponse = BookMapper.Mapper.Map<BookResponse>(newBook);
            return bookResponse;
        }
    }
}
