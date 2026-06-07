using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskForge.Data;
using TaskForge.Services;
using TaskForge.Tracking;

namespace TaskForge.Views
{
    public partial class MainForm : Form
    {
        private readonly IDatabaseInitializer _dbInitializer;
        private readonly ICategoryService _categoryService;
        private readonly IIgnoredAppService _ignoredAppService;
        private readonly IGoalService _goalService;
        private readonly ITrackingService _trackingService;
        private readonly INotificationService _notificationService;
        private readonly IProductivityService _productivityService;
        private readonly IAppCategoryService _appCategoryService;

        public MainForm(
            IDatabaseInitializer dbInitializer,
            ICategoryService categoryService,
            IIgnoredAppService ignoredAppService,
            IGoalService goalService,
            ITrackingService trackingService,
            INotificationService notificationService,
            IProductivityService productivityService,
            IAppCategoryService appCategoryService)
        {
            InitializeComponent();

            _dbInitializer = dbInitializer;
            _categoryService = categoryService;
            _ignoredAppService = ignoredAppService;
            _goalService = goalService;
            _trackingService = trackingService;
            _notificationService = notificationService;
            _productivityService = productivityService;
            _appCategoryService = appCategoryService;

            // Initialize database schema
            _dbInitializer.Initialize();

            // Register for goals met notifications
            _notificationService.NotificationTriggered += NotificationService_NotificationTriggered;

            // Initialize tracker and start tracking background events
            _trackingService.ActiveWindowChanged += Tracker_ActiveWindowChanged;
            _trackingService.SessionEnded += Tracker_SessionEnded;
            _trackingService.StartTracking();

            LoadChartData();
            LoadApplicationFilter();

            btnAddCategory.Click += btnAddCategory_Click;
            btnDeleteCategory.Click += btnDeleteCategory_Click;

            btnAddIgnore.Click += btnAddIgnore_Click;
            btnDeleteIgnore.Click += btnDeleteIgnore_Click;

            btnSaveGoal.Click += btnSaveGoal_Click;

            timerRefresh.Start();
        }

        private void NotificationService_NotificationTriggered(object? sender, string message)
        {
            // Goals checks run in a background thread, so marshal back to UI thread
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => 
                    MessageBox.Show(this, message, "Daily Goal Achieved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ));
            }
            else
            {
                MessageBox.Show(this, message, "Daily Goal Achieved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = true;
            panelHistory.Visible = false;
            panelSettings.Visible = false;
        }

        private async void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = false;
            panelHistory.Visible = true;
            panelSettings.Visible = false;

            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value);
        }

        private async void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = false;
            panelHistory.Visible = false;
            panelSettings.Visible = true;

            await LoadCategoriesAsync();
            await LoadIgnoredAppsAsync();
            await LoadGoalsAsync();
        }

        private async void LoadChartData()
        {
            chartDashboard.Series[0].Points.Clear();
            chartDashboard.Series[0].IsValueShownAsLabel = true;
            chartDashboard.Series[0]["PieLabelStyle"] = "Outside";
            chartDashboard.Series[0]["PieLineColor"] = "Black";
            chartDashboard.Series[0]["PieStartAngle"] = "270";

            var appTimes = await _trackingService.GetTodayAppTimesAsync();

            double total = 0;

            foreach (var app in appTimes)
            {
                double value = Math.Round(app.Value, 2);
                chartDashboard.Series[0].Points.AddXY(app.Key, value);
                total += value;
            }

            lblTotalTime.Text = $"Total Time: {Math.Round(total, 2)} mins";
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            LoadChartData();
        }

        private void Tracker_ActiveWindowChanged(object? sender, TrackedSession e)
        {
            Debug.WriteLine($"[Active Window Changed] App: {e.ApplicationName}, Title: {e.WindowTitle}");
        }

        private void Tracker_SessionEnded(object? sender, TrackedSession e)
        {
            // Invoke chart refresh on the UI thread as the end session is tracked in a background task
            if (InvokeRequired)
            {
                BeginInvoke(new Action(LoadChartData));
            }
            else
            {
                LoadChartData();
            }
        }

        private async Task LoadHistoryDataAsync(string selectedApp, DateTime from, DateTime to, string categoryName = "All")
        {
            var sessions = await _trackingService.GetFilteredSessionsAsync(selectedApp, from, to, categoryName);

            var displayData = sessions.Select(s => new
            {
                s.Id,
                s.ApplicationName,
                s.WindowTitle,
                StartTime = s.StartTime.ToString("g"),
                EndTime = s.EndTime?.ToString("g") ?? "_",
                Duration = Math.Round(s.Duration.TotalMinutes, 2) + " mins"
            }).ToList();

            dataGridHistory.DataSource = displayData;
        }

