using System.ComponentModel.DataAnnotations;

namespace Knjiznica.Core.Models.Models
{
    public partial class Autor
    {
        public Autor()
        {
            Books = new HashSet<Book>();
        }

        public int AutorId { get; set; }

        [Display(Name = "First name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last name")]
        public string? LastName { get; set; }
        [Display(Name = "Autor")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public virtual ICollection<Book> Books { get; set; }
    }
}
