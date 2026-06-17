using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskForge.Data;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public class DatabaseBackupService : IDatabaseBackupService
    {
        private class BackupDto
        {
            public List<CategoryDto> Categories { get; set; } = new();
            public List<IgnoredAppDto> IgnoredApps { get; set; } = new();
            public List<DailyGoalDto> DailyGoals { get; set; } = new();
            public List<AppCategoryDto> AppCategories { get; set; } = new();
            public List<TrackedSessionDto> TrackedSessions { get; set; } = new();
        }

        private class CategoryDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private class IgnoredAppDto
        {
            public int Id { get; set; }
            public string ApplicationName { get; set; } = string.Empty;
        }

        private class DailyGoalDto
        {
            public int Id { get; set; }
            public int CategoryId { get; set; }
            public int TargetMinutes { get; set; }
        }

        private class AppCategoryDto
        {
            public int Id { get; set; }
            public string Category { get; set; } = string.Empty;
            public string AppName { get; set; } = string.Empty;
        }

        private class TrackedSessionDto
        {
            public int Id { get; set; }
            public string ApplicationName { get; set; } = string.Empty;
            public string WindowTitle { get; set; } = string.Empty;
            public int? CategoryId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public async Task ExportToJsonAsync(string filePath)
        {
            using var db = new AppDbContext();
            
            var backup = new BackupDto
            {
                Categories = await db.Categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }).ToListAsync(),
                IgnoredApps = await db.IgnoredApps.Select(i => new IgnoredAppDto { Id = i.Id, ApplicationName = i.ApplicationName }).ToListAsync(),
                DailyGoals = await db.DailyGoals.Select(g => new DailyGoalDto { Id = g.Id, CategoryId = g.CategoryId, TargetMinutes = g.TargetMinutes }).ToListAsync(),
                AppCategories = await db.AppCategories.Select(ac => new AppCategoryDto { Id = ac.Id, Category = ac.Category, AppName = ac.AppName }).ToListAsync(),
                TrackedSessions = await db.TrackedSessions.Select(s => new TrackedSessionDto { Id = s.Id, ApplicationName = s.ApplicationName, WindowTitle = s.WindowTitle, CategoryId = s.CategoryId, StartTime = s.StartTime, EndTime = s.EndTime }).ToListAsync()
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(backup, options);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task ImportFromJsonAsync(string filePath)
        {
            string json = await File.ReadAllTextAsync(filePath);
            var backup = JsonSerializer.Deserialize<BackupDto>(json);
            if (backup == null) return;

            using var db = new AppDbContext();
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                // Disable foreign key constraints temporarily in SQLite
                await db.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = OFF;");

                // Clear all tables
                await db.Database.ExecuteSqlRawAsync("DELETE FROM DailyGoals;");
                await db.Database.ExecuteSqlRawAsync("DELETE FROM TrackedSessions;");
                await db.Database.ExecuteSqlRawAsync("DELETE FROM Categories;");
                await db.Database.ExecuteSqlRawAsync("DELETE FROM IgnoredApps;");
                await db.Database.ExecuteSqlRawAsync("DELETE FROM AppCategories;");

                // Seed/Insert data preserving original IDs
                foreach (var c in backup.Categories)
                {
                    db.Categories.Add(new Category { Id = c.Id, Name = c.Name });
                }
                foreach (var i in backup.IgnoredApps)
                {
                    db.IgnoredApps.Add(new IgnoredApp { Id = i.Id, ApplicationName = i.ApplicationName });
                }
                foreach (var g in backup.DailyGoals)
                {
                    db.DailyGoals.Add(new DailyGoal { Id = g.Id, CategoryId = g.CategoryId, TargetMinutes = g.TargetMinutes });
                }
                foreach (var ac in backup.AppCategories)
                {
                    db.AppCategories.Add(new App_Category { Id = ac.Id, Category = ac.Category, AppName = ac.AppName });
                }
                foreach (var s in backup.TrackedSessions)
                {
                    db.TrackedSessions.Add(new TrackedSession { Id = s.Id, ApplicationName = s.ApplicationName, WindowTitle = s.WindowTitle, CategoryId = s.CategoryId, StartTime = s.StartTime, EndTime = s.EndTime });
                }

                await db.SaveChangesAsync();

                // Re-enable foreign key constraints
                await db.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = ON;");

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
