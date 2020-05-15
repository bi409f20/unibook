using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using unibook.Models;
using unibook.Data;
using Microsoft.EntityFrameworkCore;

namespace unibook.Pages
{
    public class ListingModel : PageModel
    {
        private readonly UnibookContext _context;
        public ListingModel(UnibookContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Listing = await _context.Listings.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == id);
            //User = await _context.Users.Include(s => s.Name).FirstOrDefaultAsync();

            if (Listing == null) 
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public void OnPost()
        {
        }

        public User User { get; set; }
        public Listing Listing { get; set; }
        public List<Listing> Listings { get; private set; }
    }
}