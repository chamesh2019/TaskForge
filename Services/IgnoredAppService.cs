using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Data.Repositories;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public class IgnoredAppService : IIgnoredAppService
    {
        private readonly IIgnoredAppRepository _ignoredAppRepo;

        public IgnoredAppService(IIgnoredAppRepository ignoredAppRepo)
        {
            _ignoredAppRepo = ignoredAppRepo;
        }

        public Task<List<IgnoredApp>> GetIgnoredAppsAsync()
        {
            return _ignoredAppRepo.GetAllAsync();
        }

        public async Task<bool> AddIgnoredAppAsync(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
                return false;

            string cleanName = appName.Trim();
            if (await _ignoredAppRepo.AnyAsync(cleanName))
                return false;

            var app = new IgnoredApp { ApplicationName = cleanName };
            await _ignoredAppRepo.AddAsync(app);
            return true;
        }

        public async Task DeleteIgnoredAppAsync(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
                return;

            var app = await _ignoredAppRepo.GetByNameAsync(appName);
            if (app != null)
            {
                await _ignoredAppRepo.DeleteAsync(app);
            }
        }

        public Task<bool> IsIgnoredAsync(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
                return Task.FromResult(false);

            return _ignoredAppRepo.AnyAsync(appName.Trim());
        }
    }
}
