using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<bool> AddCategoryAsync(string name);
        Task<bool> DeleteCategoryAsync(string name);
    }
}
