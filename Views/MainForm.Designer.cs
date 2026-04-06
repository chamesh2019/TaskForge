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
            panelDashboard = new Panel();
            panelHistory = new Panel();
            panelSettings = new Panel();
            menuStrip1 = new MenuStrip();
            dashboardToolStripMenuItem = new ToolStripMenuItem();
            historyToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            panelSettings.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panelDashboard
            // 
            panelDashboard.Dock = DockStyle.Fill;
            panelDashboard.Location = new Point(0, 0);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Size = new Size(800, 450);
            panelDashboard.TabIndex = 0;
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
            menuStrip1.Dock = DockStyle.Top;
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
            Text = "MainForm";
            panelSettings.ResumeLayout(false);
            panelSettings.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelDashboard;
        private Panel panelHistory;
        private Panel panelSettings;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dashboardToolStripMenuItem;
        private ToolStripMenuItem historyToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
    }
}