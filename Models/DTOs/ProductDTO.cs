using System.Collections.Generic;

namespace Group7_FinalProject.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Dictionary<int, string> Brands { get; set; }

        public void Load(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Price = product.Price;
            Brands = new Dictionary<int, string>();
            foreach (ProductBrand ba in product.ProductBrands)
            {
                Brands.Add(ba.Brand.BrandId, ba.Brand.FullName);
            }
        }
    }
}
