using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using unibook.Models;
using unibook.Data;

namespace unibook.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UnibookContext _context;

        public IndexModel(ILogger<IndexModel> logger, UnibookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Books = _context.Books.ToList(); // SELECT * FROM Books;
        }

        public void OnPost(string query)
        {
            query = (query ?? "").ToLower();

            Books = _context.Books
                .Where(b => b.Title.ToLower().Contains(query) || b.ISBN.ToLower().Contains(query) || b.Author.ToLower().Contains(query))
                .ToList(); // SELECT * FROM Books WHERE title LIKE '%test%';
        }

        public List<Book> Books { get; set; }
        public Listing listing { get; set; }
        public User user { get; set; }

    }
}
