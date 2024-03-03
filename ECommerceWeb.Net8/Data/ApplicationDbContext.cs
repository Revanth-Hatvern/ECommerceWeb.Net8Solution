using ECommerceWeb.Net8.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Net8.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { C_Id = 1, Name = "Action", DisplayOrder = 1 },
                 new Category { C_Id = 2, Name = "SciFi", DisplayOrder = 2 },
                  new Category { C_Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }
    }
}
