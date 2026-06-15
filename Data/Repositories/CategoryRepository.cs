using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<List<Category>> GetAllAsync()
        {
            using var db = new AppDbContext();
            return await db.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            using var db = new AppDbContext();
            return await db.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task AddAsync(Category category)
        {
            using var db = new AppDbContext();
            db.Categories.Add(category);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            using var db = new AppDbContext();
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(string name)
        {
            using var db = new AppDbContext();
            return await db.Categories
                .AnyAsync(c => c.Name == name);
        }
    }
}
