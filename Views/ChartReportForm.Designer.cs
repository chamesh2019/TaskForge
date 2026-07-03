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
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)chartReport).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // chartReport
            // 
            chartArea1.Name = "ChartArea1";
            chartReport.ChartAreas.Add(chartArea1);
            chartReport.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chartReport.Legends.Add(legend1);
            chartReport.Location = new Point(0, 69);
            chartReport.Margin = new Padding(2, 3, 2, 3);
            chartReport.Name = "chartReport";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartReport.Series.Add(series1);
            chartReport.Size = new Size(887, 447);
            chartReport.TabIndex = 0;
            chartReport.Text = "chart1";
            // 
            // cmbChartType
            // 
            cmbChartType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChartType.FormattingEnabled = true;
            cmbChartType.Items.AddRange(new object[] { "Select Chart Type", "Daily Chart", "Weekly Chart" });
            cmbChartType.Location = new Point(42, 21);
            cmbChartType.Margin = new Padding(2, 3, 2, 3);
            cmbChartType.Name = "cmbChartType";
            cmbChartType.Size = new Size(165, 28);
            cmbChartType.TabIndex = 1;
            cmbChartType.SelectedIndexChanged += cmbChartType_SelectedIndexChanged;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Enabled = false;
            btnExportPdf.Location = new Point(237, 14);
            btnExportPdf.Margin = new Padding(2, 3, 2, 3);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(127, 40);
            btnExportPdf.TabIndex = 2;
            btnExportPdf.Text = "Export PDF";
            btnExportPdf.UseVisualStyleBackColor = true;
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(cmbChartType);
            panel1.Controls.Add(btnExportPdf);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(887, 69);
            panel1.TabIndex = 3;
            // 
            // ChartReportForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(887, 516);
            Controls.Add(chartReport);
            Controls.Add(panel1);
            Margin = new Padding(2, 3, 2, 3);
            Name = "ChartReportForm";
            Text = "ChartReportForm";
            ((System.ComponentModel.ISupportInitialize)chartReport).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartReport;
        private ComboBox cmbChartType;
        private Button btnExportPdf;
        private Panel panel1;
    }
}