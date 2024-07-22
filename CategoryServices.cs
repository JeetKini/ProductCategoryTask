using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;

namespace ProductCategories.Services
{
    public class CategoryServices:ICategoryServices
    {
        private readonly ApplicationDbContext _context;
     
        public CategoryServices(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<ActionResult> AddCategoryAsync(AddCategory category)
        {
            Category cat = new Category
            {
                Name = category.Name,               
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true
            };

            try
            {
                await _context.AddAsync(cat);
                await _context.SaveChangesAsync();

                return new OkObjectResult(new { message = "Category added successfully" });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = "An error occurred while adding the category", error = ex.Message })
                {
                    StatusCode = 500
                };
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.categories.Where(c => c.IsActive==true ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching active categories", ex);
            }
        }

        //public async Task<ActionResult> DeleteCategoryAsync(int id)
        //{
        //    try
        //    {
        //        var category = await _context.categories.FindAsync(id);
        //        if (category == null)
        //        {
        //            return new NotFoundObjectResult(new { message = "Category not found" });
        //        }

        //        _context.categories.Remove(category);
        //        await _context.SaveChangesAsync();

        //        return new OkObjectResult(new { message = "Category deleted successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ObjectResult(new { message = "An error occurred while deleting the category", error = ex.Message })
        //        {
        //            StatusCode = 500
        //        };
        //    }
        //}

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.categories.FindAsync(id);
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category Updatecategory)
        {
            var category = await _context.categories.FindAsync(id);
            if (category != null)
            {


                category.Name = Updatecategory.Name;
                category.IsActive = Updatecategory.IsActive;
                //category.Products = Updatecategory.Products;

                await _context.SaveChangesAsync();
                return true;



            }
            return false;
        }
        public async Task<bool> UpdateCategoryByIdAsync(int id, Category category)
        {
            try
            {
                var cat = await _context.categories.FindAsync(id);
                if (cat == null)
                {
                    // Log or handle the "not found" case if necessary
                    return false;
                }

                cat.Name = category.Name;
                cat.ModifiedDate = DateTime.Now;
                cat.IsActive = category.IsActive;

                _context.Entry(cat).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                // Optionally, log ex.Message for debugging purposes
                return false;
            }
        }


        Task<IActionResult> ICategoryServices.Edit(EditCategorycs editCategoryRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.categories.FindAsync(id);
                if (category == null)
                {
                    return false; // Category not found
                }

                category.IsActive=false; // Set IsActive to 0 (inactive)
                _context.categories.Update(category);
                await _context.SaveChangesAsync();

                return true; // Successfully made the category inactive
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                // Optionally, log ex.Message for debugging purposes
                return false;
            }
        }
    }
}
