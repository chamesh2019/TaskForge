using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskForge.Data.Repositories;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly WindowTracker _tracker;
        private readonly ITrackedSessionRepository _sessionRepo;
        private readonly IIgnoredAppRepository _ignoredAppRepo;
        private readonly IClassificationService _classificationService;
        private readonly IGoalService _goalService;

        public event EventHandler<TrackedSession>? ActiveWindowChanged;
        public event EventHandler<TrackedSession>? SessionEnded;

        public TrackingService(
            ITrackedSessionRepository sessionRepo,
            IIgnoredAppRepository ignoredAppRepo,
            IClassificationService classificationService,
            IGoalService goalService)
        {
            _sessionRepo = sessionRepo;
            _ignoredAppRepo = ignoredAppRepo;
            _classificationService = classificationService;
            _goalService = goalService;

            _tracker = new WindowTracker();
            _tracker.ActiveWindowChanged += Tracker_ActiveWindowChanged;
            _tracker.SessionEnded += Tracker_SessionEnded;
        }

        public void StartTracking()
        {
            _tracker.Start();
        }

        public void StopTracking()
        {
            _tracker.Stop();
        }

        public TrackedSession? GetCurrentSession()
        {
            return _tracker.GetCurrentSession();
        }

        public async Task<Dictionary<string, double>> GetTodayAppTimesAsync()
        {
            var sessions = await _sessionRepo.GetTodaySessionsAsync();
            return sessions
                .GroupBy(s => s.ApplicationName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(s => s.Duration.TotalMinutes)
                );
        }

        public Task<List<TrackedSession>> GetFilteredSessionsAsync(string selectedApp, DateTime from, DateTime to, string categoryName)
        {
            return _sessionRepo.GetFilteredSessionsAsync(selectedApp, from, to, categoryName);
        }

        public Task<List<string>> GetDistinctApplicationNamesAsync()
        {
            return _sessionRepo.GetDistinctApplicationNamesAsync();
        }

        public Task<List<string>> GetDistinctCategoryNamesAsync()
        {
            return _sessionRepo.GetDistinctCategoryNamesAsync();
        }

        private void Tracker_ActiveWindowChanged(object? sender, TrackedSession e)
        {
            Task.Run(async () =>
            {
                try
                {
                    e.CategoryId = await _classificationService.ClassifySessionAsync(e);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to classify active session: {ex.Message}");
                }
                ActiveWindowChanged?.Invoke(this, e);
            });
        }

        private void Tracker_SessionEnded(object? sender, TrackedSession e)
        {
            // Process the session completion asynchronously in a background task
            Task.Run(async () =>
            {
                try
                {
                    bool isIgnored = await _ignoredAppRepo.AnyAsync(e.ApplicationName);
                    if (isIgnored)
                    {
                        System.Diagnostics.Debug.WriteLine($"Ignored app skipped: {e.ApplicationName}");
                        return;
                    }

                    // Classify the session
                    e.CategoryId = await _classificationService.ClassifySessionAsync(e);

                    // Save session to database
                    await _sessionRepo.AddAsync(e);
                    System.Diagnostics.Debug.WriteLine("Session saved to database.");

                    // Check if goals were met
                    await _goalService.CheckGoalsAsync();

                    // Notify UI layer
                    SessionEnded?.Invoke(this, e);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to process ended session: {ex.Message}");
                }
            });
        }
    }
}
