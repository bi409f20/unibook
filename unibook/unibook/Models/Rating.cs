using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibook.Models
{
    public class Ratings
    {
        public int RatingId { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; } 

        public User User { get; set; }    
    }
}
