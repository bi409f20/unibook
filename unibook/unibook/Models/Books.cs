using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibook.Models
{
    public class Books
    {
        private UnibookContext context;
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string Title { get; set; }


    }
}
