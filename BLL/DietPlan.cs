using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL
{

    public class DietPlan
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Plan No.")]
        public int PlanNo { get; set; }
        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Enter start date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [DisplayName("End Date")]
        [Required(ErrorMessage = "Enter end date.")]
        [DataType(DataType.Date)]
        
        public DateTime EndDate { get; set; }

        public TimeSpan Duration => (EndDate - StartDate).Add(new TimeSpan(1,0,0,0));
        
        [DisplayName("Calories per day")]
        [Required(ErrorMessage = "Enter daily calories amount.")]
        [Range(1,int.MaxValue,ErrorMessage = "Calories value must be a positive number.")]
        public int CaloriesPerDay { get; set; }

        public List<DailyDietPlan> DailyDietPlanList { get; set; }
        public string UserId { get; set; }
    }
}
