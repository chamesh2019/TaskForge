using System;
using System.Windows.Forms;
using TaskForge.Services;

namespace TaskForge.Views
{
    public partial class ChartReportForm : Form
    {
        private readonly IReportService _reportService;

        public ChartReportForm(IReportService reportService)
        {
            InitializeComponent();
            _reportService = reportService;
        }

        private async Task LoadDailyChartAsync()
        {
            var report = await _reportService.GetApplicationSummaryAsync();

            chartReport.Series.Clear();

            var series = chartReport.Series.Add("Applications");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            foreach (var item in report)
            {
                series.Points.AddXY(item.ApplicationName, item.TotalMinutes);
            }
        }

        private async void btnDailyChart_Click(object sender, EventArgs e)
        {
            await LoadDailyChartAsync();
        }

        private async Task LoadWeeklyChartAsync()
        {
            var report = await _reportService.GetWeeklySummaryAsync();

            chartReport.Series.Clear();

            var series = chartReport.Series.Add("Weekly");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            foreach (var item in report)
            {
                series.Points.AddXY(item.Day, item.TotalMinutes);
            }
        }

        private async void btnWeeklyChart_Click(object sender, EventArgs e)
        {
            await LoadWeeklyChartAsync();
        }

        private void btnWeeklyChart_Click_1(object sender, EventArgs e)
        {

        }
    }
}