using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;
using ProductCategories.Services;

namespace ProductCategories.WebApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categorys : ControllerBase
    {
        ICategoryServices _categoryServices;       

        public Categorys(ICategoryServices categoryServices )
        {
            _categoryServices = categoryServices;
            
        }

        [HttpGet("GetAllCategories")]
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryServices.GetAllCategoriesAsync();
        }

        // POST: api/categories
        [HttpPost("AddCategory")]
        public async Task<ActionResult> AddCategory(AddCategory category)
        {
            return await _categoryServices.AddCategoryAsync(category);
        }

        // PUT: api/categories/5
        [HttpPost("UpdateCategoryByIdAsync/{id}")]
        public async Task<bool> UpdateCategoryByIdAsync(int id, EditCategorycs category)
        {
            return await _categoryServices.UpdateCategoryByIdAsync(id, category);
        }

        // DELETE: api/categories/5
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<bool> DeleteCategory(int id)
        {
            return await _categoryServices.DeleteCategoryAsync(id);
        }
    }
}
