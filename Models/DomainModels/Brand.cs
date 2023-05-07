using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group7_FinalProject.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(200)]
        public string FirstName { get; set; }

        public string FullName => $"{FirstName}";

        public ICollection<ProductBrand> ProductBrands { get; set; }
    }
}
