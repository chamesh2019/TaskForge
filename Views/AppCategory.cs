using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TaskForge.Services;

namespace TaskForge.Views
{
    public partial class AppCategory : Form
    {
        private readonly IAppCategoryService _appCategoryService;
        private readonly ICategoryService _categoryService;

        public AppCategory(IAppCategoryService appCategoryService, ICategoryService categoryService)
        {
            InitializeComponent();

            _appCategoryService = appCategoryService;
            _categoryService = categoryService;

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                var categoryNames = categories.Select(c => c.Name).OrderBy(n => n).ToList();

                var rows = await _appCategoryService.GetAppCategoryRowsAsync();

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
                    DataSource = categoryNames,
                    FlatStyle = FlatStyle.Flat
                };

                appCategoryData.Columns.Add(comboCol);
                appCategoryData.DataSource = rows;

                appCategoryData.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                appCategoryData.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                appCategoryData.ColumnHeadersDefaultCellStyle.Font =
                    new Font("Segoe UI", 10, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to load app category mapping: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = new List<AppCategoryRow>();

                foreach (DataGridViewRow row in appCategoryData.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    string appName = row.Cells["colAppName"].Value?.ToString() ?? "";
                    string category = row.Cells["colCategory"].Value?.ToString() ?? "";

                    if (!string.IsNullOrWhiteSpace(appName) && !string.IsNullOrWhiteSpace(category))
                    {
                        rows.Add(new AppCategoryRow
                        {
                            ApplicationName = appName,
                            Category = category
                        });
                    }
                }

                await _appCategoryService.SaveMappingsAsync(rows);
                MessageBox.Show(this, "Saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to save mappings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}