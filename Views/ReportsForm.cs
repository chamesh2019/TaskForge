using System;
using System.Windows.Forms;
using TaskForge.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;

namespace TaskForge.Views
{
    public partial class ReportsForm : Form
    {
        private readonly IReportService _reportService;

        public ReportsForm(IReportService reportService)
        {
            InitializeComponent();

            QuestPDF.Settings.License = LicenseType.Community;

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

        private void btnCharts_Click(object sender, EventArgs e)
        {
            using var form = new ChartReportForm(_reportService);
            form.ShowDialog(this);
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
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

                    page.Content().Column(column =>
                    {
                        column.Spacing(5);

                        foreach (DataGridViewRow row in dataGridReports.Rows)
                        {
                            if (row.IsNewRow)
                                continue;

                            string line = "";

                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                line += $"{cell.Value}    ";
                            }

                            column.Item().Text(line);
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

        private void btnExportExcel_Click(object sender, EventArgs e)
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
    }
}