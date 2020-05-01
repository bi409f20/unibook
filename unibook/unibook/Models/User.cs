using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibook.Models
{
    public class User
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string password { get; set; }
        public int OwnedListings { get; set; }
        public double Rating { get; set; }


        public Listing Listing { get; set; }

    }
}
