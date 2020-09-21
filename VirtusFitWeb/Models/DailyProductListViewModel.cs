using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Models
{
    public class DailyProductListViewModel
    {
        public int DailyPlanId { get; set; }
        public int DayNumber { get; set; }
        public List<ProductOnDietPlan> ProductListForDay { get; set; }
    }
}