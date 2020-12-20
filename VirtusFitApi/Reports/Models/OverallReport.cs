using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Reports.Models
{
    public class OverallReport
    {
        public int CreatedUserAccounts { get; set; }
        public int TotalLogonCount { get; set; }
        public int AddedProducts { get; set; }
        public int RemovedProducts { get; set; }
        public int AddedPlans { get; set; }
        public int ProductsAddedToFav { get; set; }
        public int ProductsRemovedFromFav { get; set; }
        public int ProductsAddedToPlans { get; set; }
        public int SearchesDone { get; set; }
        public string TopStringSearch { get; set; }
        public double AvgFatSearch { get; set; }
        public double AvgCaloriesSearch { get; set; }
        public double AvgCarbohydratesSearch { get; set; }
        public double AvgProteinsSearch { get; set; }
        public string TopFavId { get; set; }
        public double AvgUserBmi { get; set; }
        public double AvgPlanLength { get; set; }
        public double AvgPlanCalories { get; set; }

    }
}
