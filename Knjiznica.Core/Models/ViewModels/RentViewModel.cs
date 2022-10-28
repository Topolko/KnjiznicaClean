using System.ComponentModel.DataAnnotations;

namespace Knjiznica.Core.Models.ViewModels
{
    public class RentViewModel
    {
        //public double MaxRentDays { get { return 14.00; } }
        public int RentId { get; set; }

        [Display(Name = "Date rented")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateRented { get; set; }

        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }

        [Display(Name = "Member name")]
        public string MemberName { get; set; }

        [Display(Name = "Is late")]
        public bool IsLate { get { return (DateRented.AddDays(Constants.MAX_RENT_DAYS) < DateTime.Today); } }
        public int BookId { get; set; } 
        public int MemberId { get; set; }   

    }
}
