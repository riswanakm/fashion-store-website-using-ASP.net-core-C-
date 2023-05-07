using System.Linq;
namespace Group7_FinalProject.Models
{
    public class ProductQueryOptions : QueryOptions<Product>
    {
        public void SortFilter(ProductsGridBuilder builder)
        {
            if (builder.IsFilterByCategory)
            {
                Where = b => b.CategoryId == builder.CurrentRoute.CategoryFilter;
            }
            if (builder.IsFilterByPrice)
            {
                if (builder.CurrentRoute.PriceFilter == "under7")
                    Where = b => b.Price < 7;
                else if (builder.CurrentRoute.PriceFilter == "7to14")
                    Where = b => b.Price >= 7 && b.Price <= 14;
                else
                    Where = b => b.Price > 14;
            }
            if (builder.IsFilterByBrand)
            {
                int id = builder.CurrentRoute.BrandFilter.ToInt();
                if (id > 0)
                    Where = b => b.ProductBrands.Any(ba => ba.Brand.BrandId == id);
            }

            if (builder.IsSortByCategory)
            {
                OrderBy = b => b.Category.Name;
            }
            else if (builder.IsSortByPrice)
            {
                OrderBy = b => b.Price;
            }
            else
            {
                OrderBy = b => b.Name;
            }
        }
    }
}
