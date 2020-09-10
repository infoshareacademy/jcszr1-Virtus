using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class DietPlan
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter start date.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter end date.")]
        public DateTime EndDate { get; set; }

        public TimeSpan Duration => (EndDate - StartDate).Add(new TimeSpan(1,0,0,0));

        [Required(ErrorMessage = "Enter daily calories amount.")]
        public int CaloriesPerDay { get; set; }

        public List<DailyDietPlan> DailyDietPlanList { get; set; }
    }
}
