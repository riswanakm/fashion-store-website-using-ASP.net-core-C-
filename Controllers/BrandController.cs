using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace Group7_FinalProject.Controllers
{
    public class BrandController : Controller
    {
        private Repository<Brand> data { get; set; }
        public BrandController(FashionstoreContext ctx) => data = new Repository<Brand>(ctx);

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {
            string defaultSort = nameof(Brand.FirstName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Brand>
            {
                Include = "ProductBrands.Product",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.FirstName;
           

            var vm = new BrandListViewModel
            {
                Brands = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var brand = data.Get(new QueryOptions<Brand>
            {
                Include = "ProductBrands.Product",
                Where = a => a.BrandId == id
            });
            return View(brand);
        }
    }
}
