using System;
using System.IO;
using System.Windows.Forms;
using TaskForge.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace TaskForge.Views
{
    public partial class ChartReportForm : Form
    {
        private readonly IReportService _reportService;

        public ChartReportForm(IReportService reportService)
        {
            InitializeComponent();
            _reportService = reportService;
            cmbChartType.SelectedIndex = 0;
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

        private async void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChartType.SelectedIndex == 1)
            {
                await LoadDailyChartAsync();
                btnExportPdf.Enabled = true;
            }
            else if (cmbChartType.SelectedIndex == 2)
            {
                await LoadWeeklyChartAsync();
                btnExportPdf.Enabled = true;
            }
            else
            {
                chartReport.Series.Clear();
                btnExportPdf.Enabled = false;
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFile.FileName = "ChartReport.pdf";

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            string tempImageFile = Path.Combine(Path.GetTempPath(), "chart_export.png");
            chartReport.SaveImage(tempImageFile, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text($"TaskForge Chart Report - {cmbChartType.Text}")
                        .FontSize(20)
                        .Bold();

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);
                        column.Item().Image(tempImageFile);
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated: {DateTime.Now}");
                });
            })
            .GeneratePdf(saveFile.FileName);

            if (File.Exists(tempImageFile))
            {
                File.Delete(tempImageFile);
            }

            MessageBox.Show("Chart PDF exported successfully!");
        }
    }
}