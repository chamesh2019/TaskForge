using TaskForge.Data;
using TaskForge.Data.Repositories;
using TaskForge.Services;
using TaskForge.Views;

namespace TaskForge
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Poor Man's Dependency Injection container setup
            var dbInitializer = new DatabaseInitializer();
            
            var categoryRepo = new CategoryRepository();
            var ignoredAppRepo = new IgnoredAppRepository();
            var goalRepo = new DailyGoalRepository();
            var sessionRepo = new TrackedSessionRepository();
            var appCategoryRepo = new AppCategoryRepository();

            var categoryService = new CategoryService(categoryRepo);
            var ignoredAppService = new IgnoredAppService(ignoredAppRepo);
            var notificationService = new NotificationService();
            var classificationService = new ClassificationService(categoryRepo);
            var appCategoryService = new AppCategoryService(appCategoryRepo, sessionRepo);
            
            var goalService = new GoalService(
                goalRepo, 
                sessionRepo, 
                categoryRepo, 
                notificationService
            );
            
            var trackingService = new TrackingService(
                sessionRepo, 
                ignoredAppRepo, 
                classificationService, 
                goalService
            );

            var productivityService = new ProductivityService(
                goalRepo,
                appCategoryRepo,
                sessionRepo
            );

            var dbBackupService = new DatabaseBackupService();

            var reportService = new ReportService(sessionRepo);

            Application.Run(new MainForm(
                dbInitializer,
                categoryService,
                ignoredAppService,
                goalService,
                trackingService,
                notificationService,
                productivityService,
                appCategoryService,
                dbBackupService
            ));
        }
    }
}