using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskForge.Data;
using TaskForge.Data.Repositories;
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
        private readonly IDatabaseBackupService _dbBackupService;

        private NotifyIcon? _notifyIcon;
        private bool _allowClose = false;

        public MainForm(
            IDatabaseInitializer dbInitializer,
            ICategoryService categoryService,
            IIgnoredAppService ignoredAppService,
            IGoalService goalService,
            ITrackingService trackingService,
            INotificationService notificationService,
            IProductivityService productivityService,
            IAppCategoryService appCategoryService,
            IDatabaseBackupService dbBackupService)
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
            _dbBackupService = dbBackupService;

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

            btnExport.Click += btnExport_Click;
            btnImport.Click += btnImport_Click;

            timerRefresh.Start();

            InitializeSystemTray();
        }

        private void NotificationService_NotificationTriggered(object? sender, string message)
        {
            // Goals checks run in a background thread, so marshal back to UI thread
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => ShowNotification(message)));
            }
            else
            {
                ShowNotification(message);
            }
        }

        private void ShowNotification(string message)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.ShowBalloonTip(5000, "Daily Goal Achieved", message, ToolTipIcon.Info);
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
                Category = s.Category?.Name ?? "Neutral",
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

            // Load categories for the history filter
            await LoadCategoriesAsync();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_allowClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                if (_notifyIcon != null)
                {
                    _notifyIcon.ShowBalloonTip(3000, "TaskForge", "Application minimized to system tray. Tracking is active in the background.", ToolTipIcon.Info);
                }
                return;
            }

            _trackingService.StopTracking();

            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
            }

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

            // Get only categories that have tracked sessions
            var validCategoryNames = await _trackingService.GetDistinctCategoryNamesAsync();

            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All");

            foreach (var categoryName in validCategoryNames)
            {
                cmbCategory.Items.Add(categoryName);
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
            bool deleted = await _categoryService.DeleteCategoryAsync(selected);
            if (!deleted)
            {
                MessageBox.Show(this, "The default 'Neutral' category cannot be deleted.", "Delete Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            using var form = new AppCategory(_appCategoryService, _categoryService);
            form.ShowDialog(this);
        }

        private void OpenReportsForm()
        {
            var reportService = new ReportService(
                new TrackedSessionRepository());

            using var form = new ReportsForm(reportService);

            form.ShowDialog(this);
        }

        private async void btnExport_Click(object? sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                Title = "Export Database Backup",
                DefaultExt = "json",
                FileName = $"TaskForge_Backup_{DateTime.Now:yyyyMMdd_HHmmss}"
            };

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    await _dbBackupService.ExportToJsonAsync(filePath);
                    MessageBox.Show(this, "Database exported successfully!", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Error exporting database: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnImport_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(this,
                "Importing a database backup will overwrite all existing data. Are you sure you want to proceed?",
                "Confirm Import",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            using var openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                Title = "Import Database Backup"
            };

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Temporarily stop tracking before import to avoid database lock and inconsistent tracking state
                    _trackingService.StopTracking();

                    await _dbBackupService.ImportFromJsonAsync(filePath);

                    // Reload/Refresh UI components
                    LoadChartData();
                    LoadApplicationFilter();
                    await LoadCategoriesAsync();
                    await LoadIgnoredAppsAsync();
                    await LoadGoalsAsync();

                    MessageBox.Show(this, "Database imported successfully!", "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Error importing database: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Restart tracking after import is complete/failed
                    _trackingService.StartTracking();
                }
            }
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)

        {

            var reportService = new ReportService(new TrackedSessionRepository());

            using var form = new ReportsForm(reportService);

            form.ShowDialog(this);

        }

        private void InitializeSystemTray()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = this.Icon ?? SystemIcons.Application,
                Text = "TaskForge Tracker",
                Visible = true
            };

            var contextMenu = new ContextMenuStrip();
            
            var showItem = new ToolStripMenuItem("Show Dashboard", null, (s, e) => {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            });

            var hideItem = new ToolStripMenuItem("Hide to Tray", null, (s, e) => {
                this.Hide();
            });

            var exitItem = new ToolStripMenuItem("Exit Application", null, (s, e) => {
                _allowClose = true;
                Application.Exit();
            });

            contextMenu.Items.Add(showItem);
            contextMenu.Items.Add(hideItem);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(exitItem);

            _notifyIcon.ContextMenuStrip = contextMenu;

            _notifyIcon.DoubleClick += (s, e) => {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            };
        }
    }
}
