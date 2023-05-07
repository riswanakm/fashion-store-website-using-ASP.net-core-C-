using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Group7_FinalProject.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private FashionstoreUnitOfWork data { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(FashionstoreContext ctx, IWebHostEnvironment hostEnvironment) 
        { data = new FashionstoreUnitOfWork(ctx); 
          this._hostEnvironment = hostEnvironment; }

        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = new SearchData(TempData)
                {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Search()
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm)
            {
                var vm = new SearchViewModel
                {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Product>
                {
                    Include = "Category, ProductBrands.Brand"
                };
                if (search.IsProduct)
                {
                    options.Where = b => b.Name.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for product name '{vm.SearchTerm}'";
                }
                if (search.IsBrand)
                {
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1)
                    {
                        options.Where = b => b.ProductBrands.Any(
                            ba => ba.Brand.FirstName.Contains(vm.SearchTerm));
                    }
                    else
                    {
                        string first = vm.SearchTerm.Substring(0, index);
                        string last = vm.SearchTerm.Substring(index + 1);
                        options.Where = b => b.ProductBrands.Any(
                            ba => ba.Brand.FirstName.Contains(first));
                    }
                    vm.Header = $"Search results for brand '{vm.SearchTerm}'";
                }
                if (search.IsCategory)
                {
                    options.Where = b => b.CategoryId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for category ID '{vm.SearchTerm}'";
                }
                vm.Products = data.Products.List(options);
                return View("SearchResults", vm);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        public ViewResult Add(int id) => GetProduct(id, "Add");

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {

                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(vm.Product.ImageFile.FileName);
                string extension = Path.GetExtension(vm.Product.ImageFile.FileName);
                vm.Product.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/images/products/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await vm.Product.ImageFile.CopyToAsync(fileStream);
                }

                data.LoadNewProductBrands(vm.Product, vm.SelectedBrands);
                data.Products.Insert(vm.Product);
                data.Save();

                TempData["message"] = $"{vm.Product.Name} added to Products.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Product", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetProduct(id, "Edit");

        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {


                if (vm.Product.ImageFile != null && vm.Product.ImageFile.Length > 0)
                {
                    //Save image to wwwroot/images/product
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(vm.Product.ImageFile.FileName);
                    string extension = Path.GetExtension(vm.Product.ImageFile.FileName);
                    vm.Product.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/products/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await vm.Product.ImageFile.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    vm.Product.ImageName = vm.Product.ImageName;
                }

                data.DeleteCurrentProductBrands(vm.Product);
                data.LoadNewProductBrands(vm.Product, vm.SelectedBrands);
                data.Products.Update(vm.Product);
                data.Save();

                TempData["message"] = $"{vm.Product.Name} updated.";
                return RedirectToAction("Search");
            }
            else
            {
                Load(vm, "Edit");
                return View("Product", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetProduct(id, "Delete");

        [HttpPost]
        public IActionResult Delete(ProductViewModel vm)
        {
            data.Products.Delete(vm.Product);
            data.Save();
            TempData["message"] = $"{vm.Product.Name} removed from Products.";
            return RedirectToAction("Search");
        }

        private ViewResult GetProduct(int id, string operation)
        {
            var product = new ProductViewModel();
            Load(product, operation, id);
            return View("Product", product);
        }
        private void Load(ProductViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Product = new Product();
            }
            else
            {
                vm.Product = data.Products.Get(new QueryOptions<Product>
                {
                    Include = "ProductBrands.Brand, Category",
                    Where = b => b.ProductId == (id ?? vm.Product.ProductId)
                });
            }

            vm.SelectedBrands = vm.Product.ProductBrands?.Select(
                ba => ba.Brand.BrandId).ToArray();
            vm.Brands = data.Brands.List(new QueryOptions<Brand>
            {
                OrderBy = a => a.FirstName
            });
            vm.Categories = data.Categories.List(new QueryOptions<Category>
            {
                OrderBy = g => g.Name
            });
        }
    }
}
