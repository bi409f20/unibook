using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using unibook.Models;
using unibook.Data;
using Microsoft.EntityFrameworkCore.Query;
using System.Data.Entity;

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
            Listings = _context.Listings.ToList(); // SELECT * FROM Listings;
            Books = _context.Books.ToList();
        }

        public void OnPost(string query)
        {
            query = (query ?? "").ToLower();

            Listings = _context.Listings
                .Where(b => b.Book.Title.ToLower().Contains(query) || 
                b.Book.ISBN.ToLower().Contains(query) || 
                b.Book.Author.ToLower().Contains(query) || 
                b.Study.ToLower().Contains(query))
                .ToList(); // SELECT * FROM Listings WHERE title LIKE '%test%';
            Books = _context.Books
                .Where(i => i.Author.ToLower().Contains(query)).ToList();
        }

        public List<Book> Books { get; private set; }
        public Listing listing { get; set; }
        public User user { get; set; }
        public List<Listing> Listings { get; private set; }
    }
}
