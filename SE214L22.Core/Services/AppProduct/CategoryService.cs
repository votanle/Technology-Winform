using SE214L22.Core.ViewModels.Settings.Dtos;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.Services.AppProduct
{
    public class CategoryService : BaseService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }
        public IEnumerable<CategoryForDisplayDto> GetDisplayCategories()
        {
            var listCategory = _categoryRepository.GetCategories();
            var listCategoryForReturn = Mapper.Map<List<CategoryForDisplayDto>>(listCategory);
            return listCategoryForReturn;
        }

        public Category AddCategory(CategoryForCreationDto category)
        {
            var newCategory = Mapper.Map<Category>(category);

            return _categoryRepository.Create(newCategory);
        }

        public bool DeleteCategory(CategoryForDisplayDto category)
        {
            var deleteCategory = Mapper.Map<Category>(category);
            return _categoryRepository.Delete(deleteCategory.Id);
        }
        public bool UpdateCategory(CategoryForDisplayDto category)
        {
            var editCategory = Mapper.Map<Category>(category);
            return _categoryRepository.Update(editCategory);
        }
    }
}
