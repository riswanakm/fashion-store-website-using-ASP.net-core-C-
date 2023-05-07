using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Group7_FinalProject.Models
{  public class FashionstoreContext : IdentityDbContext<User>
    {
        public FashionstoreContext(DbContextOptions<FashionstoreContext> options)
    : base(options)
        { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BookAuthor: set primary key 
            modelBuilder.Entity<ProductBrand>().HasKey(ba => new { ba.ProductId, ba.BrandId });

            // BookAuthor: set foreign keys 
            modelBuilder.Entity<ProductBrand>().HasOne(ba => ba.Product)
                .WithMany(b => b.ProductBrands)
                .HasForeignKey(ba => ba.ProductId);
            modelBuilder.Entity<ProductBrand>().HasOne(ba => ba.Brand)
                .WithMany(a => a.ProductBrands)
                .HasForeignKey(ba => ba.BrandId);

            // Book: remove cascading delete with Category
            modelBuilder.Entity<Product>().HasOne(b => b.Category)
                .WithMany(g => g.Products)
                .OnDelete(DeleteBehavior.Restrict);

            // seed initial data
            modelBuilder.ApplyConfiguration(new SeedCategories());
            modelBuilder.ApplyConfiguration(new SeedProducts());
            modelBuilder.ApplyConfiguration(new SeedBrands());
            modelBuilder.ApplyConfiguration(new SeedProductBrands());
        }
    }
}
