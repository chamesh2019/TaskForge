using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public class DailyGoalRepository : IDailyGoalRepository
    {
        public async Task<List<DailyGoal>> GetAllWithCategoriesAsync()
        {
            using var db = new AppDbContext();
            return await db.DailyGoals
                .Include(g => g.Category)
                .ToListAsync();
        }

        public async Task<DailyGoal?> GetByCategoryIdAsync(int categoryId)
        {
            using var db = new AppDbContext();
            return await db.DailyGoals
                .FirstOrDefaultAsync(g => g.CategoryId == categoryId);
        }

        public async Task AddAsync(DailyGoal goal)
        {
            using var db = new AppDbContext();
            db.DailyGoals.Add(goal);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(DailyGoal goal)
        {
            using var db = new AppDbContext();
            db.DailyGoals.Update(goal);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(DailyGoal goal)
        {
            using var db = new AppDbContext();
            db.DailyGoals.Remove(goal);
            await db.SaveChangesAsync();
        }
    }
}
