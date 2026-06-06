using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public interface IClassificationService
    {
        Task<int?> ClassifySessionAsync(TrackedSession session);
    }
}
