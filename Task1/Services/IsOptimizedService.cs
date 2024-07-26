using Microsoft.EntityFrameworkCore;
using ProductCategories.Models;

namespace ProductCategories.Services
{
    public class IsOptimizedService
    {
        private readonly ApplicationDbContext _context;

        public IsOptimizedService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<bool> IsProductOptimizedAsync()
        {
            var result = await _context.isOptimized                
                .Select(p=>p.IsApiOptimized)
                .FirstOrDefaultAsync();

            return result;
        }

    }
}
