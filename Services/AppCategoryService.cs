using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskForge.Data.Repositories;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public class AppCategoryService : IAppCategoryService
    {
        private readonly IAppCategoryRepository _appCategoryRepo;
        private readonly ITrackedSessionRepository _sessionRepo;

        public AppCategoryService(
            IAppCategoryRepository appCategoryRepo,
            ITrackedSessionRepository sessionRepo)
        {
            _appCategoryRepo = appCategoryRepo;
            _sessionRepo = sessionRepo;
        }

        public async Task<List<AppCategoryRow>> GetAppCategoryRowsAsync()
        {
            var apps = await _sessionRepo.GetDistinctApplicationNamesAsync();
            var savedMappings = await _appCategoryRepo.GetAllAsync();

            var rows = new List<AppCategoryRow>();
            foreach (var app in apps)
            {
                var savedCategory = savedMappings
                    .FirstOrDefault(x => x.AppName == app)?.Category ?? string.Empty;

                rows.Add(new AppCategoryRow
                {
                    ApplicationName = app,
                    Category = savedCategory
                });
            }

            return rows;
        }

        public async Task SaveMappingsAsync(List<AppCategoryRow> rows)
        {
            if (rows == null) return;

            var entities = rows
                .Where(r => !string.IsNullOrWhiteSpace(r.ApplicationName) && !string.IsNullOrWhiteSpace(r.Category))
                .Select(r => new App_Category
                {
                    AppName = r.ApplicationName,
                    Category = r.Category
                })
                .ToList();

            await _appCategoryRepo.SaveMappingsAsync(entities);
        }
    }
}
