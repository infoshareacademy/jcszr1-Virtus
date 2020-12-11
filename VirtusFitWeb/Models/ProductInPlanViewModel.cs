using BLL;

namespace VirtusFitWeb.Models
{
    public class ProductInPlanViewModel
    {
        public int DietPlanId { get; set; }
        public int DietPlanNo { get; set; }
        public int DayNumber { get; set; }

        public string Date { get; set; }
        public ProductInDietPlan ProductInPlan { get; set; }
    }
}