using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Http;

namespace Group7_FinalProject.Models
{
    public class ProductsGridBuilder : GridBuilder
    {
        public ProductsGridBuilder(ISession sess) : base(sess) { }

        public ProductsGridBuilder(ISession sess, ProductsGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Category.IndexOf(FilterPrefix.Category) == -1;
            routes.BrandFilter = (isInitial) ? FilterPrefix.Brand + values.Brand : values.Brand;
            routes.CategoryFilter = (isInitial) ? FilterPrefix.Category + values.Category : values.Category;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
        }

        public void LoadFilterSegments(string[] filter, Brand brand)
        {
            if (brand == null)
            {
                routes.BrandFilter = FilterPrefix.Brand + filter[0];
            }
            else
            {
                routes.BrandFilter = FilterPrefix.Brand + filter[0]
                    + "-" + brand.FullName.Slug();
            }
            routes.CategoryFilter = FilterPrefix.Category + filter[1];
            routes.PriceFilter = FilterPrefix.Price + filter[2];
        }

        public void ClearFilterSegments() => routes.ClearFilters();

        string def = ProductsGridDTO.DefaultFilter;
        public bool IsFilterByBrand => routes.BrandFilter != def;
        public bool IsFilterByCategory => routes.CategoryFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        public bool IsSortByCategory =>
            routes.SortField.EqualsNoCase(nameof(Category));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Product.Price));

    }
}
