using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;

namespace ProductCategories.Services
{
    public class ProductServices : IProductServices
    {
        
        private readonly ApplicationDbContext _context;
        public ProductServices( ApplicationDbContext context)
        {
          
            _context = context;
        }

        public async Task<ActionResult> AddProductAsync(AddProduct product)
        {
            Product prod = new Product();

            prod.Name = product.Name;
            prod.Price = product.Price;
            prod.Quantity = product.Quantity;           
            prod.CreatedDate = DateTime.Now;
            prod.ModifiedDate = DateTime.Now;
            prod.IsActive = true;
            prod.CategoryId= product.CategoryId;

            try
            {
                // Add the job to the database
                await _context.AddAsync(prod);
                await _context.SaveChangesAsync();

                // Return success response
                return new OkObjectResult(new { message = "Product added successfully" });
            }
            catch (Exception ex)
            {
                // Return error response
                return new ObjectResult(new { message = "An error occurred while adding the Product", error = ex.Message })
                {
                    StatusCode = 500
                };
            }



        }
       
        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            try
            {
                return await _context.products
    .Include(p => p.Category)
    .Where(p => p.IsActive && p.Category.IsActive)
    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching active Product", ex);
            }
        }

        

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.products.FindAsync(id);
        }

        public async Task<bool> UpdateProductAsync(int id, Product Updateproduct)
        {
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {


                product.Name = Updateproduct.Name;
                product.IsActive = Updateproduct.IsActive;
                product.Price= Updateproduct.Price;
                product.ModifiedDate = DateTime.Now;
                product.Quantity = Updateproduct.Quantity;
                //category.Products = Updatecategory.Products;

                await _context.SaveChangesAsync();
                return true;



            }
            return false;
        }
        public async Task<bool> UpdateProductByIdAsync(int id, EditProduct product)
        {
            try
            {
                var cat = await _context.products.FindAsync(id);
                if (cat == null)
                {
                    // Log or handle the "not found" case if necessary
                    return false;
                }

                cat.Name = product.Name;
                cat.Price= product.Price;
                cat.Quantity= product.Quantity;
                cat.ModifiedDate = DateTime.Now;
                cat.IsActive = product.IsActive;

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


       

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.products.FindAsync(id);
                if (product == null)
                {
                    return false; // Category not found
                }

                product.IsActive = false; // Set IsActive to 0 (inactive)
                _context.products.Update(product);
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

        Task<IActionResult> IProductServices.Edit(EditProduct editCategoryRequest)
        {
            throw new NotImplementedException();
        }
    }
}
