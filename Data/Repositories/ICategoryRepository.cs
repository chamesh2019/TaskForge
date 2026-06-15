using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByNameAsync(string name);
        Task AddAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> AnyAsync(string name);
    }
}
