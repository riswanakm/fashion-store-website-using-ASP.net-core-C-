using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group7_FinalProject.Models
{
    internal class SeedBrands : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> entity)
        {
            entity.HasData(
                new Brand { BrandId = 1, FirstName = "Michelle" },
                new Brand { BrandId = 2, FirstName = "Christian" }

            );
        }
    }
}
