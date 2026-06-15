using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Data.Repositories
{
    public interface ITrackedSessionRepository
    {
        Task AddAsync(TrackedSession session);
        Task<List<TrackedSession>> GetTodaySessionsAsync();
        Task<List<TrackedSession>> GetFilteredSessionsAsync(string selectedApp, DateTime from, DateTime to, string categoryName);
        Task<List<string>> GetDistinctApplicationNamesAsync();
        Task<List<string>> GetDistinctCategoryNamesAsync();
    }
}
