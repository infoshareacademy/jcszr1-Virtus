using System;
using System.Collections.Generic;

namespace BLL
{
    public class DailyDietPlan
    {
        public int DietPlanId { get; set; }
        public int DayNumber { get; set; }
        public DateTime Date { get; set; }
        public int CaloriesSum { get; set; }
        public int FatSum { get; set; }
        public int CarbohydratesSum { get; set; }
        public int ProteinSum { get; set; }
        public List<ProductOnDietPlan> ProductListForDay { get; set; }
    }
}
