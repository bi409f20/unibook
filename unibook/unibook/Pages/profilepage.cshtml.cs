using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Rating")]
            public int Rating { get; set; }
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var ownUser = await _userManager.GetUserAsync(User);
            ShowEdit = ownUser.Id.Equals(id);
            Listings = await _context.Listings.Include(l => l.Book).Where(l => l.UserId == id).ToListAsync();
            await LoadProfileAsync(id);
            if (Listings == null)
                {
                    return RedirectToPage("./Index");
                }
                return Page();
        }

        private async Task LoadProfileAsync(string id)
        {
            Profile = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IActionResult> OnPostAsync(string id, string returnUrl = null)
        {
            await LoadProfileAsync(id);

            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.Rating == Input.Rating);
                if (rating == null)
                {
                    var ratingcreator = new Ratings()
                    {
                        Rating = Input.Rating,
                        User = await _userManager.GetUserAsync(User),
                    };
                    _context.Ratings.Add(ratingcreator);
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
        public Ratings Rating { get; set; }
        public User Profile { get; set; }
        public Listing Listing { get; set; }
        public List<Listing> Listings { get; private set; }
        public bool ShowEdit { get; set; }
    }
}