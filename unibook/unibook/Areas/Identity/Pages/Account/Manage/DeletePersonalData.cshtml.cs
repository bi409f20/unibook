using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using _context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SQLitePCL;
using unibook.Data;
using unibook.Models;

namespace unibook.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly UnibookContext _context;

        public DeletePersonalDataModel(
            UnibookContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<DeletePersonalDataModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public Listing listing { get; set; }
        public List<Ratings> Ratings { get; private set; }
        public List<Listing> Listings { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            Ratings = _context.Ratings.ToList();
            Listings = _context.Listings.ToList(); // SELECT * FROM Listings;
            var userId = await _userManager.GetUserIdAsync(user);
            var listing = _context.Listings.Where(u => u.UserId == userId).ToList();
            var rating = _context.Ratings.Where(u => u.UserId == userId).ToList();
            var ownrating = _context.Ratings.Where(r => r.RaterId == userId).ToList();

            if (listing != null)
            {
                _context.Listings.RemoveRange(listing);
                await _context.SaveChangesAsync();
            }
            if (rating != null)
            {
                _context.Ratings.RemoveRange(rating);
                await _context.SaveChangesAsync();
            }
            if (ownrating != null)
            {
                _context.Ratings.RemoveRange(ownrating);
                await _context.SaveChangesAsync();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
