using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Data.Repositories;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return _categoryRepo.GetAllAsync();
        }

        public async Task<bool> AddCategoryAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string cleanName = name.Trim();
            if (await _categoryRepo.AnyAsync(cleanName))
                return false;

            var category = new Category { Name = cleanName };
            await _categoryRepo.AddAsync(category);
            return true;
        }

        public async Task DeleteCategoryAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            var category = await _categoryRepo.GetByNameAsync(name);
            if (category != null)
            {
                await _categoryRepo.DeleteAsync(category);
            }
        }
    }
}
