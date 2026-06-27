using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Services
{
    public interface IReportService
    {
        Task<List<ApplicationReportDto>> GetApplicationSummaryAsync();

        Task<List<CategoryReportDto>> GetCategorySummaryAsync();

        Task<List<WeeklyReportDto>> GetWeeklySummaryAsync();
    }
}
