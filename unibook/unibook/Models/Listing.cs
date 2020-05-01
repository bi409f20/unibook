using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibook.Models
{
    public class Listing
    {
        public string Semester { get; set; }
        public string Study { get; set; }
        public string University { get; set; }
        public int Listing_ID { get; set; }
        public int User_ID { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }


        public User User { get; set; }
        public Book Book { get; set; }
    }
}
