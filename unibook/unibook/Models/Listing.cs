using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibook.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public string Semester { get; set; }
        public string Study { get; set; }
        public string University { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string BookISBN { get; set; }
        public string ListingImage { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}
