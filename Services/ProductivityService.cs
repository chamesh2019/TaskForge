using TaskForge.Data.Repositories;

namespace TaskForge.Services
{
    public class ProductivityService : IProductivityService
    {
        private readonly IDailyGoalRepository _goalRepo;
        private readonly IAppCategoryRepository _appCategoryRepo;
        private readonly ITrackedSessionRepository _sessionRepo;

        public ProductivityService(
            IDailyGoalRepository goalRepo,
            IAppCategoryRepository appCategoryRepo,
            ITrackedSessionRepository sessionRepo)
        {
            _goalRepo = goalRepo;
            _appCategoryRepo = appCategoryRepo;
            _sessionRepo = sessionRepo;
        }

        public async Task<List<string>> GetExceededGoalMessagesAsync()
        {
            var messages = new List<string>();
            var today = DateTime.Today;

            // Fetch data through repositories asynchronously
            var goals = await _goalRepo.GetAllWithCategoriesAsync();
            var savedMappings = await _appCategoryRepo.GetAllAsync();
            var sessions = await _sessionRepo.GetTodaySessionsAsync();

            foreach (var goal in goals)
            {
                var categoryName = goal.Category?.Name;
                if (string.IsNullOrWhiteSpace(categoryName))
                    continue;

                var mappedApps = savedMappings
                    .Where(a => a.Category == categoryName)
                    .Select(a => a.AppName)
                    .ToList();

                if (mappedApps.Count == 0)
                    continue;

                // Filter sessions by mapped application names
                double usedMinutes = sessions
                    .Where(s => mappedApps.Contains(s.ApplicationName))
                    .Sum(s => s.Duration.TotalMinutes);

                if (usedMinutes > goal.TargetMinutes)
                {
                    messages.Add(
                        $"{categoryName} limit exceeded! Used {usedMinutes:F0} min / Goal {goal.TargetMinutes} min"
                    );
                }
            }

            return messages;
        }
    }
}