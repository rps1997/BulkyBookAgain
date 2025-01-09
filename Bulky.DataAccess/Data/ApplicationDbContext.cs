using Bulky.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1, CreatedDateTime = DateTime.Now },
                new Category { Id = 2, Name = "Romance", DisplayOrder = 2, CreatedDateTime = DateTime.Now }
                );

            //modelBuilder.Entity<Product>().HasData(
            //    new Product { Id = 1, Title = "Harry Potter", Author = "JK Rowling", Description = "harry potter book", ISBN = "IS547998", ListPrice = 99, Price = 90, Price50 = 85, Price100 = 80}
            //    );
        }
    }
}
