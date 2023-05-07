using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace Group7_FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private FashionstoreUnitOfWork data { get; set; }
        public ProductController(FashionstoreContext ctx) => data = new FashionstoreUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(ProductsGridDTO values)
        {
            var builder = new ProductsGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Product.Name));

            var options = new ProductQueryOptions
            {
                Include = "ProductBrands.Brand, Category",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new ProductListViewModel
            {
                Products = data.Products.List(options),
                Brands = data.Brands.List(new QueryOptions<Brand>
                {
                    OrderBy = a => a.FirstName
                }),
                Categories = data.Categories.List(new QueryOptions<Category>
                {
                    OrderBy = g => g.Name
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Products.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var product = data.Products.Get(new QueryOptions<Product>
            {
                Include = "ProductBrands.Brand, Category",
                Where = b => b.ProductId == id
            });
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new ProductsGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var brand = data.Brands.Get(filter[0].ToInt());
                builder.LoadFilterSegments(filter, brand);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }
}
