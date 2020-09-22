using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Models
{
    public class DailyProductListViewModel
    {
        public int DailyPlanId { get; set; }
        public int DayNumber { get; set; }
        public int CaloriesPerDay { get; set; }
        public List<ProductInDietPlan> ProductListForDay { get; set; }
    }
}