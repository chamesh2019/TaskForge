namespace TaskForge.Views
{
    partial class ChartReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartReport = new System.Windows.Forms.DataVisualization.Charting.Chart();
            cmbChartType = new ComboBox();
            btnExportPdf = new Button();
            ((System.ComponentModel.ISupportInitialize)chartReport).BeginInit();
            SuspendLayout();
            // 
            // chartReport
            // 
            chartArea1.Name = "ChartArea1";
            chartReport.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartReport.Legends.Add(legend1);
            chartReport.Location = new Point(3, 56);
            chartReport.Margin = new Padding(2, 2, 2, 2);
            chartReport.Name = "chartReport";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartReport.Series.Add(series1);
            chartReport.Size = new Size(762, 320);
            chartReport.TabIndex = 0;
            chartReport.Text = "chart1";
            // 
            // cmbChartType
            // 
            cmbChartType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChartType.FormattingEnabled = true;
            cmbChartType.Items.AddRange(new object[] { "Select Chart Type", "Daily Chart", "Weekly Chart" });
            cmbChartType.Location = new Point(81, 11);
            cmbChartType.Margin = new Padding(2, 2, 2, 2);
            cmbChartType.Name = "cmbChartType";
            cmbChartType.Size = new Size(145, 23);
            cmbChartType.TabIndex = 1;
            cmbChartType.SelectedIndexChanged += cmbChartType_SelectedIndexChanged;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Enabled = false;
            btnExportPdf.Location = new Point(249, 7);
            btnExportPdf.Margin = new Padding(2, 2, 2, 2);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(111, 30);
            btnExportPdf.TabIndex = 2;
            btnExportPdf.Text = "Export PDF";
            btnExportPdf.UseVisualStyleBackColor = true;
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // ChartReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 387);
            Controls.Add(btnExportPdf);
            Controls.Add(cmbChartType);
            Controls.Add(chartReport);
            Margin = new Padding(2, 2, 2, 2);
            Name = "ChartReportForm";
            Text = "ChartReportForm";
            ((System.ComponentModel.ISupportInitialize)chartReport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartReport;
        private ComboBox cmbChartType;
        private Button btnExportPdf;
    }
}