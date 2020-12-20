using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Models
{
    public class PlanDetailsViewModel
    {
        public int DietPlanId { get; set; }
        public int DietPlanNo { get; set; }
        public int PlanCaloriesPerDay { get; set; }
        public List<DailyDietPlan> DailyDietPlans { get; set; }
    }
}
