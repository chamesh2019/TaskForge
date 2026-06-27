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
            btnDailyChart = new Button();
            btnWeeklyChart = new Button();
            ((System.ComponentModel.ISupportInitialize)chartReport).BeginInit();
            SuspendLayout();
            // 
            // chartReport
            // 
            chartArea1.Name = "ChartArea1";
            chartReport.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartReport.Legends.Add(legend1);
            chartReport.Location = new Point(12, 237);
            chartReport.Name = "chartReport";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartReport.Series.Add(series1);
            chartReport.Size = new Size(1307, 525);
            chartReport.TabIndex = 0;
            chartReport.Text = "chart1";
            // 
            // btnDailyChart
            // 
            btnDailyChart.Location = new Point(226, 58);
            btnDailyChart.Name = "btnDailyChart";
            btnDailyChart.Size = new Size(344, 125);
            btnDailyChart.TabIndex = 1;
            btnDailyChart.Text = "Daily Report";
            btnDailyChart.UseVisualStyleBackColor = true;
            btnDailyChart.Click += btnDailyChart_Click;
            // 
            // btnWeeklyChart
            // 
            btnWeeklyChart.Location = new Point(743, 58);
            btnWeeklyChart.Name = "btnWeeklyChart";
            btnWeeklyChart.Size = new Size(344, 125);
            btnWeeklyChart.TabIndex = 2;
            btnWeeklyChart.Text = "Weekly Report";
            btnWeeklyChart.UseVisualStyleBackColor = true;
            btnWeeklyChart.Click += btnWeeklyChart_Click;
            // 
            // ChartReportForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1331, 774);
            Controls.Add(btnWeeklyChart);
            Controls.Add(btnDailyChart);
            Controls.Add(chartReport);
            Name = "ChartReportForm";
            Text = "ChartReportForm";
            ((System.ComponentModel.ISupportInitialize)chartReport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartReport;
        private Button btnDailyChart;
        private Button btnWeeklyChart;
    }
}