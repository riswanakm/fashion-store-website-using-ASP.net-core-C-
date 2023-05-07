using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Group7_FinalProject.Models
{
    public class Validate
    {
        private const string CategoryKey = "validCategory";
        private const string BrandKey = "validBrand";
        private const string EmailKey = "validEmail";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckCategory(string categoryId, Repository<Category> data)
        {
            Category entity = data.Get(categoryId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Category id {categoryId} is already in the database.";
        }
        public void MarkCategoryChecked() => tempData[CategoryKey] = true;
        public void ClearCategory() => tempData.Remove(CategoryKey);
        public bool IsCategoryChecked => tempData.Keys.Contains(CategoryKey);

        public void CheckBrand(string firstName, string operation, Repository<Brand> data)
        {
            Brand entity = null;
            if (Operation.IsAdd(operation))
            {
                entity = data.Get(new QueryOptions<Brand>
                {
                    Where = a => a.FirstName == firstName 
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Brand {entity.FullName} is already in the database.";
        }
        public void MarkBrandChecked() => tempData[BrandKey] = true;
        public void ClearBrand() => tempData.Remove(BrandKey);
        public bool IsBrandChecked => tempData.Keys.Contains(BrandKey);
    }
}
