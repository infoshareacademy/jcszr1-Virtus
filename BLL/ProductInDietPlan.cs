using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class ProductInDietPlan
    {
        public int OrdinalNumber { get; set; }
        
        public Product Product { get; set; }

        [DisplayName("Portion Size")]
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Portion size must be a positive number")]
        public int PortionSize { get; set; }
        
        [DisplayName("Number of Portions")]
        [Required]
        [Range(1, int.MaxValue,ErrorMessage = "Number of portions must be a positive number")]
        public int NumberOfPortions { get; set; }

        [DisplayName("Total Calories")]
        public int TotalCalories { get; set; }
    }
}
