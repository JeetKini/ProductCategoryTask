using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;

namespace ProductCategories.MVCServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddCategoryAsync(AddCategory category);
        Task<bool> UpdateCategoryByIdAsync(int id,EditCategorycs category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
