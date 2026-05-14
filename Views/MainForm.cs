using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using TaskForge.Data;
using TaskForge.Tracking;
using System.Linq;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using TaskForge.Services;

namespace TaskForge.Views
{
    public partial class MainForm : Form
    {
        private WindowTracker _tracker;

        public MainForm()
        {
            InitializeComponent();

            // database is created
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }

            _tracker = new WindowTracker();

            LoadCategoriesIntoGoalComboBox();
            LoadSavedGoals();

            // initialize tracker and start
            _tracker = new WindowTracker();
            _tracker.ActiveWindowChanged += Tracker_ActiveWindowChanged;
            _tracker.SessionEnded += Tracker_SessionEnded;
            _tracker.Start();

            LoadChartData();

            LoadApplicationFilter();

            btnAddCategory.Click += btnAddCategory_Click;
            btnDeleteCategory.Click += btnDeleteCategory_Click;

            btnAddIgnore.Click += btnAddIgnore_Click;
            btnDeleteIgnore.Click += btnDeleteIgnore_Click;

            btnSaveGoal.Click += btnSaveGoal_Click;


            timerRefresh.Start();
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

            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value); // refresh history data when opened
        }

        private async void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = false;
            panelHistory.Visible = false;
            panelSettings.Visible = true;

            await LoadCategoriesAsync();
            await LoadIgnoredAppsAsync();
            await LoadGoalsAsync();

            LoadCategoriesIntoGoalComboBox();
        }

        private async Task<Dictionary<string, double>> GetTodayAppTimesAsync()
        {
            using var db = new AppDbContext();

            // get start of today
            DateTime today = DateTime.Today;

            // get all tracked sessions for today
            var sessions = await db.TrackedSessions
                .Where(s => s.StartTime >= today)
                .ToListAsync();


            // group by application and sum durations in minutes
            var appTimes = sessions
                .GroupBy(s => s.ApplicationName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(s => s.Duration.TotalMinutes)
                );
            return appTimes;
        }


        private async void LoadChartData()
        {
            chartDashboard.Series[0].Points.Clear();
            chartDashboard.Series[0].IsValueShownAsLabel = true;
            chartDashboard.Series[0]["PieLabelStyle"] = "Outside";
            chartDashboard.Series[0]["PieLineColor"] = "Black";
            chartDashboard.Series[0]["PieStartAngle"] = "270";

            var appTimes = await GetTodayAppTimesAsync();

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
            LoadChartData(); // refresh chart
        }


        private void Tracker_ActiveWindowChanged(object? sender, TrackedSession e)
        {
            // show in debug output
            Debug.WriteLine($"[Active Window Changed] App: {e.ApplicationName}, Title: {e.WindowTitle}");
        }

        private void Tracker_SessionEnded(object? sender, TrackedSession e)
        {

            // save to database
            Task.Run(() =>
            {
                try
                {
                    using var db = new AppDbContext();

                    bool isIgnored = db.IgnoredApps
                        .Any(x => x.ApplicationName == e.ApplicationName);

                    if (isIgnored)
                    {
                        Debug.WriteLine($"Ignored app skipped: {e.ApplicationName}");
                        return; // not save
                    }

                    db.TrackedSessions.Add(e);
                    db.SaveChanges();
                    Debug.WriteLine("Session saved to database.");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to save session: {ex.Message}");
                }
            });
        }


        private async Task LoadHistoryDataAsync(string selectedApp, DateTime from, DateTime to, string category = "All")
        {
            using var db = new AppDbContext();

            var query = db.TrackedSessions.AsQueryable();

            // application filter
            if (selectedApp != "All")
            {
                query = query.Where(s => s.ApplicationName == selectedApp);
            }

            // date range filter
            DateTime toEnd = to.Date.AddDays(1);

            query = query.Where(s => s.StartTime >= from.Date && s.StartTime < toEnd);

            //if (category != "All")
            //    query = query.Where(s => s.Category == category);

            // get all tracked sessions
            var sessions = await query
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();

            var displayData = sessions.Select(s => new
            {
                s.Id,
                s.ApplicationName,
                s.WindowTitle,
                StartTime = s.StartTime.ToString("g"),// general datetime format
                EndTime = s.EndTime?.ToString("g") ?? "_",
                Duration = Math.Round(s.Duration.TotalMinutes, 2) + " mins"
            }).ToList();

            dataGridHistory.DataSource = displayData;
        }

        private void LoadApplicationFilter()
        {
            cmbApplication.Items.Clear();
            cmbApplication.Items.Add("All");

            using (var conn = new SqliteConnection("Data Source=taskforge.db"))
            {
                conn.Open();

                var cmd = new SqliteCommand("SELECT DISTINCT ApplicationName FROM TrackedSessions", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmbApplication.Items.Add(reader["ApplicationName"].ToString());
                }
            }

            cmbApplication.SelectedIndex = 0;

            dtFrom.Value = DateTime.Today;
            dtTo.Value = DateTime.Today;
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _tracker.Stop();
            base.OnFormClosing(e);
        }

        private async void cmbApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value);
        }

        private async void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value);
        }

        private async void dtTo_ValueChanged(object sender, EventArgs e)
        {
            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value);
        }

        private async void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value, cmbCategory.Text);
        }

        private async Task LoadCategoriesAsync()
        {
            using var db = new AppDbContext();

            var categories = await db.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            lstCategories.Items.Clear();

            foreach (var c in categories)
            {
                lstCategories.Items.Add(c.Name);
            }

            // also refresh filter dropdown
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All");

            foreach (var c in categories)
            {
                cmbCategory.Items.Add(c.Name);
            }

            cmbCategory.SelectedIndex = 0;

            //// fill goal category dropdown
            //cmbGoalCategory.Items.Clear();

            //foreach (var c in categories)
            //{
            //    cmbGoalCategory.Items.Add(c.Name);
            //}

            //if (cmbGoalCategory.Items.Count > 0)
            //    cmbGoalCategory.SelectedIndex = 0;
        }

        private async void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategory.Text))
                return;

            using var db = new AppDbContext();

            var exists = await db.Categories
                .AnyAsync(c => c.Name == txtCategory.Text);

            if (exists)
            {
                MessageBox.Show("Category already exists!");
                return;
            }

            db.Categories.Add(new Category
            {
                Name = txtCategory.Text.Trim()
            });

            await db.SaveChangesAsync();

            txtCategory.Clear();

            await LoadCategoriesAsync();
        }

        private async void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItem == null)
                return;

            string selected = lstCategories.SelectedItem.ToString();

            using var db = new AppDbContext();

            var category = await db.Categories
                .FirstOrDefaultAsync(c => c.Name == selected);

            if (category == null)
                return;

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            await LoadCategoriesAsync();
        }


        // for ignore setting

        private async Task LoadIgnoredAppsAsync()
        {
            using var db = new AppDbContext();

            var apps = await db.IgnoredApps
                .OrderBy(x => x.ApplicationName)
                .ToListAsync();

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

            using var db = new AppDbContext();

            bool exists = await db.IgnoredApps
                .AnyAsync(x => x.ApplicationName == txtIgnoreApp.Text);

            if (exists)
            {
                MessageBox.Show("App already in ignore list!");
                return;
            }

            db.IgnoredApps.Add(new IgnoredApp
            {
                ApplicationName = txtIgnoreApp.Text.Trim()
            });

            await db.SaveChangesAsync();

            txtIgnoreApp.Clear();

            await LoadIgnoredAppsAsync();
        }


        private async void btnDeleteIgnore_Click(object sender, EventArgs e)
        {
            if (lstIgnoredApps.SelectedItem == null)
                return;

            string selected = lstIgnoredApps.SelectedItem.ToString();

            using var db = new AppDbContext();

            var app = await db.IgnoredApps
                .FirstOrDefaultAsync(x => x.ApplicationName == selected);

            if (app == null)
                return;

            db.IgnoredApps.Remove(app);
            await db.SaveChangesAsync();

            await LoadIgnoredAppsAsync();
        }

        // save goal

        private async Task LoadGoalsAsync()
        {
            using var db = new AppDbContext();

            var goals = await db.DailyGoals
                .Include(g => g.Category)
                .ToListAsync();

            lstGoals.Items.Clear();

            foreach (var g in goals)
            {
                lstGoals.Items.Add($"{g.Category.Name} - {g.TargetMinutes} mins");
            }
        }

        //private async void btnSaveGoal_Click(object sender, EventArgs e)
        //{
        //    if (cmbGoalCategory.SelectedItem == null)
        //        return;

        //    string categoryName = cmbGoalCategory.SelectedItem.ToString();
        //    int minutes = (int)numGoalMinutes.Value;

        //    using var db = new AppDbContext();

        //    var category = await db.Categories
        //        .FirstOrDefaultAsync(c => c.Name == categoryName);

        //    if (category == null)
        //        return;

        //    // check if goal already exists for this category
        //    var existing = await db.DailyGoals
        //        .FirstOrDefaultAsync(g => g.CategoryId == category.Id);

        //    if (existing != null)
        //    {
        //        existing.TargetMinutes = minutes; // update
        //    }
        //    else
        //    {
        //        db.DailyGoals.Add(new DailyGoal
        //        {
        //            CategoryId = category.Id,
        //            TargetMinutes = minutes
        //        });
        //    }

        //    await db.SaveChangesAsync();

        //    await LoadGoalsAsync();
        //}

        private void btnCheckGoals_Click(object sender, EventArgs e)
        {
            var service = new ProductivityService();
            var messages = service.GetExceededGoalMessages();

            if (messages.Count == 0)
            {
                MessageBox.Show("No goals exceeded today.");
            }
            else
            {
                MessageBox.Show(string.Join(Environment.NewLine, messages));
            }
        }
        private void LoadCategoriesIntoGoalComboBox()
        {
            using (var db = new AppDbContext())
            {
                var categories = db.Categories.ToList();

                cmbGoalCategory.DataSource = categories;
                cmbGoalCategory.DisplayMember = "Name";
                cmbGoalCategory.ValueMember = "Id";
                //cmbGoalCategory.SelectedIndex = -1;
                if (categories.Count > 0)
                    cmbGoalCategory.SelectedIndex = 0;
            }
        }

        private void LoadSavedGoals()
        {
            lstGoals.Items.Clear();

            using (var db = new AppDbContext())
            {
                var goals = db.DailyGoals.ToList();

                foreach (var goal in goals)
                {
                    var categoryName = db.Categories
                        .Where(c => c.Id == goal.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault() ?? "Unknown";

                    lstGoals.Items.Add($"{categoryName} - {goal.TargetMinutes} minutes");
                }
            }
        }

        private void btnSaveGoal_Click(object sender, EventArgs e)
        {
            if (cmbGoalCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            if (numGoalMinutes.Value <= 0)
            {
                MessageBox.Show("Please enter minutes greater than 0.");
                return;
            }

            var selectedCategory = (Category)cmbGoalCategory.SelectedItem;
            int minutes = (int)numGoalMinutes.Value;

            using (var db = new AppDbContext())
            {
                var existingGoal = db.DailyGoals
                    .FirstOrDefault(g => g.CategoryId == selectedCategory.Id);

                if (existingGoal == null)
                {
                    var goal = new DailyGoal
                    {
                        CategoryId = selectedCategory.Id,
                        TargetMinutes = minutes
                    };

                    db.DailyGoals.Add(goal);
                }
                else
                {
                    existingGoal.TargetMinutes = minutes;
                }

                db.SaveChanges();
            }

            MessageBox.Show("Goal saved successfully.");
            LoadSavedGoals();
        }

        private void btnCheckGoals_Click_1(object sender, EventArgs e)
        {
            var service = new ProductivityService();
            var messages = service.GetExceededGoalMessages();

            if (messages.Count == 0)
            {
                MessageBox.Show("No goals exceeded today.");
            }
            else
            {
                MessageBox.Show(string.Join(Environment.NewLine, messages));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppCatergory form = new AppCatergory();
            form.ShowDialog();
        }
    }
}

