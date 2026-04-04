using System;

namespace TaskForge.Tracking
{
    public class TrackedSession
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; } = string.Empty;
        public string WindowTitle { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public TimeSpan Duration => (EndTime ?? DateTime.Now) - StartTime;
    }
}
