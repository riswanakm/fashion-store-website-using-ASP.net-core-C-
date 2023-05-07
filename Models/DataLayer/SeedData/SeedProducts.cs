using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group7_FinalProject.Models
{
    internal class SeedProducts : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasData(
                new Product { ProductId = 1, Name = "Long shirt", CategoryId = "tshirt", Price = 18.00,ImageName= "balloon-sleeved dress230504665.jpeg" },
                new Product { ProductId = 2, Name = "fancy skirt", CategoryId = "skirt", Price = 22.00, ImageName = "Cut-out Dress.jpeg" }

            );
        }
    }
}
