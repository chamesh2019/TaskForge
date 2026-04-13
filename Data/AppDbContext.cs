using Microsoft.EntityFrameworkCore;
using TaskForge.Tracking;

namespace TaskForge.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TrackedSession> TrackedSessions { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<IgnoredApp> IgnoredApps { get; set; }

        public DbSet<DailyGoal> DailyGoals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TaskForge.db");
        }
    }
}
