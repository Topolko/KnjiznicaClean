using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Knjiznica.Core.Models.Models
{
    public partial class Book
    {
        public Book()
        {
            Rents = new HashSet<Rent>();
        }

        public int BookId { get; set; }
        public string? Title { get; set; }
        public int BrojPrimjeraka { get; set; }
        public int AutorId { get; set; }
        public int GenreId { get; set; }

        public virtual Autor? Autor { get; set; } = null!;
        public virtual Genre? Genre { get; set; } = null!;
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
