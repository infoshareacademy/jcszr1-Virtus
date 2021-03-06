﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Reports.Models
{
    public class UserReport
    {
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogon { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public string State { get; set; }
        public int TotalLogonCount { get; set; }
        public int TotalAddedProducts { get; set; }
        public int TotalAddedPlans { get; set; }
        public string MostUsedProduct { get; set; }
        public double AvgPlanCalories { get; set; }
        public double AvgPlanLength { get; set; }
        public int TotalFav { get; set; }
    }
}
