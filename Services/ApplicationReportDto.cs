using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Services
{
    public class ApplicationReportDto
    {
        public string ApplicationName { get; set; } = string.Empty;

        public double TotalMinutes { get; set; }
    }
}
