using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public class AppCategoryRepository : IAppCategoryRepository
    {
        public async Task<List<App_Category>> GetAllAsync()
        {
            using var db = new AppDbContext();
            return await db.AppCategories.ToListAsync();
        }

        public async Task SaveMappingsAsync(List<App_Category> mappings)
        {
            using var db = new AppDbContext();
            
            // Replicate original bulk-replace logic
            db.AppCategories.RemoveRange(db.AppCategories);
            await db.SaveChangesAsync();

            if (mappings != null && mappings.Count > 0)
            {
                db.AppCategories.AddRange(mappings);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetAppNamesByCategoryNameAsync(string categoryName)
        {
            using var db = new AppDbContext();
            return await db.AppCategories
                .Where(x => x.Category == categoryName)
                .Select(x => x.AppName)
                .ToListAsync();
        }
    }
}
