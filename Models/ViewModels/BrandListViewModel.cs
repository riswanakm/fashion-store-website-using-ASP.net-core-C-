using Group7_FinalProject.Models;
using System.Collections.Generic;

namespace Group7_FinalProject.Models
{
    public class BrandListViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}
