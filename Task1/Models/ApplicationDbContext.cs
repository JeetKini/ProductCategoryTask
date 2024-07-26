using Microsoft.EntityFrameworkCore;

namespace ProductCategories.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<Category> categories { get; set; }
        public virtual DbSet<IsOptimized> isOptimized { get; set; }
    }
}
