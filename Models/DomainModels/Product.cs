using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Group7_FinalProject.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter brand name.")]
        [StringLength(200)]
        public string Name { get; set; }

        [Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<ProductBrand> ProductBrands { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }


        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
