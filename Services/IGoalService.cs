using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public interface IGoalService
    {
        Task<List<DailyGoal>> GetGoalsAsync();
        Task SaveGoalAsync(string categoryName, int targetMinutes);
        Task CheckGoalsAsync();
    }
}
