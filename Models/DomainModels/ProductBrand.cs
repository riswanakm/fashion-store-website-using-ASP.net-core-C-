namespace Group7_FinalProject.Models
{
    public class ProductBrand
    {
        public int ProductId { get; set; }
        public int BrandId { get; set; }

        public Brand Brand { get; set; }
        public Product Product { get; set; }
    }
}
