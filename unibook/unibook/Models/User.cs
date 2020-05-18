using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibook.Models
{
    public class User : IdentityUser
    {
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string University { get; set; }
        public virtual string ImageName { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode {get; set;}
        public double Rating => Ratings.Select(r => r.Rating).Average();


        public ICollection<Listing> Listings { get; set; }
        public ICollection<Ratings> Ratings { get; set; }

    }
}

