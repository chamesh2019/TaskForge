namespace TaskForge.Views
{
    partial class ReportsForm
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
            dataGridReports = new DataGridView();
            btnLoadApplications = new Button();
            btnLoadCategories = new Button();
            btnCharts = new Button();
            btnExportPdf = new Button();
            btnExportExcel = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridReports).BeginInit();
            SuspendLayout();
            // 
            // dataGridReports
            // 
            dataGridReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridReports.Location = new Point(0, 0);
            dataGridReports.Name = "dataGridReports";
            dataGridReports.RowHeadersWidth = 72;
            dataGridReports.Size = new Size(693, 739);
            dataGridReports.TabIndex = 0;
            // 
            // btnLoadApplications
            // 
            btnLoadApplications.Location = new Point(753, 57);
            btnLoadApplications.Name = "btnLoadApplications";
            btnLoadApplications.Size = new Size(410, 150);
            btnLoadApplications.TabIndex = 1;
            btnLoadApplications.Text = "Application Report";
            btnLoadApplications.UseVisualStyleBackColor = true;
            btnLoadApplications.Click += button1_Click;
            // 
            // btnLoadCategories
            // 
            btnLoadCategories.Location = new Point(753, 234);
            btnLoadCategories.Name = "btnLoadCategories";
            btnLoadCategories.Size = new Size(410, 150);
            btnLoadCategories.TabIndex = 2;
            btnLoadCategories.Text = "Category Report";
            btnLoadCategories.UseVisualStyleBackColor = true;
            btnLoadCategories.Click += btnLoadCategories_Click;
            // 
            // btnCharts
            // 
            btnCharts.Location = new Point(753, 527);
            btnCharts.Name = "btnCharts";
            btnCharts.Size = new Size(359, 150);
            btnCharts.TabIndex = 4;
            btnCharts.Text = "Charts";
            btnCharts.UseVisualStyleBackColor = true;
            btnCharts.Click += btnCharts_Click;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Location = new Point(753, 403);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(182, 52);
            btnExportPdf.TabIndex = 5;
            btnExportPdf.Text = "Export PDF";
            btnExportPdf.UseVisualStyleBackColor = true;
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(981, 403);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(182, 52);
            btnExportExcel.TabIndex = 6;
            btnExportExcel.Text = "Export Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1193, 761);
            Controls.Add(btnExportExcel);
            Controls.Add(btnExportPdf);
            Controls.Add(btnCharts);
            Controls.Add(btnLoadCategories);
            Controls.Add(btnLoadApplications);
            Controls.Add(dataGridReports);
            Name = "ReportsForm";
            Text = "ReportsForm";
            ((System.ComponentModel.ISupportInitialize)dataGridReports).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridReports;
        private Button btnLoadApplications;
        private Button btnLoadCategories;
        private Button Weekly_Chart;
        private Button btnCharts;
        private Button btnExportPdf;
        private Button btnExportExcel;
    }
}