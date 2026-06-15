using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskForge.Services
{
    public class AppCategoryRow
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public interface IAppCategoryService
    {
        Task<List<AppCategoryRow>> GetAppCategoryRowsAsync();
        Task SaveMappingsAsync(List<AppCategoryRow> rows);
    }
}
