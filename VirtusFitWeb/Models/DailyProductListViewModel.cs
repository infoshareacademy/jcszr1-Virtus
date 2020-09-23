using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Models
{
    public class DailyProductListViewModel
    {
        public int DietPlanId { get; set; }
        public int DayNumber { get; set; }
        public int CaloriesPerDay { get; set; }
        public int TotalCalories { get; set; }
        public List<ProductInDietPlan> ProductListForDay { get; set; }
    }
}