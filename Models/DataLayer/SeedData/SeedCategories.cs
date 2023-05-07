using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group7_FinalProject.Models
{
    internal class SeedCategories : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasData(
                new Category { CategoryId = "tshirt", Name = "TShirt" },
                new Category { CategoryId = "skirt", Name = "Skirt" }
            );
        }
    }
}
