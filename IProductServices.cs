using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;
using ProductCategories.WebApiController;

namespace ProductCategories.Services
{
    public interface IProductServices
    {

        //public  Task<ActionResult<IEnumerable<Product>>> GetAllProductAsync();

        //   public Task<ActionResult> AddProductAsync(Product product);

        //    Task<ActionResult> UpdateProductByIdAsync(int id, Product product);
        //Task<ActionResult> DeleteProductAsync(int id);

        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<ActionResult> AddProductAsync(AddProduct category);
        Task<bool> UpdateProductByIdAsync(int id, Product category);
        Task<bool> DeleteProductAsync(int id);

        Task<IActionResult> Edit(EditProduct editCategoryRequest);
        Task<Product> GetByIdAsync(int id);

    }
}
