using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibook.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string Title { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}
