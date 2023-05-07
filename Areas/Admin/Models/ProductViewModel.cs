using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Group7_FinalProject.Models
{
    public class ProductViewModel : IValidatableObject
    {
        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public int[] SelectedBrands { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (SelectedBrands == null)
            {
                yield return new ValidationResult(
                  "Please select at least one brand",
                  new[] { nameof(SelectedBrands) });
            }
        }
    }
}
