using Group7_FinalProject.Models;
using System.Collections.Generic;

namespace Group7_FinalProject.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Dictionary<string, string> Prices =>
            new Dictionary<string, string> {
                { "under7", "Under $7" },
                { "7to14", "$7 to $14" },
                { "over14", "Over $14" }
            };
    }
}
