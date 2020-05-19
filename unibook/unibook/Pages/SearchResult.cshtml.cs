using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using unibook.Data;
using unibook.Models;

namespace unibook.Pages
{
    public class SearchResultModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UnibookContext _context;
        public SearchResultModel(ILogger<IndexModel> logger, UnibookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet(string SearchString)
        {
            SearchString = (SearchString ?? "").ToLower();

            Listings = _context.Listings
                .Where(b => b.Book.Title.ToLower().Contains(SearchString) || b.Book.ISBN.ToLower().Contains(SearchString) || b.Book.Author.ToLower().Contains(SearchString))
                .ToList(); // SELECT * FROM Listings WHERE title LIKE '%test%';
        }
        public void OnPost(string SearchString)
        {
            SearchString = (SearchString ?? "").ToLower();

            Listings = _context.Listings
                .Where(b => b.Book.Title.ToLower().Contains(SearchString) || b.Book.ISBN.ToLower().Contains(SearchString) || b.Book.Author.ToLower().Contains(SearchString))
                .ToList(); // SELECT * FROM Listings WHERE title LIKE '%test%';
        }
        public List<Book> Books { get; set; }
        public Listing listing { get; set; }
        public User user { get; set; }
        public List<Listing> Listings { get; private set; }
    }
}