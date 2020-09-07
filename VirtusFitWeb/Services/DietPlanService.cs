using System;
using System.Collections.Generic;
using System.Linq;
using VirtusFitWeb.Models;

namespace VirtusFitWeb.Services
{
    public class DietPlanService : IDietPlanService
    {
        private static List<DietPlan> _dietPlans = new List<DietPlan>
        {
            new DietPlan
            {
                Id = 1, StartDate = new DateTime(2020,09,07), EndDate = new DateTime(2020,09,17), CaloriesPerDay = 2000
            }
        };
        public IEnumerable<DietPlan> ListAll()
        {
            return _dietPlans;
        }

        public DietPlan GetDietPlan(int id)
        {
            return _dietPlans.FirstOrDefault(p => p.Id == id);
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
