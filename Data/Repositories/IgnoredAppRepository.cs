using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public class IgnoredAppRepository : IIgnoredAppRepository
    {
        public async Task<List<IgnoredApp>> GetAllAsync()
        {
            using var db = new AppDbContext();
            return await db.IgnoredApps
                .OrderBy(x => x.ApplicationName)
                .ToListAsync();
        }

        public async Task<IgnoredApp?> GetByNameAsync(string appName)
        {
            using var db = new AppDbContext();
            return await db.IgnoredApps
                .FirstOrDefaultAsync(x => x.ApplicationName == appName);
        }

        public async Task AddAsync(IgnoredApp app)
        {
            using var db = new AppDbContext();
            db.IgnoredApps.Add(app);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(IgnoredApp app)
        {
            using var db = new AppDbContext();
            db.IgnoredApps.Remove(app);
            await db.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(string appName)
        {
            using var db = new AppDbContext();
            return await db.IgnoredApps
                .AnyAsync(x => x.ApplicationName == appName);
        }
    }
}
