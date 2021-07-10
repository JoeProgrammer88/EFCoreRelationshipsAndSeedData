using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreRelationshipsAndSeedData.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasData(new Product { ProductId = 1, ProductName = "Test", Price = 9.99 });

            builder.Entity<Category>()
                .HasData
                (
                    new Category { CategoryId = 1, CategoryName = "Electronics" },
                    new Category { CategoryId = 2, CategoryName = "Home" }
                );

            // Have to manually add rows to linking/junction table
            builder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(cat => cat.Products)
                .UsingEntity(junctionTable => junctionTable.HasData(
                    // Notice these are anonymous types because the linking table
                    // is not definited in the DBContext
                    new { CategoriesCategoryId = 1, ProductsProductId = 1 },
                    new { CategoriesCategoryId = 2, ProductsProductId = 1 }
                ));
        }
    }
}
