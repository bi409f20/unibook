using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using unibook.Data;
using unibook.Models;

namespace unibook.Pages
{
    public class EditListingModel : PageModel
    {
        private readonly unibook.Data.UnibookContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<User> _userManager;

        public EditListingModel(unibook.Data.UnibookContext context, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _context = context;
            hostingEnvironment = environment;
            _userManager = userManager;
        }
        
        [BindProperty]
        public Listing Listing { get; set; }
        [BindProperty]
        public IFormFile ListingImageInput { set; get; }

        [Display(Name = "Image")]
        public string ListingImage { get; set; }

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
            ViewData["BookISBN"] = new SelectList(_context.Books, "ISBN", "ISBN");
            ViewData["ListingImage"] = new SelectList(_context.Listings, "ListingImage", "ListingImage");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }        
            _context.Attach(Listing).State = EntityState.Modified;
            if (ListingImageInput == null)
            {
                Listing.ListingImage = ListingImage;
            }
            if (ListingImageInput != null)
            {
                var fileName = GetUniqueName(ListingImageInput.FileName);
                var Images = Path.Combine(hostingEnvironment.WebRootPath, "Images/ListingImages");
                var filePath = Path.Combine(Images, fileName);
                this.ListingImageInput.CopyTo(new FileStream(filePath, FileMode.Create));
                this.Listing.ListingImage = fileName; // Set the file name
                Listing.ListingImage = fileName;
            }
            Listing.User = await _userManager.GetUserAsync(User);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(Listing.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ListingExists(int id)
        {
            return _context.Listings.Any(e => e.Id == id);
        }
        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
