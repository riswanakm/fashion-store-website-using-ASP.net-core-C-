using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Group7_FinalProject.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    [Area("Admin")]
    public class BrandController : Controller
    {
        private Repository<Brand> data { get; set; }
        public BrandController(FashionstoreContext ctx) => data = new Repository<Brand>(ctx);

        public ViewResult Index()
        {
            var brands = data.List(new QueryOptions<Brand>
            {
                OrderBy = a => a.FirstName
            });
            return View(brands);
        }

        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "view products":
                    return RedirectToAction("ViewProducts", new { id });
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Add() => View("Brand", new Brand());

        [HttpPost]
        public IActionResult Add(Brand brand, string operation)
        {
            var validate = new Validate(TempData);
            if (!validate.IsBrandChecked)
            {
                validate.CheckBrand(brand.FirstName, operation, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(brand.FirstName), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(brand);
                data.Save();
                validate.ClearBrand();
                TempData["message"] = $"{brand.FullName} added to Authors.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Brand", brand);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => View("Brand", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                data.Update(brand);
                data.Save();
                TempData["message"] = $"{brand.FullName} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Brand", brand);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var brand = data.Get(new QueryOptions<Brand>
            {
                Include = "ProductBrands",
                Where = a => a.BrandId == id
            });

            if (brand.ProductBrands.Count > 0)
            {
                TempData["message"] = $"Can't delete Brand {brand.FullName} because they are associated with these products.";
                return GoToBrandSearch(brand);
            }
            else
            {
                return View("Brand", brand);
            }
        }

        [HttpPost]
        public RedirectToActionResult Delete(Brand brand)
        {
            data.Delete(brand);
            data.Save();
            TempData["message"] = $"{brand.FullName} removed from Brands.";
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ViewProducts(int id)
        {
            var Brand = data.Get(id);
            return GoToBrandSearch(Brand);
        }

        private RedirectToActionResult GoToBrandSearch(Brand brand)
        {
            var search = new SearchData(TempData)
            {
                SearchTerm = brand.FullName,
                Type = "Brand"
            };
            return RedirectToAction("Search", "Product");
        }
    }
}
