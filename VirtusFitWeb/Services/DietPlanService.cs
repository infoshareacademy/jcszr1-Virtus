using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtusFitWeb.Models;

namespace VirtusFitWeb.Services
{
    public class DietPlanService : IDietPlanService
    {
        private static readonly List<DietPlan> _dietPlans = new List<DietPlan>
        {
            new DietPlan
            {
                Id = 1, StartDate = new DateTime(2020,09,07), EndDate = new DateTime(2020,09,17), CaloriesPerDay = 1700
            },
            new DietPlan
            {
                Id = 2, StartDate = new DateTime(2020,09,17), EndDate = new DateTime(2020,09,25), CaloriesPerDay = 1900
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

        public IEnumerable<DailyDietPlan> ListDailyDietPlans(int id)
        {
            var plan = GetDietPlan(id);
            plan.DailyDietPlanList = new List<DailyDietPlan>();
            
            for (int i = 0; i < plan.Duration.Days; i++)
            {
                plan.DailyDietPlanList.Add(new DailyDietPlan()
                    {
                        DietPlanId = id, DayNumber = i + 1
                    }
                );
            }
            return plan.DailyDietPlanList;
        }
        public DailyDietPlan GetDailyDietPlan(int id, int dayNumber)
        {
            return ListDailyDietPlans(id).FirstOrDefault(d => d.DayNumber == dayNumber);
        }

        public IEnumerable<ProductOnDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber)
        {
            var dailyPlan = GetDailyDietPlan(id, dayNumber);
            dailyPlan.ProductListForDay = new List<ProductOnDietPlan>();

            dailyPlan.ProductListForDay.Add(new ProductOnDietPlan()
            {
                Product = new Product()
                {
                    ProductName = "Bułka",
                    Energy = 100
                },
                PortionSize = 100,
                NumberOfPortions = 1
            });
            return dailyPlan.ProductListForDay;
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
