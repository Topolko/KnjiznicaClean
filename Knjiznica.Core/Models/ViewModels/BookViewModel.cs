using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Knjiznica.Core.Models.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        [Display(Name = "Book Title")]
        public String? Title { get; set; }
        [Display(Name = "Autor")]
        public String? AuthorFullName { get; set; }
        [Display(Name = "Genre")]
        public string? GenreName { get; set; }
        [Required(ErrorMessage = "Number od copies is required")]
        [Display(Name = "Number od copies")]
        [Range(0, 1000, ErrorMessage = "Enter a value between 0 and 1000")]
        public int BrojPrimjeraka { get; set; }

        public string? AutorLastName { get; set; }
        
        public int AutorId { get; set; }

        public int GenreId { get; set; }

    }
}
