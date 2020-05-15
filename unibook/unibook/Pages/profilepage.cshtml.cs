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
                User = await _context.Users.Include(l => l.Id).FirstOrDefaultAsync(l => l.Id == id);

                if (User == null)
                {
                    return RedirectToPage("./Index");
                }
                return Page();
        }
        public User User { get; set; }
    }
}