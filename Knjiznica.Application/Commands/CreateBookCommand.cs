using MediatR;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjiznica.Application.Commands
{
    public class CreateBookCommand : IRequest<BookResponse>
    {
        public string Title
        {
            get;
            set;
        }
        public int BrojPrimjeraka
        {
            get;
            set;
        }
        public int AuthorId
        {
            get;
            set;
        }
        public int GenreId
        {
            get;
            set;
        }
    }
}
