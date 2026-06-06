using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TaskForge.Data;
using TaskForge.Tracking;

namespace TaskForge.Views
{
    public partial class AppCatergory : Form
    {
        public AppCatergory()
        {
            InitializeComponent();

            List<string> categories = LoadCategoryNames();
            List<AppCategoryRow> rows = LoadAppRows();

            appCategoryData.RowHeadersVisible = false;
            appCategoryData.Columns.Clear();
            appCategoryData.AutoGenerateColumns = false;

            appCategoryData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ApplicationName",
                HeaderText = "Application",
                Name = "colAppName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn
            {
                HeaderText = "Category",
                Name = "colCategory",
                DataPropertyName = "Category",
                DataSource = categories,
                FlatStyle = FlatStyle.Flat
            };

            appCategoryData.Columns.Add(comboCol);
            appCategoryData.DataSource = rows;

            appCategoryData.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            appCategoryData.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            appCategoryData.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private List<string> LoadCategoryNames()
        {
            using var db = new AppDbContext();

            return db.Categories
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToList();
        }

        private List<AppCategoryRow> LoadAppRows()
        {
            using var db = new AppDbContext();

            var apps = db.TrackedSessions
                .Select(s => s.ApplicationName)
                .Distinct()
                .ToList();

            var savedMappings = db.AppCategories.ToList();

            var rows = new List<AppCategoryRow>();

            foreach (var app in apps)
            {
                var savedCategory = savedMappings
                    .FirstOrDefault(x => x.AppName == app)?.Category ?? "";

                rows.Add(new AppCategoryRow
                {
                    ApplicationName = app,
                    Category = savedCategory
                });
            }

            return rows;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();

            db.AppCategories.RemoveRange(db.AppCategories);
            db.SaveChanges();

            foreach (DataGridViewRow row in appCategoryData.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string appName = row.Cells["colAppName"].Value?.ToString() ?? "";
                string category = row.Cells["colCategory"].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(appName) ||
                    string.IsNullOrWhiteSpace(category))
                {
                    continue;
                }

                var existing = db.AppCategories
                    .FirstOrDefault(x => x.AppName == appName);

                if (existing == null)
                {
                    db.AppCategories.Add(new App_Category
                    {
                        AppName = appName,
                        Category = category
                    });
                }
                else
                {
                    existing.Category = category;
                }
            }

            db.SaveChanges();

            MessageBox.Show("Saved successfully.");
        }
    }

    public class AppCategoryRow
    {
        public string ApplicationName { get; set; } = "";
        public string Category { get; set; } = "";
    }
}