using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;

namespace ProductCategories.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<ActionResult> AddCategoryAsync(AddCategory category);
        Task<bool> UpdateCategoryByIdAsync(int id, Category category);
        Task<bool> DeleteCategoryAsync(int id);

        Task<IActionResult> Edit(EditCategorycs editCategoryRequest);
        Task<Category> GetByIdAsync(int id);
    }
}
