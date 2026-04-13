using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Tracking
{
    public class DailyGoal
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } 

        public int TargetMinutes { get; set; }
    }
}
