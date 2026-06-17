using System.Threading.Tasks;

namespace TaskForge.Services
{
    public interface IDatabaseBackupService
    {
        Task ExportToJsonAsync(string filePath);
        Task ImportFromJsonAsync(string filePath);
    }
}
