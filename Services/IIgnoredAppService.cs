using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public interface IIgnoredAppService
    {
        Task<List<IgnoredApp>> GetIgnoredAppsAsync();
        Task<bool> AddIgnoredAppAsync(string appName);
        Task DeleteIgnoredAppAsync(string appName);
        Task<bool> IsIgnoredAsync(string appName);
    }
}
