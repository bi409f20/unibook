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
using Microsoft.AspNetCore.Identity;

namespace unibook.Pages
{
    public class ListingModel : PageModel
    {
        private readonly UnibookContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public ListingModel(UnibookContext context,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        private async Task LoadProfileAsync(string id)
        {
            var ownUser = await _userManager.GetUserAsync(User);
            ShowEdit = ownUser.Id.Equals(id);
            AdminEdit = ownUser.IsAdmin;  
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Listing = await _context.Listings.Include(l => l.Book).Include(l => l.User).FirstOrDefaultAsync(l => l.Id == id);
            if(_signInManager.IsSignedIn(User))
            { 
            var userid = Listing.UserId;
            await LoadProfileAsync(userid);
            }
            if (Listing == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public void OnPost()
        {
        }

        public User Profile { get; set; }
        public Listing Listing { get; set; }
        public List<Listing> Listings { get; private set; }
        public bool ShowEdit { get; set; }
        public bool AdminEdit { get; set; }
    }
}