using System;
using System.Collections.Generic;
using System.Linq;
using TaskForge.Data;

namespace TaskForge.Services
{
    public class ProductivityService
    {
        public List<string> GetExceededGoalMessages()
        {
            using var db = new AppDbContext();

            var messages = new List<string>();
            var today = DateTime.Today;

            var goals = db.DailyGoals.ToList();

            foreach (var goal in goals)
            {
                var categoryName = db.Categories
                    .Where(c => c.Id == goal.CategoryId)
                    .Select(c => c.Name)
                    .FirstOrDefault();

                if (string.IsNullOrWhiteSpace(categoryName))
                    continue;

                var mappedApps = db.AppCategories
                    .Where(a => a.Category == categoryName)
                    .Select(a => a.AppName)
                    .ToList();

                if (mappedApps.Count == 0)
                    continue;

                var sessions = db.TrackedSessions
                    .Where(s => s.StartTime.Date == today &&
                                mappedApps.Contains(s.ApplicationName))
                    .ToList();

                double usedMinutes = sessions.Sum(s => s.Duration.TotalMinutes);

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