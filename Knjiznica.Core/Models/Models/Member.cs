using System;
using System.Collections.Generic;

namespace Knjiznica.Core.Models.Models
{
    public partial class Member
    {
        public Member()
        {
            Rents = new HashSet<Rent>();
        }

        public int MemberId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
