using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Db_Models
{
    public class ProductInDietPlanDb
    {
        [Key]
        public int Id { get; set; }
        public int OrdinalNumber { get; set; }

        public int ProductId { get; set; }

        [DisplayName("Portion Size")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Portion size must be a positive number")]
        public int PortionSize { get; set; }

        [DisplayName("Number of Portions")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of portions must be a positive number")]
        public int NumberOfPortions { get; set; }

        [DisplayName("Total Calories")]
        public int TotalCalories { get; set; }
        public int DailyDietPlanId { get; set; }

    }
}
