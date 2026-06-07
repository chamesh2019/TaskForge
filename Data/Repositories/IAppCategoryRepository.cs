using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public interface IAppCategoryRepository
    {
        Task<List<App_Category>> GetAllAsync();
        Task SaveMappingsAsync(List<App_Category> mappings);
        Task<List<string>> GetAppNamesByCategoryNameAsync(string categoryName);
    }
}
