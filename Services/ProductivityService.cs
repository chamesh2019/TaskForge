using System;
using System.Collections.Generic;
using System.Linq;
using TaskForge.Data;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    internal class ProductivityService
    {
        public double GetTodayMinutesByCategory(int categoryId)
        {
            using (var db = new AppDbContext())
            {
                DateTime today = DateTime.Today;

                var sessions = db.TrackedSessions
                    .Where(s =>
                        s.CategoryId == categoryId &&
                        s.StartTime.Date == today)
                    .ToList();

                double totalMinutes = 0;

                foreach (var session in sessions)
                {
                    totalMinutes += session.Duration.TotalMinutes;
                }

                return totalMinutes;
            }
        }

        public DailyGoal? GetDailyGoal(int categoryId)
        {
            using (var db = new AppDbContext())
            {
                return db.DailyGoals.FirstOrDefault(g => g.CategoryId == categoryId);
            }
        }

        public bool IsGoalExceeded(int categoryId)
        {
            var goal = GetDailyGoal(categoryId);

            if (goal == null)
                return false;

            double usedMinutes = GetTodayMinutesByCategory(categoryId);

            return usedMinutes > goal.TargetMinutes;
        }

        public List<string> GetExceededGoalMessages()
        {
            using (var db = new AppDbContext())
            {
                var messages = new List<string>();

                var goals = db.DailyGoals.ToList();

                foreach (var goal in goals)
                {
                    double usedMinutes = GetTodayMinutesByCategory(goal.CategoryId);

                    if (usedMinutes > goal.TargetMinutes)
                    {
                        var categoryName = db.Categories
                            .Where(c => c.Id == goal.CategoryId)
                            .Select(c => c.Name)
                            .FirstOrDefault() ?? "Unknown";

                        messages.Add(
                            $"{categoryName} limit exceeded! Used {usedMinutes:F0} min / Goal {goal.TargetMinutes} min"
                        );
                    }
                }

                return messages;
            }
        }
    }
}
