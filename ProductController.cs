using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models.ViewModel;
using ProductCategories.Models;
using ProductCategories.MVCServices;
using ProductCategories.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductCategories.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices, ApplicationDbContext context)
        {
            _productServices = productServices;

            _context = context;

        }
        // GET: api/category
        [HttpGet]
        public async Task<ActionResult> GetProduct()
        {
            var categories = await _productServices.GetAllProductAsync();

            return View(categories); // Return view named "Categories" with data
        }

        // POST: api/category/add

        [HttpGet]
        public IActionResult AddProd()
        {
            var model = new AddProduct
            {
                Categories = _context.categories
                                    .Where(c => c.IsActive)
                                    .Select(c => new SelectListItem
                                    {
                                        Value = c.Id.ToString(),
                                        Text = c.Name
                                    })
                                     .ToList()
            };

            return View(model);
        }



        [HttpPost]

        public async Task<IActionResult> AddProd(AddProduct model)
        {
           
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    IsActive = model.IsActive,
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    CategoryId = model.CategoryId
                };

                _context.products.Add(product);
                await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

        }
    




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productServices.GetByIdAsync(id);
            if (product != null)
            {
                var editRequest = new EditProduct
                {
                    Quantity = product.Quantity,
                    Price = product.Price,
                    ModifiedDate = DateTime.Now,
                    IsActive = product.IsActive,
                    Name = product.Name,
                };
                return View(editRequest);
            }
            return View(null);


        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProduct editProductRequest)
        {
            // Assuming _categoryServices.UpdateCategoryByIdAsync expects an AddCategory model
            var product = new Product
            {
                Name = editProductRequest.Name,
                Price=editProductRequest.Price,
                Quantity = editProductRequest.Quantity,
                ModifiedDate= DateTime.Now, 
                IsActive = editProductRequest.IsActive,
                // Make sure Id is set appropriately
            };

            var result = await _productServices.UpdateProductByIdAsync(editProductRequest.Id, product);

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
        public async Task<IActionResult> Delete(DeleteProd deleteProd)
        {
            var result = await _productServices.DeleteProductAsync(deleteProd.Id);
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
