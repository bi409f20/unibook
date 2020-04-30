using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace unibook.Models
{
    public class UnibookContext : DbContext
    {
        public DbSet<Books> Books { get; set; }

        public UnibookContext(DbContextOptions<UnibookContext> options)
            : base(options) { }
    }
}
