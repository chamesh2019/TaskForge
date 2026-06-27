using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Services
{
    public class CategoryReportDto
    {
        public string CategoryName { get; set; } = string.Empty;

        public double TotalMinutes { get; set; }
    }
}
