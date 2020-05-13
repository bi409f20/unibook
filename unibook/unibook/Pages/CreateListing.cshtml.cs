using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using unibook.Data;
using unibook.Models;

namespace unibook.Pages
{
    public class CreateListingModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly UnibookContext _context;
        public CreateListingModel(UserManager<User> userManager, UnibookContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Required]
            [Display(Name = "Price")]
            public decimal Price { get; set; }

            [Required]
            [Display(Name = "Edition")]
            public string Edition { get; set; }

            [Required]
            [Display(Name = "ISBN")]
            public string ISBN { get; set; }

            [Required]
            [Display(Name = "University")]
            public string University { get; set; }

            [Required]
            [Display(Name = "Semester")]
            public string Semester { get; set; }

            [Required]
            [Display(Name = "Study")]
            public string Study { get; set; }

            [Required]
            [Display(Name = "Author")]
            public string Author { get; set; }

            [Required]
            [Display(Name = "Description")]
            public string Description { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == Input.ISBN);
                if (book == null)
                {
                    var bookcreator = new Book()
                    {
                        Title = Input.Title,
                        Author = Input.Author,
                        Edition = Input.Edition,
                        ISBN = Input.ISBN,
                    };
                    var listingcreator = new Listing()
                    {
                        Book = book,
                        User = await _userManager.GetUserAsync(User),
                        Price = Input.Price,
                        University = Input.University,
                        Semester = Input.Semester,
                        Study = Input.Study,
                        Description = Input.Description
                    };
                _context.Listings.Add(listingcreator);
                await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }

}