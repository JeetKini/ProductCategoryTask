using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;
using ProductCategories.Services;

namespace ProductCategories.WebApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        IProductServices _productServices;

        public Products(IProductServices productServices)
        {
            _productServices = productServices;

        }

        [HttpGet("GetAllProducts")]
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _productServices.GetAllProductAsync();
        }

        // POST: api/product
        [HttpPost("Add_Product")]
        public async Task<ActionResult> AddProduct(AddProduct product)
        {


            return await _productServices.AddProductAsync(product);
        }

        // PUT: api/product/5
        [HttpPut("UpdateProductById")]
        public async Task<bool> UpdateProductByIdAsync(int id, EditProduct product)
        {
            return await _productServices.UpdateProductByIdAsync(id, product);

        }

        // DELETE: api/product/5
        [HttpDelete("DeleteProduct")]
        public async Task<bool> DeleteProduct(int id)
        {
            return await _productServices.DeleteProductAsync(id);
        }

       
    }

}

