using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskForge.Data.Repositories;
using TaskForge.Tracking;

namespace TaskForge.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IAppCategoryRepository _appCategoryRepo;

        public ClassificationService(ICategoryRepository categoryRepo, IAppCategoryRepository appCategoryRepo)
        {
            _categoryRepo = categoryRepo;
            _appCategoryRepo = appCategoryRepo;
        }

        public async Task<int?> ClassifySessionAsync(TrackedSession session)
        {
            if (session == null) return null;

            var categories = await _categoryRepo.GetAllAsync();
            if (categories.Count == 0) return null;

            string app = session.ApplicationName.ToLowerInvariant();
            string title = session.WindowTitle.ToLowerInvariant();

            // 1. Check user-defined manual application-to-category mappings first
            var mappings = await _appCategoryRepo.GetAllAsync();
            var matchedMapping = mappings.FirstOrDefault(m => m.AppName.Equals(session.ApplicationName, StringComparison.OrdinalIgnoreCase));
            if (matchedMapping != null)
            {
                var category = categories.FirstOrDefault(c => c.Name.Equals(matchedMapping.Category, StringComparison.OrdinalIgnoreCase));
                if (category != null)
                {
                    return category.Id;
                }
            }

            // 2. Exact or partial match of application name / window title with category names
            foreach (var category in categories)
            {
                string catName = category.Name.ToLowerInvariant();
                if (app == catName || title.Contains(catName))
                {
                    return category.Id;
                }
            }

            // 3. Built-in heuristics for common category terms
            // Let's find matches based on common keywords
            var categoryMap = new Dictionary<string, Func<string, string, bool>>
            {
                ["development"] = (a, t) => a.Contains("devenv") || a.Contains("vscode") || a.Contains("rider") || a.Contains("git") || a.Contains("powershell") || a.Contains("cmd"),
                ["work"] = (a, t) => a.Contains("devenv") || a.Contains("vscode") || a.Contains("excel") || a.Contains("winword") || a.Contains("powerpnt") || a.Contains("teams") || a.Contains("slack") || a.Contains("zoom") || a.Contains("outlook"),
                ["browsing"] = (a, t) => a.Contains("chrome") || a.Contains("firefox") || a.Contains("edge") || a.Contains("safari") || a.Contains("opera"),
                ["entertainment"] = (a, t) => t.Contains("youtube") || t.Contains("netflix") || t.Contains("twitch") || t.Contains("spotify") || a.Contains("vlc") || a.Contains("spotify"),
                ["social media"] = (a, t) => t.Contains("facebook") || t.Contains("twitter") || t.Contains("reddit") || t.Contains("instagram") || t.Contains("linkedin"),
                ["gaming"] = (a, t) => a.Contains("steam") || a.Contains("epicgames") || a.Contains("minecraft") || a.Contains("solitaire")
            };

            foreach (var mapping in categoryMap)
            {
                // Find a category whose name matches or contains the mapped category keyword
                var targetCategory = categories.FirstOrDefault(c => c.Name.ToLowerInvariant().Contains(mapping.Key));
                if (targetCategory != null && mapping.Value(app, title))
                {
                    return targetCategory.Id;
                }
            }

            // 4. Fallback: try mapping category names as substrings of application name
            foreach (var category in categories)
            {
                string catName = category.Name.ToLowerInvariant();
                if (app.Contains(catName))
                {
                    return category.Id;
                }
            }

            return null; // Unclassified
        }
    }
}
