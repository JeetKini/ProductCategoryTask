namespace ProductCategories.Models.ViewModel
{
    public class DeleteProd
    {
        public int Id { get; set; }
       
        public string Name { get; set; } = String.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
