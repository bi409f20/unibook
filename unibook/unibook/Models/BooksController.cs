using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace unibook.Models
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            UnibookContext context = HttpContext.RequestServices.GetService(typeof(unibook.Models.UnibookContext)) as UnibookContext;

            return View(context.GetAllBooks());
        }
    }
}