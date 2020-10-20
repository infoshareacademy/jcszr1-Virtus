using System.Collections.Generic;
using BLL;

namespace VirtusFitWeb.DAL
{
    public interface IDietPlanRepository
    {
        List<DietPlan> ListAllDietPlans();
        DietPlan GetDietPlanById(int id);
        void InsertDietPlan(DietPlan dietPlan);
        void DeleteDietPlan(DietPlan dietPlan);
        void UpdateDietPlan(DietPlan dietPlan);
    }
}
