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
    public class DeleteListingModel : PageModel
    {
        private readonly unibook.Data.UnibookContext _context;

        public DeleteListingModel(unibook.Data.UnibookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Listing Listing { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Listing = await _context.Listings
                .Include(l => l.Book)
                .Include(l => l.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Listing == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Listing = await _context.Listings.FindAsync(id);

            if (Listing != null)
            {
                _context.Listings.Remove(Listing);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
