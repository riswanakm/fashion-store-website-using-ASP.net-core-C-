using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group7_FinalProject.Models
{
    public class Category
    {
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter a category id.")]
        [Remote("CheckGenre", "Validation", "Area")]
        public string CategoryId { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "Please enter a category name.")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