        private async void LoadApplicationFilter()
        {
            cmbApplication.Items.Clear();
            cmbApplication.Items.Add("All");

            var apps = await _trackingService.GetDistinctApplicationNamesAsync();
            foreach (var app in apps)
            {
                cmbApplication.Items.Add(app);
            }

            cmbApplication.SelectedIndex = 0;

            dtFrom.Value = DateTime.Today;
            dtTo.Value = DateTime.Today;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _trackingService.StopTracking();
            base.OnFormClosing(e);
        }

        private async void cmbApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value, cmbCategory.Text);
        }

        private async void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value, cmbCategory.Text);
        }

        private async void dtTo_ValueChanged(object sender, EventArgs e)
        {
            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value, cmbCategory.Text);
        }

        private async void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value, cmbCategory.Text);
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            lstCategories.Items.Clear();
            foreach (var c in categories)
            {
                lstCategories.Items.Add(c.Name);
            }

            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All");

            foreach (var c in categories)
            {
                cmbCategory.Items.Add(c.Name);
            }

            cmbCategory.SelectedIndex = 0;

            // Bind categories to Goal combobox
            cmbGoalCategory.DataSource = categories;
            cmbGoalCategory.DisplayMember = "Name";
            cmbGoalCategory.ValueMember = "Id";

            if (categories.Count > 0)
                cmbGoalCategory.SelectedIndex = 0;
        }

        private async void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategory.Text))
                return;

            bool success = await _categoryService.AddCategoryAsync(txtCategory.Text);
            if (!success)
            {
                MessageBox.Show("Category already exists or could not be added!");
                return;
            }

            txtCategory.Clear();
            await LoadCategoriesAsync();
        }

        private async void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItem == null)
                return;

            string selected = lstCategories.SelectedItem.ToString()!;
            await _categoryService.DeleteCategoryAsync(selected);
            await LoadCategoriesAsync();
        }

        private async Task LoadIgnoredAppsAsync()
        {
            var apps = await _ignoredAppService.GetIgnoredAppsAsync();

            lstIgnoredApps.Items.Clear();
            foreach (var app in apps)
            {
                lstIgnoredApps.Items.Add(app.ApplicationName);
            }
        }

        private async void btnAddIgnore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIgnoreApp.Text))
                return;

            bool success = await _ignoredAppService.AddIgnoredAppAsync(txtIgnoreApp.Text);
            if (!success)
            {
                MessageBox.Show("App already in ignore list or could not be added!");
                return;
            }

            txtIgnoreApp.Clear();
            await LoadIgnoredAppsAsync();
        }

        private async void btnDeleteIgnore_Click(object sender, EventArgs e)
        {
            if (lstIgnoredApps.SelectedItem == null)
                return;

            string selected = lstIgnoredApps.SelectedItem.ToString()!;
            await _ignoredAppService.DeleteIgnoredAppAsync(selected);
            await LoadIgnoredAppsAsync();
        }

        private async Task LoadGoalsAsync()
        {
            var goals = await _goalService.GetGoalsAsync();

            lstGoals.Items.Clear();
            foreach (var g in goals)
            {
                string categoryName = g.Category?.Name ?? "Unknown";
                lstGoals.Items.Add($"{categoryName} - {g.TargetMinutes} minutes");
            }
        }

        private async void btnSaveGoal_Click(object sender, EventArgs e)
        {
            if (cmbGoalCategory.SelectedItem == null)
            {
                MessageBox.Show(this, "Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numGoalMinutes.Value <= 0)
            {
                MessageBox.Show(this, "Please enter minutes greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedCategory = (Category)cmbGoalCategory.SelectedItem;
            int minutes = (int)numGoalMinutes.Value;

            try
            {
                await _goalService.SaveGoalAsync(selectedCategory.Name, minutes);
                MessageBox.Show(this, "Goal saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadGoalsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error saving goal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCheckGoals_Click_1(object sender, EventArgs e)
        {
            try
            {
                var messages = await _productivityService.GetExceededGoalMessagesAsync();
                if (messages.Count == 0)
                {
                    MessageBox.Show(this, "No goals exceeded today.", "Productivity Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, string.Join(Environment.NewLine, messages), "Productivity Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error checking goals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var form = new AppCatergory(_appCategoryService, _categoryService);
            form.ShowDialog(this);
        }
    }
}
