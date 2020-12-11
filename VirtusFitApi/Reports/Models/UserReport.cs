using System;
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
        public int TotalAddedProducts { get; set; }
        public int TotalAddedPlans { get; set; }
        public int MostUsedProduct { get; set; }
        public double AvgPlanCalories { get; set; }
        public double AvgPlanLength { get; set; }
        public int TotalFav { get; set; }
    }
}
