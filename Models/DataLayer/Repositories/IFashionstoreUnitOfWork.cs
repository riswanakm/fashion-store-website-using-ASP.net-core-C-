namespace Group7_FinalProject.Models
{
    public interface IFashionstoreUnitOfWork
    {
        Repository<Product> Products { get; }
        Repository<Brand> Brands { get; }
        Repository<ProductBrand> ProductBrands { get; }
        Repository<Category> Categories { get; }

        void DeleteCurrentProductBrands(Product product);
        void LoadNewProductBrands(Product product, int[] brandids);
        void Save();
    }
}
