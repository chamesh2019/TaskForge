using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TaskForge.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadChartData();
            timerRefresh.Start();
        }

        private void LoadChartData()
        {
            chartDashboard.Series[0].Points.Clear();

            chartDashboard.Series[0].Points.AddXY("Chrome", 120);
            chartDashboard.Series[0].Points.AddXY("VS Code", 90);
            chartDashboard.Series[0].Points.AddXY("YouTube", 60);
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = true;
            panelHistory.Visible = false;
            panelSettings.Visible = false;
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = false;
            panelHistory.Visible = true;
            panelSettings.Visible = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDashboard.Visible = false;
            panelHistory.Visible = false;
            panelSettings.Visible = true;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            LoadChartData(); // refresh chart
        }
    }
}
