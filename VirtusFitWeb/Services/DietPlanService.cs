using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtusFitWeb.Services
{
    public class DietPlanService : IDietPlanService
    {
        private readonly List<DietPlan> _dietPlans = new List<DietPlan>();
        
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
            if (_dietPlans.Count > 0)
            {
                var highestId = _dietPlans.Select(dietPlan => dietPlan.Id).Max();
                newDietPlan.Id = highestId + 1;
            }
            else newDietPlan.Id = 1;
            newDietPlan.DailyDietPlanList = new List<DailyDietPlan>();

            for (int i = 0; i < newDietPlan.Duration.Days; i++)
            {
                newDietPlan.DailyDietPlanList.Add(new DailyDietPlan()
                {
                    DietPlanId = newDietPlan.Id,
                    DayNumber = i + 1,
                    Date = newDietPlan.StartDate + new TimeSpan(i, 0, 0, 0),
                    ProductListForDay = new List<ProductInDietPlan>()
                }
                );
            }
            _dietPlans.Add(newDietPlan);

            return newDietPlan;
        }

        public List<DailyDietPlan> ListDailyDietPlans(int id)
        {
            return GetDietPlan(id).DailyDietPlanList;
        }

        public DailyDietPlan GetDailyDietPlan(int id, int dayNumber)
        {
            return ListDailyDietPlans(id).FirstOrDefault(d => d.DayNumber == dayNumber);
        }

        public List<ProductInDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber)
        {
            return _dietPlans[id - 1].DailyDietPlanList[dayNumber - 1].ProductListForDay;
        }

        public void Edit(int id, DietPlan dietPlan)
        {
            var editedDietPlan = dietPlan;
            var dietPlanToEdit = _dietPlans[id - 1];

            editedDietPlan.DailyDietPlanList = new List<DailyDietPlan>();
            for (var i = 0; i < editedDietPlan.Duration.Days; i++)
            {
                editedDietPlan.DailyDietPlanList.Add(new DailyDietPlan()
                {
                    DietPlanId = id,
                    DayNumber = i + 1,
                    Date = editedDietPlan.StartDate + new TimeSpan(i, 0, 0, 0),
                    ProductListForDay = new List<ProductInDietPlan>()
                });
            }

            foreach (var dailyDietPlanInEdited in editedDietPlan.DailyDietPlanList)
            {
                foreach (var dailyDietPlanInToEdit in dietPlanToEdit.DailyDietPlanList)
                {
                    if (dailyDietPlanInEdited.Date == dailyDietPlanInToEdit.Date)
                    {
                        dailyDietPlanInEdited.ProductListForDay = dailyDietPlanInToEdit.ProductListForDay;
                        dailyDietPlanInEdited.CaloriesSum = dailyDietPlanInToEdit.CaloriesSum;
                        dailyDietPlanInEdited.FatSum = dailyDietPlanInToEdit.FatSum;
                        dailyDietPlanInEdited.CarbohydratesSum = dailyDietPlanInToEdit.CarbohydratesSum;
                        dailyDietPlanInEdited.ProteinSum = dailyDietPlanInToEdit.ProteinSum;
                    }
                }
            }

            _dietPlans[id - 1] = editedDietPlan;
        }

        public void DeleteById(int id)
        {
            _dietPlans.Remove(GetDietPlan(id));
        }
    }
}
