using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductCategories.Models.ViewModel
{
    public class AddProduct
    {
        public string Name { get; set; }
       
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
