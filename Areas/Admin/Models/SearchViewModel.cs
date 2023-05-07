using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group7_FinalProject.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        [Required(ErrorMessage = "Please enter a search term.")]
        public string SearchTerm { get; set; }

        public string Type { get; set; }
        public string Header { get; set; }
    }
}
