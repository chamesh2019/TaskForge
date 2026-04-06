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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            panelDashboard = new Panel();
            lblTotalTime = new Label();
            chartDashboard = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panelHistory = new Panel();
            panelSettings = new Panel();
            menuStrip1 = new MenuStrip();
            dashboardToolStripMenuItem = new ToolStripMenuItem();
            historyToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            timerRefresh = new System.Windows.Forms.Timer(components);
            panelDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartDashboard).BeginInit();
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
            chartArea1.Name = "ChartArea1";
            chartDashboard.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartDashboard.Legends.Add(legend1);
            chartDashboard.Location = new Point(0, 31);
            chartDashboard.Name = "chartDashboard";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartDashboard.Series.Add(series1);
            chartDashboard.Size = new Size(800, 419);
            chartDashboard.TabIndex = 0;
            chartDashboard.Text = "chart1";
            // 
            // panelHistory
            // 
            panelHistory.Dock = DockStyle.Fill;
            panelHistory.Location = new Point(0, 0);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(800, 450);
            panelHistory.TabIndex = 1;
            panelHistory.Visible = false;
            // 
            // panelSettings
            // 
            panelSettings.Dock = DockStyle.Fill;
            panelSettings.Location = new Point(0, 0);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(800, 450);
            panelSettings.TabIndex = 2;
            panelSettings.Visible = false;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dashboardToolStripMenuItem, historyToolStripMenuItem, settingsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}