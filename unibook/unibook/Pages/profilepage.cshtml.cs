using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using unibook.Data;
using unibook.Models;

namespace unibook.Pages
{
    public class profilepageModel : PageModel
    {
        private readonly UnibookContext _context;
        public profilepageModel(UnibookContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            Listings = await _context.Listings.Include(l => l.Book).Where(l => l.UserId == id).ToListAsync();
            User = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (Listings == null)
                {
                    return RedirectToPage("./Index");
                }
                return Page();
        }
        public User User { get; set; }
        public Listing Listing { get; set; }
        public List<Listing> Listings { get; private set; }
    }
}