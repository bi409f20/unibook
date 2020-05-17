using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public profilepageModel(UnibookContext context, UserManager<User> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var ownUser = await _userManager.GetUserAsync(User);
            ShowEdit = ownUser.Id.Equals(id);
            Listings = await _context.Listings.Include(l => l.Book).Where(l => l.UserId == id).ToListAsync();
            Profile = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (Listings == null)
                {
                    return RedirectToPage("./Index");
                }
                return Page();
        }
        public User Profile { get; set; }
        public Listing Listing { get; set; }
        public List<Listing> Listings { get; private set; }
        public bool ShowEdit { get; set; }
    }
}