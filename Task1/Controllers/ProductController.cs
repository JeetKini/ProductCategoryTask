using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models.ViewModel;
using ProductCategories.Models;
using ProductCategories.MVCServices;
using ProductCategories.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategories.WebApiController;

namespace ProductCategories.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IProductServices _productServices;
        private readonly IsOptimizedService _isOptimizedService;
        private readonly IProductService _productService;

        public ProductController(IProductServices productServices, ApplicationDbContext context,IsOptimizedService isOptimizedService,IProductService productService  )
        {
            _productServices = productServices;

            _context = context;
            _isOptimizedService = isOptimizedService;
          _productService = productService;

        }
        // GET: api/category
        [HttpGet]
        public async Task<ActionResult> GetProduct()

        {
            
            bool isOptimized = await _isOptimizedService.IsProductOptimizedAsync();

            if (isOptimized)
            {

                var categories = await _productServices.GetAllProductAsync();
                return View(categories);

            }
            else
            {
                var categories = await _productService.GetAllProductAsync();
                return View(categories);
            }

           ; // Return view named "Categories" with data
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
            var product = new AddProduct
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                IsActive = model.IsActive,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CategoryId = model.CategoryId
            };

            bool isOptimized = await _isOptimizedService.IsProductOptimizedAsync();

            if (isOptimized)
            {
                await _productServices.AddProductAsync(product);
            }
            else
            {
                await _productService.AddProductAsync(product);
            }

            TempData["SuccessMessage"] = "Product added successfully!";
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
            var product = new EditProduct
            {
                Name = editProductRequest.Name,
                Price = editProductRequest.Price,
                Quantity = editProductRequest.Quantity,
                ModifiedDate = DateTime.Now,
                IsActive = editProductRequest.IsActive,
                  // Make sure Id is set appropriately
            };

            bool isOptimized = await _isOptimizedService.IsProductOptimizedAsync();

            bool result;

            if (isOptimized)
            {
                result = await _productServices.UpdateProductByIdAsync(editProductRequest.Id, product);
            }
            else
            {
                result = await _productService.UpdateProductByIdAsync(editProductRequest.Id, product);
            }

            if (result)
            {
                TempData["SuccessMessage"] = "Product updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update product.";
                // Handle error scenario if needed
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool isOptimized = await _isOptimizedService.IsProductOptimizedAsync();

            bool result;

            if (isOptimized)
            {
                result = await _productServices.DeleteProductAsync(id);
            }
            else
            {
                result = await _productService.DeleteProductAsync(id);
            }

            if (result)
            {
                TempData["SuccessMessage"] = "Product has been successfully deleted.";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Failed to delete the product.";
            return RedirectToAction("Edit");
        }

    }
}
