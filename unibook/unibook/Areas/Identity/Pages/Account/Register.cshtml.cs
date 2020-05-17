using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using unibook.Models;
using unibook.Data;

namespace unibook.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IHostingEnvironment hostingEnvironment;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IHostingEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.hostingEnvironment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public User User { set; get; }

        [BindProperty]
        public IFormFile ImageNameInput { set; get; }
    

    public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "City")]
            public string City { get; set; }
            [Display(Name = "Postal code")]
            public string PostalCode { get; set; }

            [Display(Name = "Address")]
            public string Address { get; set; }

            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "University")]
            public string University { get; set; }

            [Display(Name = "Image")]
            public string ImageName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
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
                var fileName = ImageNameInput == null ? "DefaultImage/default-avatar.jpg" : GetUniqueName(ImageNameInput.FileName);
                var Images = Path.Combine(hostingEnvironment.WebRootPath, "Images/UserImages");
                if (ImageNameInput != null)
                {
                    var filePath = Path.Combine(Images, fileName);
                    this.ImageNameInput.CopyTo(new FileStream(filePath, FileMode.Create));
                    this.User.ImageName = fileName; // Set the file name
                }

                var user = new User { Email = Input.Email, UserName = Input.Email, Name = Input.Name, University = Input.University, City = Input.City, Address = Input.Address, PostalCode = Input.PostalCode, ImageName = fileName};
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            // If we got this far, something failed, redisplay form
            return Page();
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
