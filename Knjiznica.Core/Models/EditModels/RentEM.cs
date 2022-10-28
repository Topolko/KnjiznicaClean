using Knjiznica.Core.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace Knjiznica.Core.Models.EditModels
{
    public class RentEM
    {
        public int RentId { get; set; }
        [Display(Name = "Date rented")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateRented { get; set; }
        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }
        [Display(Name = "Member name")]
        public string MemberName { get; set; }
        [Display(Name = "Is late")]
        public bool IsLate { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }

        public virtual Book? Book { get; set; } 
        public virtual Member? Member { get; set; }

        public Dictionary<int, string>? Members { get; set; }
        public Dictionary<int, string>? Books { get; set; }

    }
}
