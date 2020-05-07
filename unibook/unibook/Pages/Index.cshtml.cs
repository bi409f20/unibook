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
        private readonly UnibookContext context;

        public IndexModel(ILogger<IndexModel> logger, UnibookContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public void OnGet()
        {
            Book = context.Books.FirstOrDefault();
        }

        public Book Book { get; set; }
        public Listing listing { get; set; }
        public User user { get; set; }

    }
}
