using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskForge.Services
{
    public interface IProductivityService
    {
        Task<List<string>> GetExceededGoalMessagesAsync();
    }
}
