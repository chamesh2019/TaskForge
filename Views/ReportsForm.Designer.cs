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
            ((System.ComponentModel.ISupportInitialize)dataGridReports).BeginInit();
            SuspendLayout();
            // 
            // dataGridReports
            // 
            dataGridReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridReports.Location = new Point(0, 0);
            dataGridReports.Name = "dataGridReports";
            dataGridReports.RowHeadersWidth = 72;
            dataGridReports.Size = new Size(716, 723);
            dataGridReports.TabIndex = 0;
            // 
            // btnLoadApplications
            // 
            btnLoadApplications.Location = new Point(753, 57);
            btnLoadApplications.Name = "btnLoadApplications";
            btnLoadApplications.Size = new Size(359, 150);
            btnLoadApplications.TabIndex = 1;
            btnLoadApplications.Text = "Application Report";
            btnLoadApplications.UseVisualStyleBackColor = true;
            btnLoadApplications.Click += button1_Click;
            // 
            // btnLoadCategories
            // 
            btnLoadCategories.Location = new Point(753, 293);
            btnLoadCategories.Name = "btnLoadCategories";
            btnLoadCategories.Size = new Size(359, 150);
            btnLoadCategories.TabIndex = 2;
            btnLoadCategories.Text = "Category Report";
            btnLoadCategories.UseVisualStyleBackColor = true;
            btnLoadCategories.Click += btnLoadCategories_Click;
            // 
            // btnCharts
            // 
            btnCharts.Location = new Point(753, 528);
            btnCharts.Name = "btnCharts";
            btnCharts.Size = new Size(359, 150);
            btnCharts.TabIndex = 4;
            btnCharts.Text = "Charts";
            btnCharts.UseVisualStyleBackColor = true;
            btnCharts.Click += btnCharts_Click;

            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1193, 761);
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
    }
}