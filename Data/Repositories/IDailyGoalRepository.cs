using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public interface IDailyGoalRepository
    {
        Task<List<DailyGoal>> GetAllWithCategoriesAsync();
        Task<DailyGoal?> GetByCategoryIdAsync(int categoryId);
        Task AddAsync(DailyGoal goal);
        Task UpdateAsync(DailyGoal goal);
        Task DeleteAsync(DailyGoal goal);
    }
}
