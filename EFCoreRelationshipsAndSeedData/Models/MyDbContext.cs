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

            builder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(cat => cat.Products)
                .UsingEntity(junctionTable => junctionTable.HasData(
                    new { CategoriesCategoryId = 1, ProductsProductId = 1 },
                    new { CategoriesCategoryId = 2, ProductsProductId = 1 }
                ));

            //builder.Entity<Product>()
            //    .HasMany(p => p.Categories)
            //    .WithMany(cat => cat.Products)
            //    .UsingEntity(prodCats => prodCats.ToTable("CategoryProduct")); // Hidden linking table

            //Product prod1 = new Product()
            //{
            //    ProductId = 1,
            //    Price = 9.99,
            //    ProductName = "test"
            //};

            //List<Product> products = new List<Product>() { prod1 };

            //builder.Entity<Product>(p =>
            //{
            //    p.HasData(prod1);
            //    p.OwnsMany(p => p.Categories).HasData(new
            //    {
            //        CategoryId = 1, CategoryName = "Electronic", ProductId = 1
            //    });
            //});
            //builder.Entity<Product>()
            //    .OwnsOne(p => p.Categories);

            //builder.Entity<Category>()
            //    .OwnsOne(cat => cat.Products);

            //Category cat1 = new Category { CategoryId = 1, CategoryName = "Electronics" };
            //Category cat2 = new Category { CategoryId = 2, CategoryName = "Automotive" };
            //Category cat3 = new Category { CategoryId = 3, CategoryName = "Snacks" };
            //Category cat4 = new Category { CategoryId = 4, CategoryName = "Home" };

            //Product prod1 = new Product
            //{
            //    ProductId = 1,
            //    ProductName = "Test Product"
            //};

            //builder.Entity<Category>().HasData(cat1, cat2, cat3, cat4);

            //builder.Entity<Product>().HasData(prod1);
            //builder.Entity<Product>().HasData(prod1.Categories = new List<Category>() { cat1, cat2 });

        }
    }
}
