namespace ProductCategories.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }=String.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; }


    }
}
