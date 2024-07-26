using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductCategories.Models;
using ProductCategories.Models.ViewModel;
using System.Net;

namespace ProductCategories.MVCServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7295/");
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Categorys/GetAllCategories");

                if (response.IsSuccessStatusCode)
                {
                    var categories = await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();
                    return categories.ToList(); // Return categories as ActionResult
                }
                else
                {
                    throw new Exception("An error occurred while fetching categories"); // Return status code of unsuccessful response
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while fetching categories"); // Internal Server Error
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while fetching categories");  // Internal Server Error
            }
        }



        public async Task<Category> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Category>($"/api/Categorys/GetAllCategories{id}");
        }

        public async Task AddCategoryAsync(AddCategory category)
        {
            await _httpClient.PostAsJsonAsync($"/api/Categorys/AddCategory", category);
        }

        public async Task<bool> UpdateCategoryByIdAsync(int id, EditCategorycs category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/api/Categorys/UpdateCategoryByIdAsync/{id}", category);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Indicate success
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception("An error occurred while fetching categories"); // Internal Server Error // Indicate error with status code and message
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while fetching categories"); // Internal Server Error
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while fetching categories"); // Internal Server Error
            }
        }




        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Categorys/DeleteCategory/{id}");

            // Check if the response status code indicates success
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                // Optionally, log the error or handle it as needed
                // var errorContent = await response.Content.ReadAsStringAsync();
                // _logger.LogError($"Failed to delete category {id}. Error: {errorContent}");

                return false;
            }
        }

    }
}
