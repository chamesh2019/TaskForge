namespace TaskForge.Views
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            panelDashboard = new Panel();
            lblTotalTime = new Label();
            chartDashboard = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panelHistory = new Panel();
            dataGridHistory = new DataGridView();
            panelFilters = new Panel();
            cmbCategory = new ComboBox();
            lblCategory = new Label();
            dtTo = new DateTimePicker();
            dtFrom = new DateTimePicker();
            lblTo = new Label();
            lblFrom = new Label();
            cmbApplication = new ComboBox();
            lblApplication = new Label();
            panelSettings = new Panel();
            tabSettings = new TabControl();
            tabPage1 = new TabPage();
            txtCategory = new TextBox();
            lstCategories = new ListBox();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            menuStrip1 = new MenuStrip();
            dashboardToolStripMenuItem = new ToolStripMenuItem();
            historyToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            timerRefresh = new System.Windows.Forms.Timer(components);
            btnAddCategory = new Button();
            btnDeleteCategory = new Button();
            panelDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartDashboard).BeginInit();
            panelHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridHistory).BeginInit();
            panelFilters.SuspendLayout();
            panelSettings.SuspendLayout();
            tabSettings.SuspendLayout();
            tabPage1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panelDashboard
            // 
            panelDashboard.Controls.Add(lblTotalTime);
            panelDashboard.Controls.Add(chartDashboard);
            panelDashboard.Dock = DockStyle.Fill;
            panelDashboard.Location = new Point(0, 0);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Size = new Size(800, 450);
            panelDashboard.TabIndex = 0;
            // 
            // lblTotalTime
            // 
            lblTotalTime.AutoSize = true;
            lblTotalTime.BackColor = Color.White;
            lblTotalTime.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalTime.ForeColor = Color.Black;
            lblTotalTime.Location = new Point(12, 57);
            lblTotalTime.Name = "lblTotalTime";
            lblTotalTime.Size = new Size(177, 20);
            lblTotalTime.TabIndex = 1;
            lblTotalTime.Text = "Total Time Today: 0 min";
            // 
            // chartDashboard
            // 
            chartArea2.Name = "ChartArea1";
            chartDashboard.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartDashboard.Legends.Add(legend2);
            chartDashboard.Location = new Point(0, 31);
            chartDashboard.Name = "chartDashboard";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartDashboard.Series.Add(series2);
            chartDashboard.Size = new Size(800, 419);
            chartDashboard.TabIndex = 0;
            chartDashboard.Text = "chart1";
            // 
            // panelHistory
            // 
            panelHistory.Controls.Add(dataGridHistory);
            panelHistory.Controls.Add(panelFilters);
            panelHistory.Dock = DockStyle.Fill;
            panelHistory.Location = new Point(0, 0);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(1000, 450);
            panelHistory.TabIndex = 1;
            panelHistory.Visible = false;
            // 
            // dataGridHistory
            // 
            dataGridHistory.AllowUserToAddRows = false;
            dataGridHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridHistory.Dock = DockStyle.Fill;
            dataGridHistory.Location = new Point(0, 80);
            dataGridHistory.Name = "dataGridHistory";
            dataGridHistory.ReadOnly = true;
            dataGridHistory.RowHeadersWidth = 51;
            dataGridHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridHistory.Size = new Size(1000, 370);
            dataGridHistory.TabIndex = 0;
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(cmbCategory);
            panelFilters.Controls.Add(lblCategory);
            panelFilters.Controls.Add(dtTo);
            panelFilters.Controls.Add(dtFrom);
            panelFilters.Controls.Add(lblTo);
            panelFilters.Controls.Add(lblFrom);
            panelFilters.Controls.Add(cmbApplication);
            panelFilters.Controls.Add(lblApplication);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(0, 0);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1000, 80);
            panelFilters.TabIndex = 0;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Items.AddRange(new object[] { "All", "Work", "Entertainment", "Other" });
            cmbCategory.Location = new Point(788, 35);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(151, 28);
            cmbCategory.TabIndex = 8;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(710, 38);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(72, 20);
            lblCategory.TabIndex = 7;
            lblCategory.Text = "Category:";
            // 
            // dtTo
            // 
            dtTo.Format = DateTimePickerFormat.Short;
            dtTo.Location = new Point(543, 36);
            dtTo.Name = "dtTo";
            dtTo.Size = new Size(100, 27);
            dtTo.TabIndex = 6;
            dtTo.ValueChanged += dtTo_ValueChanged;
            // 
            // dtFrom
            // 
            dtFrom.Format = DateTimePickerFormat.Short;
            dtFrom.Location = new Point(374, 36);
            dtFrom.Name = "dtFrom";
            dtFrom.Size = new Size(100, 27);
            dtFrom.TabIndex = 5;
            dtFrom.ValueChanged += dtFrom_ValueChanged;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(509, 38);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(28, 20);
            lblTo.TabIndex = 4;
            lblTo.Text = "To:";
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(322, 38);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(46, 20);
            lblFrom.TabIndex = 3;
            lblFrom.Text = "From:";
            // 
            // cmbApplication
            // 
            cmbApplication.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbApplication.FormattingEnabled = true;
            cmbApplication.Location = new Point(107, 35);
            cmbApplication.Name = "cmbApplication";
            cmbApplication.Size = new Size(180, 28);
            cmbApplication.TabIndex = 1;
            cmbApplication.SelectedIndexChanged += cmbApplication_SelectedIndexChanged;
            // 
            // lblApplication
            // 
            lblApplication.AutoSize = true;
            lblApplication.Location = new Point(12, 38);
            lblApplication.Name = "lblApplication";
            lblApplication.Size = new Size(89, 20);
            lblApplication.TabIndex = 0;
            lblApplication.Text = "Application:";
            // 
            // panelSettings
            // 
            panelSettings.Controls.Add(tabSettings);
            panelSettings.Dock = DockStyle.Fill;
            panelSettings.Location = new Point(0, 0);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(1000, 450);
            panelSettings.TabIndex = 2;
            panelSettings.Visible = false;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(tabPage1);
            tabSettings.Controls.Add(tabPage2);
            tabSettings.Controls.Add(tabPage3);
            tabSettings.Dock = DockStyle.Fill;
            tabSettings.Location = new Point(0, 0);
            tabSettings.Name = "tabSettings";
            tabSettings.SelectedIndex = 0;
            tabSettings.Size = new Size(1000, 450);
            tabSettings.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnDeleteCategory);
            tabPage1.Controls.Add(btnAddCategory);
            tabPage1.Controls.Add(txtCategory);
            tabPage1.Controls.Add(lstCategories);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(992, 417);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Categories";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(410, 15);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(275, 27);
            txtCategory.TabIndex = 1;
            // 
            // lstCategories
            // 
            lstCategories.FormattingEnabled = true;
            lstCategories.Location = new Point(8, 15);
            lstCategories.Name = "lstCategories";
            lstCategories.Size = new Size(320, 104);
            lstCategories.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(992, 417);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Ignore List";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(992, 417);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Daily Goals";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dashboardToolStripMenuItem, historyToolStripMenuItem, settingsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1000, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // dashboardToolStripMenuItem
            // 
            dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            dashboardToolStripMenuItem.Size = new Size(96, 24);
            dashboardToolStripMenuItem.Text = "Dashboard";
            dashboardToolStripMenuItem.Click += dashboardToolStripMenuItem_Click;
            // 
            // historyToolStripMenuItem
            // 
            historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            historyToolStripMenuItem.Size = new Size(70, 24);
            historyToolStripMenuItem.Text = "History";
            historyToolStripMenuItem.Click += historyToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(76, 24);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // timerRefresh
            // 
            timerRefresh.Interval = 5000;
            timerRefresh.Tick += timerRefresh_Tick;
            // 
            // btnAddCategory
            // 
            btnAddCategory.Location = new Point(726, 15);
            btnAddCategory.Name = "btnAddCategory";
            btnAddCategory.Size = new Size(179, 29);
            btnAddCategory.TabIndex = 2;
            btnAddCategory.Text = "Add";
            btnAddCategory.UseVisualStyleBackColor = true;
            // 
            // btnDeleteCategory
            // 
            btnDeleteCategory.Location = new Point(482, 59);
            btnDeleteCategory.Name = "btnDeleteCategory";
            btnDeleteCategory.Size = new Size(331, 29);
            btnDeleteCategory.TabIndex = 3;
            btnDeleteCategory.Text = "Delete Selected";
            btnDeleteCategory.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 450);
            Controls.Add(menuStrip1);
            Controls.Add(panelDashboard);
            Controls.Add(panelHistory);
            Controls.Add(panelSettings);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "TaskForge";
            panelDashboard.ResumeLayout(false);
            panelDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartDashboard).EndInit();
            panelHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridHistory).EndInit();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            panelSettings.ResumeLayout(false);
            tabSettings.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelDashboard;
        private Panel panelHistory;
        private Panel panelSettings;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dashboardToolStripMenuItem;
        private ToolStripMenuItem historyToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDashboard;
        private System.Windows.Forms.Timer timerRefresh;
        private Label lblTotalTime;
        private DataGridView dataGridHistory;
        private Panel panelFilters;
        private ComboBox cmbApplication;
        private Label lblApplication;
        private DateTimePicker dtFrom;
        private Label lblTo;
        private Label lblFrom;
        private DateTimePicker dtTo;
        private ComboBox cmbCategory;
        private Label lblCategory;
        private TabControl tabSettings;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private ListBox lstCategories;
        private TextBox txtCategory;
        private Button btnDeleteCategory;
        private Button btnAddCategory;
    }
}