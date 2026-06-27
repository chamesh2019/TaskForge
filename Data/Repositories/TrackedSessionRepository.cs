using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public class TrackedSessionRepository : ITrackedSessionRepository
    {
        public async Task AddAsync(TrackedSession session)
        {
            using var db = new AppDbContext();
            if (!session.CategoryId.HasValue)
            {
                var neutral = await db.Categories.FirstOrDefaultAsync(c => c.Name == "Neutral");
                session.CategoryId = neutral?.Id ?? 1;
            }
            db.TrackedSessions.Add(session);
            await db.SaveChangesAsync();
        }

        public async Task<List<TrackedSession>> GetTodaySessionsAsync()
        {
            using var db = new AppDbContext();
            DateTime today = DateTime.Today;
            return await db.TrackedSessions
                .Include(s => s.Category)
                .Where(s => s.StartTime >= today)
                .ToListAsync();
        }

        public async Task<List<TrackedSession>> GetFilteredSessionsAsync(string selectedApp, DateTime from, DateTime to, string categoryName)
        {
            using var db = new AppDbContext();
            var query = db.TrackedSessions
                .Include(s => s.Category)
                .AsQueryable();

            if (selectedApp != "All")
            {
                query = query.Where(s => s.ApplicationName == selectedApp);
            }

            DateTime toEnd = to.Date.AddDays(1);
            query = query.Where(s => s.StartTime >= from.Date && s.StartTime < toEnd);

            if (!string.IsNullOrEmpty(categoryName) && categoryName != "All")
            {
                query = query.Where(s => s.Category != null && s.Category.Name == categoryName);
            }

            return await query
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<string>> GetDistinctApplicationNamesAsync()
        {
            using var db = new AppDbContext();
            return await db.TrackedSessions
                .Select(s => s.ApplicationName)
                .Distinct()
                .OrderBy(name => name)
                .ToListAsync();
        }

        public async Task<List<string>> GetDistinctCategoryNamesAsync()
        {
            using var db = new AppDbContext();
            return await db.TrackedSessions
                .Include(s => s.Category)
                .Where(s => s.Category != null)
                .Select(s => s.Category!.Name)
                .Distinct()
                .OrderBy(name => name)
                .ToListAsync();
        }
    }
}
