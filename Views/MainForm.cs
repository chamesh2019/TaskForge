using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using TaskForge.Data;
using TaskForge.Tracking;
using System.Linq;
using System.Diagnostics;

namespace TaskForge.Views
{
    public partial class MainForm : Form
    {
        private WindowTracker _tracker;

        public MainForm()
        {
            InitializeComponent();

            // database is created
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }

            // initialize tracker and start
            _tracker = new WindowTracker();
            _tracker.ActiveWindowChanged += Tracker_ActiveWindowChanged;
            _tracker.SessionEnded += Tracker_SessionEnded;
            _tracker.Start();

            LoadChartData();
            timerRefresh.Start();
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

        private async Task<Dictionary<string, double>> GetTodayAppTimesAsync()
        {
            using var db = new AppDbContext();

            // get start of today
            DateTime today = DateTime.Today;

            // get all tracked sessions for today
            var sessions = await db.TrackedSessions
                .Where(s => s.StartTime >= today)
                .ToListAsync();


            // group by application and sum durations in minutes
            var appTimes = sessions
                .GroupBy(s => s.ApplicationName)
                .ToDictionary(
                    g => g.Key, 
                    g => g.Sum(s => s.Duration.TotalMinutes)
                );
            return appTimes;
        }


        private async void LoadChartData()
        {
            chartDashboard.Series[0].Points.Clear();
            chartDashboard.Series[0].IsValueShownAsLabel = true;
            chartDashboard.Series[0]["PieLabelStyle"] = "Outside";
            chartDashboard.Series[0]["PieLineColor"] = "Black";
            chartDashboard.Series[0]["PieStartAngle"] = "270";

            var appTimes = await GetTodayAppTimesAsync();

            double total = 0;

            foreach(var app in appTimes)
            {
                double value = Math.Round(app.Value, 2);
                chartDashboard.Series[0].Points.AddXY(app.Key, value);
                total += value;
            }

            lblTotalTime.Text = $"Total Time: {Math.Round(total, 2)} mins";
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            LoadChartData(); // refresh chart
        }


        private void Tracker_ActiveWindowChanged(object? sender, TrackedSession e)
        {
            // show in debug output
            Debug.WriteLine($"[Active Window Changed] App: {e.ApplicationName}, Title: {e.WindowTitle}");
        }

        private void Tracker_SessionEnded(object? sender, TrackedSession e)
        {
            
            // save to database
            Task.Run(() =>
            {
                try
                {
                    using var db = new AppDbContext();
                    db.TrackedSessions.Add(e);
                    db.SaveChanges();
                    Debug.WriteLine("Session saved to database.");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to save session: {ex.Message}");
                }
            });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _tracker.Stop();
            base.OnFormClosing(e);
        }


    }
}
