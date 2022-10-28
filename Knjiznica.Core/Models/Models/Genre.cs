using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Knjiznica.Core.Models.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public int GenreId { get; set; }

        [Display(Name = "Genre")]
        public string? GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
