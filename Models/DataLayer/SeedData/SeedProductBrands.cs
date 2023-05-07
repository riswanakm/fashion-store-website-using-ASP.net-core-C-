using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group7_FinalProject.Models
{
    internal class SeedProductBrands : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> entity)
        {
            entity.HasData(
                new ProductBrand { ProductId = 1, BrandId = 2  },
                new ProductBrand { ProductId = 2, BrandId = 1 }
            );
        }
    }
}
