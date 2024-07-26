using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;
using ProductCategories.MVCServices;
using ProductCategories.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCategories.Controllers
{


    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryServices _categoryServices;
        private readonly IsOptimizedService _isOptimizedService;


        public CategoryController(ICategoryService categoryService, ICategoryServices categoryServices, IsOptimizedService isOptimizedService)
        {
            _categoryService = categoryService;//MVC
            _categoryServices = categoryServices;//WEBAPI
            _isOptimizedService = isOptimizedService;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            bool isOptimized = await _isOptimizedService.IsProductOptimizedAsync();

            if (isOptimized)
            {
                var categories = await _categoryServices.GetAllCategoriesAsync();

                return View(categories);
            }
            else
            {
                var categories = await _categoryService.GetAllCategoriesAsync();

                return View(categories);
            }// Return view named "Categories" with data
        }

        // POST: api/category/add

        [HttpGet]
        public async Task<IActionResult> AddCat()
        {
            return View();
        }


        // [HttpPost]

        //public async Task<IActionResult> AddCat(AddCategory category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _categoryServices.AddCategoryAsync(category);
        //        TempData["SuccessMessage"] = "Category added successfully!";
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return BadRequest(ModelState);
        //}

        [HttpPost]
        public async Task<IActionResult> AddCat(AddCategory category)
        {
            bool isOptimized = await _isOptimizedService.IsProductOptimizedAsync();

            if (ModelState.IsValid)
            {
                if (isOptimized)
                {
                    await _categoryServices.AddCategoryAsync(category);
                }
                else
                {
                    await _categoryService.AddCategoryAsync(category);
                }

                TempData["SuccessMessage"] = "Category added successfully!";
                return RedirectToAction("Index", "Home");
            }

            return BadRequest(ModelState);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryServices.GetByIdAsync(id);
            if (category != null)
            {
                var editRequest = new EditCategorycs
                {
                    Id = id,
                    IsActive = category.IsActive,
                    Name = category.Name,
                };
                return View(editRequest);
            }
            return View(null);


        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditCategorycs editCategoryRequest)
        {
            bool isOptimized = await _isOptimizedService.IsProductOptimizedAsync();

            var category = new EditCategorycs
            {
                Name = editCategoryRequest.Name,
                IsActive = editCategoryRequest.IsActive,
                // Make sure Id is set appropriately
            };

            bool result;

            if (isOptimized)
            {
                result = await _categoryServices.UpdateCategoryByIdAsync(editCategoryRequest.Id, category);
            }
            else
            {
                result = await _categoryService.UpdateCategoryByIdAsync(editCategoryRequest.Id, category);
            }

            if (result)
            {
                TempData["SuccessMessage"] = "Category updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update category.";
                // Handle error scenario if needed
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCat deleteCat)
        {
            bool isOptimized = await _isOptimizedService.IsProductOptimizedAsync();

            bool result;

            if (isOptimized)
            {
                result = await _categoryServices.DeleteCategoryAsync(deleteCat.Id);
            }
            else
            {
                result = await _categoryService.DeleteCategoryAsync(deleteCat.Id);
            }

            if (result)
            {
                TempData["SuccessMessage"] = "Category has been successfully marked as inactive.";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Failed to mark the category as inactive.";
            return RedirectToAction("Edit");
        }




    }
}
