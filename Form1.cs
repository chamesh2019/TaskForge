using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskForge.Data;
using TaskForge.Tracking;

namespace TaskForge
{
    public partial class Form1 : Form
    {
        private WindowTracker _tracker;

        public Form1()
        {
            InitializeComponent();

            // Ensure database is created
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }

            _tracker = new WindowTracker();
            _tracker.ActiveWindowChanged += Tracker_ActiveWindowChanged;
            _tracker.SessionEnded += Tracker_SessionEnded;

            // Start tracking
            _tracker.Start();
        }

        private void Tracker_ActiveWindowChanged(object? sender, TrackedSession e)
        {
            Debug.WriteLine($"[Active Window Changed] App: {e.ApplicationName}, Title: {e.WindowTitle}");
        }

        private void Tracker_SessionEnded(object? sender, TrackedSession e)
        {
            Debug.WriteLine($"[Session Ended] App: {e.ApplicationName}, Duration: {e.Duration.TotalSeconds:F2} seconds");

            // Save to database
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
