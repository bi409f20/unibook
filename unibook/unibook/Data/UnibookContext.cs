using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using unibook.Models;

namespace unibook.Data
{
    public class UnibookContext : IdentityDbContext
    {
        public UnibookContext(DbContextOptions<UnibookContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Listing>().ToTable("Listings");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }

        private class ListingConfiguration : IEntityTypeConfiguration<Listing>
        {
            public void Configure(EntityTypeBuilder<Listing> builder)
            {
                builder.HasOne(l => l.Book).WithMany(b => b.Listings);
            }
        }

        private class BookConfiguration : IEntityTypeConfiguration<Book>
        {
            public void Configure(EntityTypeBuilder<Book> builder)
            {
                builder.HasKey(b => b.ISBN);
            }
        }
    }
}