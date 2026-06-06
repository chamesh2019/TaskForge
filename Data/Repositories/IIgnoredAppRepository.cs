using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public interface IIgnoredAppRepository
    {
        Task<List<IgnoredApp>> GetAllAsync();
        Task<IgnoredApp?> GetByNameAsync(string appName);
        Task AddAsync(IgnoredApp app);
        Task DeleteAsync(IgnoredApp app);
        Task<bool> AnyAsync(string appName);
    }
}
