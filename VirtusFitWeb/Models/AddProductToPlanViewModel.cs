using BLL;

namespace VirtusFitWeb.Models
{
    public class AddProductToPlanViewModel
    {
        public int DietPlanId { get; set; }
        public int DayNumber { get; set; }
        public ProductOnDietPlan ProductToAdd { get; set; }
    }
}