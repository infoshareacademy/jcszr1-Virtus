using System;
using System.Collections.Generic;
using System.Linq;
using VirtusFitWeb.Models;

namespace VirtusFitWeb.Services
{
    public class DietPlanService : IDietPlanService
    {
        private static List<DietPlan> _dietPlans = new List<DietPlan>();
        public IEnumerable<DietPlan> ListAll()
        {
            return _dietPlans;
        }
        public DietPlan Create(DietPlan newDietPlan)
        {
            newDietPlan.Id = _dietPlans.Max(plan => plan.Id) + 1;
            _dietPlans.Add(newDietPlan);

            return newDietPlan;
        }
        public bool Edit(int id, DietPlan dietPlan)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
