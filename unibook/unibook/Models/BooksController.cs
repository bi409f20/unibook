using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace unibook.Models
{
    public class BooksController : ControllerBase
    {
        private readonly UnibookContext _context;

        public BooksController(UnibookContext context)
        {
            _context = context;
        }
    }
}