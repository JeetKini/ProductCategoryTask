using ProductCategories.Models.ViewModel;
using ProductCategories.Models;

namespace ProductCategories.MVCServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddProductAsync(AddProduct category);
        Task<bool> UpdateProductByIdAsync(int id, EditProduct category);
        Task<bool> DeleteProductAsync(int id);
    }
}
