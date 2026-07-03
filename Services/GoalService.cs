using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskForge.Data.Repositories;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public class GoalService : IGoalService
    {
        private readonly IDailyGoalRepository _goalRepo;
        private readonly ITrackedSessionRepository _sessionRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly INotificationService _notificationService;

        // Keep track of goals notified today in memory
        private readonly HashSet<int> _notifiedGoalsToday = new HashSet<int>();
        private DateTime _lastNotificationDate = DateTime.Today;

        public GoalService(
            IDailyGoalRepository goalRepo,
            ITrackedSessionRepository sessionRepo,
            ICategoryRepository categoryRepo,
            INotificationService notificationService)
        {
            _goalRepo = goalRepo;
            _sessionRepo = sessionRepo;
            _categoryRepo = categoryRepo;
            _notificationService = notificationService;
        }

        public Task<List<DailyGoal>> GetGoalsAsync()
        {
            return _goalRepo.GetAllWithCategoriesAsync();
        }

        public async Task SaveGoalAsync(string categoryName, int targetMinutes)
        {
            if (string.IsNullOrWhiteSpace(categoryName)) return;

            var category = await _categoryRepo.GetByNameAsync(categoryName);
            if (category == null) return;

            var existing = await _goalRepo.GetByCategoryIdAsync(category.Id);
            if (existing != null)
            {
                existing.TargetMinutes = targetMinutes;
                await _goalRepo.UpdateAsync(existing);
            }
            else
            {
                var newGoal = new DailyGoal
                {
                    CategoryId = category.Id,
                    TargetMinutes = targetMinutes
                };
                await _goalRepo.AddAsync(newGoal);
            }

            // After saving/updating a goal, re-evaluate to see if it's already met
            await CheckGoalsAsync();
        }

        public async Task CheckGoalsAsync(TrackedSession? activeSession = null)
        {
            // Reset the notified set if the day has changed
            if (DateTime.Today != _lastNotificationDate)
            {
                _notifiedGoalsToday.Clear();
                _lastNotificationDate = DateTime.Today;
            }

            var goals = await _goalRepo.GetAllWithCategoriesAsync();
            if (goals.Count == 0) return;

            var sessions = await _sessionRepo.GetTodaySessionsAsync();

            // Group sessions by CategoryId and calculate total minutes
            var categoryMinutes = sessions
                .Where(s => s.CategoryId.HasValue)
                .GroupBy(s => s.CategoryId!.Value)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(s => s.Duration.TotalMinutes)
                );

            if (activeSession != null && activeSession.CategoryId.HasValue)
            {
                int catId = activeSession.CategoryId.Value;
                double activeMinutes = activeSession.Duration.TotalMinutes;
                if (categoryMinutes.ContainsKey(catId))
                {
                    categoryMinutes[catId] += activeMinutes;
                }
                else
                {
                    categoryMinutes[catId] = activeMinutes;
                }
            }

            foreach (var goal in goals)
            {
                // Skip if already notified today
                if (_notifiedGoalsToday.Contains(goal.Id)) continue;

                if (categoryMinutes.TryGetValue(goal.CategoryId, out double totalMinutes))
                {
                    if (totalMinutes >= goal.TargetMinutes)
                    {
                        _notifiedGoalsToday.Add(goal.Id);
                        _notificationService.TriggerNotification(
                            $"Daily goal of {goal.TargetMinutes} minutes met for category '{goal.Category?.Name}'! Total spent: {Math.Round(totalMinutes, 1)} mins."
                        );
                    }
                }
            }
        }

        public async Task DeleteGoalAsync(int goalId)
        {
            var goals = await _goalRepo.GetAllWithCategoriesAsync();
            var goal = goals.FirstOrDefault(g => g.Id == goalId);
            if (goal != null)
            {
                await _goalRepo.DeleteAsync(goal);
            }
        }
    }
}
