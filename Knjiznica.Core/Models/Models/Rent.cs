using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Knjiznica.Core.Models.Models
{
    public partial class Rent
    {
        int MaxRentDays = 14;
        public int RentId { get; set; }
        public DateTime DateRented { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public bool IsLate { get { return (DateRented.AddDays(MaxRentDays) < DateTime.Today); } }

        public virtual Book? Book { get; set; } = null!;
        public virtual Member? Member { get; set; } = null!;
    }
}
