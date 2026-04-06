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

        
    }
}
