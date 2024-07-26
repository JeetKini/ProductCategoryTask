using ProductCategories.Models;
using ProductCategories.Models.ViewModel;

namespace ProductCategories.MVCServices
{
    public class ProductService:IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7295/");
        }
        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Products/GetAllProducts");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
                    return products.ToList();
                }
                else
                {
                    throw new Exception("An error occurred while fetching products");
                }
            }
            catch (HttpRequestException)
            {
                throw new Exception("An error occurred while fetching products");
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching products");
            }
        }

        public async Task AddProductAsync(AddProduct product)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/api/Products/AddProduct", product);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("An error occurred while adding the product");
                }
            }
            catch (HttpRequestException)
            {
                throw new Exception("An error occurred while adding the product");
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while adding the product");
            }
        }

        public async Task<bool> UpdateProductByIdAsync(int id, EditProduct product)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/api/Products/UpdateProductById/{id}", product);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("An error occurred while updating the product");
                }
            }
            catch (HttpRequestException)
            {
                throw new Exception("An error occurred while updating the product");
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while updating the product");
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"/api/Products/GetAllCategories{id}");
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Products/DeleteProduct/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("An error occurred while deleting the product");
                }
            }
            catch (HttpRequestException)
            {
                throw new Exception("An error occurred while deleting the product");
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while deleting the product");
            }
        }



    }
}
