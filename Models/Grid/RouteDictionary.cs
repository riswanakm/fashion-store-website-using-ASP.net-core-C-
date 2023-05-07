using Group7_FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace Group7_FinalProject.Models
{
    public static class FilterPrefix
    {
        public const string Category = "category-";
        public const string Price = "price-";
        public const string Brand = "brand-";
    }

    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            this[nameof(GridDTO.SortField)] = fieldName;

            if (current.SortField.EqualsNoCase(fieldName) &&
                current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }

        public string CategoryFilter
        {
            get => Get(nameof(ProductsGridDTO.Category))?.Replace(FilterPrefix.Category, "");
            set => this[nameof(ProductsGridDTO.Category)] = value;
        }

        public string PriceFilter
        {
            get => Get(nameof(ProductsGridDTO.Price))?.Replace(FilterPrefix.Price, "");
            set => this[nameof(ProductsGridDTO.Price)] = value;
        }

        public string BrandFilter
        {
            get
            {
                string s = Get(nameof(ProductsGridDTO.Brand))?.Replace(FilterPrefix.Brand, "");
                int index = s?.IndexOf('-') ?? -1;
                return (index == -1) ? s : s.Substring(0, index);
            }
            set => this[nameof(ProductsGridDTO.Brand)] = value;
        }

        public void ClearFilters() =>
            CategoryFilter = PriceFilter = BrandFilter = ProductsGridDTO.DefaultFilter;

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }
    }
}
