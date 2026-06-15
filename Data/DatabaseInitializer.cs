namespace TaskForge.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        public void Initialize()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
        }
    }
}
