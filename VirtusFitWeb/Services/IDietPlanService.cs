using System.Collections.Generic;
using VirtusFitWeb.Models;

namespace VirtusFitWeb.Services
{
    public interface IDietPlanService
    {
        IEnumerable<DietPlan> ListAll();
        DietPlan GetDietPlan(int id);
        bool Edit(int id, DietPlan dietPlan);
        bool DeleteById(int id);
        DietPlan Create(DietPlan newDietPlan);
    }
}
