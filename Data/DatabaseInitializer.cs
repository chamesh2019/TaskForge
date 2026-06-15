using TaskForge.Tracking;

namespace TaskForge.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        public void Initialize()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            // Seed "Neutral" category if it doesn't exist
            if (!db.Categories.Any(c => c.Name == "Neutral"))
            {
                db.Categories.Add(new Category { Name = "Neutral" });
                db.SaveChanges();
            }
        }
    }
}
