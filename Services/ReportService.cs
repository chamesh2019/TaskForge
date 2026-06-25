using System;
using System.Collections.Generic;
using System.Text;
using TaskForge.Data.Repositories;

namespace TaskForge.Services
{
    public class ReportService : IReportService
    {
        private readonly ITrackedSessionRepository _sessionRepo;

        public ReportService(ITrackedSessionRepository sessionRepo)
        {
            _sessionRepo = sessionRepo;
        }

        public async Task<List<ApplicationReportDto>> GetApplicationSummaryAsync()
        {
            var sessions = await _sessionRepo.GetTodaySessionsAsync();

            return sessions
                .GroupBy(s => s.ApplicationName)
                .Select(g => new ApplicationReportDto
                {
                    ApplicationName = g.Key,
                    TotalMinutes = g.Sum(x => x.Duration.TotalMinutes)
                })
                .OrderByDescending(x => x.TotalMinutes)
                .ToList();
        }

        public async Task<List<CategoryReportDto>> GetCategorySummaryAsync()
        {
            var sessions = await _sessionRepo.GetTodaySessionsAsync();

            return sessions
                .Where(s => s.Category != null)
                .GroupBy(s => s.Category!.Name)
                .Select(g => new CategoryReportDto
                {
                    CategoryName = g.Key,
                    TotalMinutes = g.Sum(x => x.Duration.TotalMinutes)
                })
                .OrderByDescending(x => x.TotalMinutes)
                .ToList();
        }
    }
}