using System;
using System.Windows.Forms;
using TaskForge.Services;

namespace TaskForge.Views
{
    public partial class ReportsForm : Form
    {
        private readonly IReportService _reportService;

        public ReportsForm(IReportService reportService)
        {
            InitializeComponent();
            _reportService = reportService;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var report = await _reportService.GetApplicationSummaryAsync();
            dataGridReports.DataSource = report;
        }

        private async void btnLoadCategories_Click(object sender, EventArgs e)
        {
            var report = await _reportService.GetCategorySummaryAsync();
            dataGridReports.DataSource = report;
        }

        
    }
}