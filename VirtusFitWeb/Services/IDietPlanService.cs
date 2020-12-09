using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IDietPlanService
    {
        List<DietPlan> ListAllDietPlans(string userId);
        DietPlan GetDietPlan(int id);
        void Edit(int id, DietPlan dietPlan);
        void DeleteById(int id);
        DietPlan Create(DietPlan newDietPlan);
        List<DailyDietPlan> ListDailyDietPlans(int id);
        DailyDietPlan GetDailyDietPlan(int id, int dayNumber);
        List<ProductInDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber);
    }
}
