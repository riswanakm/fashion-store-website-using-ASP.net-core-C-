using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace Group7_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private Repository<Brand> brandData { get; set; }
        private Repository<Category> categoryData { get; set; }

        public ValidationController(FashionstoreContext ctx)
        {
            brandData = new Repository<Brand>(ctx);
            categoryData = new Repository<Category>(ctx);
        }

        public JsonResult CheckGenre(string categoryId)
        {
            var validate = new Validate(TempData);
            validate.CheckCategory(categoryId, categoryData);
            if (validate.IsValid)
            {
                validate.MarkCategoryChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckBrand(string firstName, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckBrand(firstName, operation, brandData);
            if (validate.IsValid)
            {
                validate.MarkBrandChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }
    }
}
