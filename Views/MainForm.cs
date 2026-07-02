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
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;
using System.Drawing;
using System.Data;
using System.IO;
using Color = System.Drawing.Color;
using Size = System.Drawing.Size;
using Point = System.Drawing.Point;
using Font = System.Drawing.Font;



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
        private readonly IReportService _reportService;

        private NotifyIcon? _notificationIcon;
        private bool _allowClose;

        public MainForm(
            IDatabaseInitializer dbInitializer,
            ICategoryService categoryService,
            IIgnoredAppService ignoredAppService,
            IGoalService goalService,
            ITrackingService trackingService,
            INotificationService notificationService,
            IProductivityService productivityService,
            IAppCategoryService appCategoryService,
            IDatabaseBackupService dbBackupService,
            IReportService reportService)
        {
            InitializeComponent();

            QuestPDF.Settings.License = LicenseType.Community;

            _dbInitializer = dbInitializer;
            _categoryService = categoryService;
            _ignoredAppService = ignoredAppService;
            _goalService = goalService;
            _trackingService = trackingService;
            _notificationService = notificationService;
            _productivityService = productivityService;
            _appCategoryService = appCategoryService;
            _dbBackupService = dbBackupService;
            _reportService = reportService;

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
            InitializeSettingsUI();

            // Set initial state for Report dropdowns
            cmbReportType.SelectedIndex = 0;
            if (cmbExportFormat.Items.Count > 0)
            {
                cmbExportFormat.SelectedIndex = 0;
            }
        }

        private void NotificationService_NotificationTriggered(object? sender, string message)
        {
            // Goals checks run in a background thread, so marshal back to UI thread
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => ShowSystemNotification(message)));
            }
            else
            {
                ShowSystemNotification(message);
            }
        }

        private void ShowSystemNotification(string message)
        {
            if (_notificationIcon != null)
            {
                _notificationIcon.ShowBalloonTip(5000, "Daily Goal Achieved", message, ToolTipIcon.Info);
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
            panelReports.Visible = false;
        }

        private async void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = false;
            panelHistory.Visible = true;
            panelSettings.Visible = false;
            panelReports.Visible = false;

            await LoadHistoryDataAsync(cmbApplication.Text, dtFrom.Value, dtTo.Value);
        }

        private async void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = false;
            panelHistory.Visible = false;
            panelSettings.Visible = true;
            panelReports.Visible = false;

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

        private async void timerRefresh_Tick(object sender, EventArgs e)
        {
            LoadChartData();
            var currentSession = _trackingService.GetCurrentSession();
            await _goalService.CheckGoalsAsync(currentSession);
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
                var result = MessageBox.Show(this, 
                    "Do you want to minimize TaskForge to the system tray to keep tracking active in the background?\n\nChoose 'Yes' to hide to tray, 'No' to close/exit the application completely, or 'Cancel' to keep the window open.", 
                    "Exit TaskForge", 
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    e.Cancel = true;
                    this.Hide();
                    if (_notificationIcon != null)
                    {
                        _notificationIcon.ShowBalloonTip(3000, "TaskForge", "Application minimized to system tray. Tracking is active in the background.", ToolTipIcon.Info);
                    }
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else // DialogResult.No
                {
                    _allowClose = true;
                }
            }

            _trackingService.StopTracking();

            if (_notificationIcon != null)
            {
                _notificationIcon.Visible = false;
                _notificationIcon.Dispose();
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
            panelDashboard.Visible = false;
            panelHistory.Visible = false;
            panelSettings.Visible = false;
            panelReports.Visible = true;
        }

        private async void cmbReportType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbReportType.SelectedIndex == 1)
            {
                var report = await _reportService.GetApplicationSummaryAsync();
                dataGridReports.DataSource = report;
                btnExportReport.Enabled = true;
            }
            else if (cmbReportType.SelectedIndex == 2)
            {
                var report = await _reportService.GetCategorySummaryAsync();
                dataGridReports.DataSource = report;
                btnExportReport.Enabled = true;
            }
            else
            {
                dataGridReports.DataSource = null;
                btnExportReport.Enabled = false;
            }
        }

        private void btnCharts_Click(object? sender, EventArgs e)
        {
            using var form = new ChartReportForm(_reportService);
            form.ShowDialog(this);
        }

        private void btnExportReport_Click(object? sender, EventArgs e)
        {
            if (cmbExportFormat.SelectedIndex == 0) // PDF
            {
                btnExportPdf_Click(sender, e);
            }
            else if (cmbExportFormat.SelectedIndex == 1) // Excel
            {
                btnExportExcel_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an export format.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExportPdf_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFile.FileName = "TaskForgeReport.pdf";

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("TaskForge Report")
                        .FontSize(20)
                        .Bold();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            for (int i = 0; i < dataGridReports.Columns.Count; i++)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        table.Header(header =>
                        {
                            for (int i = 0; i < dataGridReports.Columns.Count; i++)
                            {
                                header.Cell().BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Black)
                                    .Padding(5).Text(dataGridReports.Columns[i].HeaderText).Bold();
                            }
                        });

                        foreach (DataGridViewRow row in dataGridReports.Rows)
                        {
                            if (row.IsNewRow)
                                continue;

                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                table.Cell().BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
                                    .Padding(5).Text(row.Cells[i].Value?.ToString() ?? "");
                            }
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated: {DateTime.Now}");
                });
            })
            .GeneratePdf(saveFile.FileName);

            MessageBox.Show("PDF exported successfully!");
        }

        private void btnExportExcel_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            saveFile.FileName = "TaskForgeReport.xlsx";

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Report");

            // Column headers
            for (int i = 0; i < dataGridReports.Columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = dataGridReports.Columns[i].HeaderText;
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            // Data
            int row = 2;

            foreach (DataGridViewRow dgvRow in dataGridReports.Rows)
            {
                if (dgvRow.IsNewRow)
                    continue;

                for (int col = 0; col < dgvRow.Cells.Count; col++)
                {
                    worksheet.Cell(row, col + 1).Value =
                        dgvRow.Cells[col].Value?.ToString() ?? "";
                }

                row++;
            }

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(saveFile.FileName);

            MessageBox.Show("Excel exported successfully!");
        }

        private void InitializeSystemTray()
        {
            _notificationIcon = new NotifyIcon
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

            _notificationIcon.ContextMenuStrip = contextMenu;

            _notificationIcon.DoubleClick += (s, e) => {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            };
        }

        private void InitializeSettingsUI()
        {
            // --- Tab Page 1: Categories ---
            tabPage1.Controls.Clear();

            var tbl1 = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10)
            };
            tbl1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbl1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            var grpAddCategory = new GroupBox
            {
                Text = "Add New Category",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(15)
            };
            
            var lblCategoryName = new Label
            {
                Text = "Enter Category Name:",
                Location = new Point(15, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            txtCategory.Location = new Point(15, 65);
            txtCategory.Size = new Size(grpAddCategory.Width - 30, 27);
            txtCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            btnAddCategory.Text = "Add Category";
            btnAddCategory.Location = new Point(15, 110);
            btnAddCategory.Size = new Size(grpAddCategory.Width - 30, 35);
            btnAddCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAddCategory.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnAddCategory.FlatStyle = FlatStyle.Flat;
            btnAddCategory.BackColor = Color.FromArgb(0, 122, 204);
            btnAddCategory.ForeColor = Color.White;
            btnAddCategory.FlatAppearance.BorderSize = 0;

            grpAddCategory.Controls.Add(lblCategoryName);
            grpAddCategory.Controls.Add(txtCategory);
            grpAddCategory.Controls.Add(btnAddCategory);

            var grpExistingCategories = new GroupBox
            {
                Text = "Existing Categories",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(15)
            };

            lstCategories.Location = new Point(15, 35);
            lstCategories.Size = new Size(grpExistingCategories.Width - 30, 220);
            lstCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            btnDeleteCategory.Text = "Delete Selected";
            btnDeleteCategory.Location = new Point(15, 280);
            btnDeleteCategory.Size = new Size(grpExistingCategories.Width - 30, 35);
            btnDeleteCategory.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDeleteCategory.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnDeleteCategory.FlatStyle = FlatStyle.Flat;
            btnDeleteCategory.BackColor = Color.FromArgb(220, 53, 69);
            btnDeleteCategory.ForeColor = Color.White;
            btnDeleteCategory.FlatAppearance.BorderSize = 0;

            grpExistingCategories.Controls.Add(lstCategories);
            grpExistingCategories.Controls.Add(btnDeleteCategory);

            tbl1.Controls.Add(grpAddCategory, 0, 0);
            tbl1.Controls.Add(grpExistingCategories, 1, 0);
            tabPage1.Controls.Add(tbl1);

            // --- Tab Page 2: Ignore List ---
            tabPage2.Controls.Clear();

            var tbl2 = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10)
            };
            tbl2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbl2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            var grpAddIgnore = new GroupBox
            {
                Text = "Ignore Application",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(15)
            };

            var lblIgnoreName = new Label
            {
                Text = "Enter Application Name to Ignore:",
                Location = new Point(15, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            txtIgnoreApp.Location = new Point(15, 65);
            txtIgnoreApp.Size = new Size(grpAddIgnore.Width - 30, 27);
            txtIgnoreApp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            btnAddIgnore.Text = "Add to Ignore List";
            btnAddIgnore.Location = new Point(15, 110);
            btnAddIgnore.Size = new Size(grpAddIgnore.Width - 30, 35);
            btnAddIgnore.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAddIgnore.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnAddIgnore.FlatStyle = FlatStyle.Flat;
            btnAddIgnore.BackColor = Color.FromArgb(0, 122, 204);
            btnAddIgnore.ForeColor = Color.White;
            btnAddIgnore.FlatAppearance.BorderSize = 0;

            grpAddIgnore.Controls.Add(lblIgnoreName);
            grpAddIgnore.Controls.Add(txtIgnoreApp);
            grpAddIgnore.Controls.Add(btnAddIgnore);

            var grpIgnoredApps = new GroupBox
            {
                Text = "Ignored Applications",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(15)
            };

            lstIgnoredApps.Location = new Point(15, 35);
            lstIgnoredApps.Size = new Size(grpIgnoredApps.Width - 30, 220);
            lstIgnoredApps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            btnDeleteIgnore.Text = "Remove Selected";
            btnDeleteIgnore.Location = new Point(15, 280);
            btnDeleteIgnore.Size = new Size(grpIgnoredApps.Width - 30, 35);
            btnDeleteIgnore.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDeleteIgnore.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnDeleteIgnore.FlatStyle = FlatStyle.Flat;
            btnDeleteIgnore.BackColor = Color.FromArgb(220, 53, 69);
            btnDeleteIgnore.ForeColor = Color.White;
            btnDeleteIgnore.FlatAppearance.BorderSize = 0;

            grpIgnoredApps.Controls.Add(lstIgnoredApps);
            grpIgnoredApps.Controls.Add(btnDeleteIgnore);

            tbl2.Controls.Add(grpAddIgnore, 0, 0);
            tbl2.Controls.Add(grpIgnoredApps, 1, 0);
            tabPage2.Controls.Add(tbl2);

            // --- Tab Page 3: Daily Goals ---
            tabPage3.Controls.Clear();

            var tbl3 = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10)
            };
            tbl3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbl3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            var grpSetGoal = new GroupBox
            {
                Text = "Set Daily Goal",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(15)
            };

            var lblGoalCategory = new Label
            {
                Text = "Select Category:",
                Location = new Point(15, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            cmbGoalCategory.Location = new Point(15, 65);
            cmbGoalCategory.Size = new Size(grpSetGoal.Width - 30, 28);
            cmbGoalCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            var lblGoalMinutes = new Label
            {
                Text = "Target Duration (Minutes):",
                Location = new Point(15, 110),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            numGoalMinutes.Location = new Point(15, 140);
            numGoalMinutes.Size = new Size(grpSetGoal.Width - 30, 27);
            numGoalMinutes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            btnSaveGoal.Text = "Save Daily Goal";
            btnSaveGoal.Location = new Point(15, 190);
            btnSaveGoal.Size = new Size(grpSetGoal.Width - 30, 35);
            btnSaveGoal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSaveGoal.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnSaveGoal.FlatStyle = FlatStyle.Flat;
            btnSaveGoal.BackColor = Color.FromArgb(0, 122, 204);
            btnSaveGoal.ForeColor = Color.White;
            btnSaveGoal.FlatAppearance.BorderSize = 0;

            grpSetGoal.Controls.Add(lblGoalCategory);
            grpSetGoal.Controls.Add(cmbGoalCategory);
            grpSetGoal.Controls.Add(lblGoalMinutes);
            grpSetGoal.Controls.Add(numGoalMinutes);
            grpSetGoal.Controls.Add(btnSaveGoal);

            var grpSavedGoals = new GroupBox
            {
                Text = "Saved Daily Goals",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(15)
            };

            lstGoals.Location = new Point(15, 35);
            lstGoals.Size = new Size(grpSavedGoals.Width - 30, 280);
            lstGoals.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            grpSavedGoals.Controls.Add(lstGoals);

            tbl3.Controls.Add(grpSetGoal, 0, 0);
            tbl3.Controls.Add(grpSavedGoals, 1, 0);
            tabPage3.Controls.Add(tbl3);
        }
    }
}
