using System.ComponentModel.DataAnnotations;

namespace ProductCategories.Models
{
    public class IsOptimized
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsApiOptimized { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set;}

    }
}
