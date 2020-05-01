using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unibook.Models;


namespace unibook.Data
{
    public class DbInitializer
    {
        public static void Initialize(UnibookContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }

            var books = new Book[]
           {
                new Book{ISBN="ISBNTest",Author="AuthorTest",Edition="ETest",Title="TitleTest"},
           };
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();
        }
    }
}
