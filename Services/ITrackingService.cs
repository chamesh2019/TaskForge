using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public interface ITrackingService
    {
        event EventHandler<TrackedSession> ActiveWindowChanged;
        event EventHandler<TrackedSession> SessionEnded;

        void StartTracking();
        void StopTracking();
        TrackedSession? GetCurrentSession();
        Task<Dictionary<string, double>> GetTodayAppTimesAsync();
        Task<List<TrackedSession>> GetFilteredSessionsAsync(string selectedApp, DateTime from, DateTime to, string categoryName);
        Task<List<string>> GetDistinctApplicationNamesAsync();
        Task<List<string>> GetDistinctCategoryNamesAsync();
    }
}
