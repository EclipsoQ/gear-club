using GearClub.Domain.Models;
using GearClub.Presentation.CompositeModels;

namespace GearClub.Domain.ServiceInterfaces
{
    public interface ICategoryService
    {
        void CreateCategory(CategoryViewModel model);
        void DeleteCategory(Category category);
        void UpdateCategory(CategoryViewModel model);
        List<Category> GetAllCategories();
        Category? GetCategoryById(int id);
        void AddProductToCategory(int productId, int categoryId);

    }
}
