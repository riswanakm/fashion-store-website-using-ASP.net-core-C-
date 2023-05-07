using Newtonsoft.Json;

namespace Group7_FinalProject.Models
{
    public class ProductsGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Brand { get; set; } = DefaultFilter;
        public string Category { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
    }
}
