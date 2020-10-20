using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class DailyDietPlan
    {
        [Key]
        public int DietPlanId { get; set; }

        [DisplayName("Day Number")]
        public int DayNumber { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DisplayName("Calories Total")]
        public int CaloriesSum { get; set; }

        [DisplayName("Fat Total")]
        public double FatSum { get; set; }

        [DisplayName("Carbohydrates Total")]
        public double CarbohydratesSum { get; set; }

        [DisplayName("Protein Total")]
        public double ProteinSum { get; set; }

        public List<ProductInDietPlan> ProductListForDay { get; set; }
    }
}
