using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskForge.Tracking
{
    public class App_Category
    {
        [Key]
        public int Id { get; set; }

        public string Category { get; set; } = "Neutral";

        public string AppName { get; set; }

    }
}
