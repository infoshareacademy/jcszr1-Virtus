using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtusFitWeb.DAL;

namespace VirtusFitWeb.Services
{
    public class DietPlanService : IDietPlanService
    {
        private readonly IDietPlanRepository _dietPlanRepository;

        public DietPlanService(IDietPlanRepository dietPlanRepository)
        {
            _dietPlanRepository = dietPlanRepository;
        }

        public IEnumerable<DietPlan> ListAllDietPlans()
        {
            return _dietPlanRepository.ListAllDietPlans();
        }

        public DietPlan GetDietPlan(int id)
        {
            return _dietPlanRepository.GetDietPlanById(id);
        }

        public DietPlan Create(DietPlan newDietPlan)
        {
            newDietPlan.DailyDietPlanList = new List<DailyDietPlan>();

            for (var i = 0; i < newDietPlan.Duration.Days; i++)
            {
                newDietPlan.DailyDietPlanList.Add(new DailyDietPlan()
                {
                    DayNumber = i + 1,
                    Date = newDietPlan.StartDate + new TimeSpan(i, 0, 0, 0),
                    ProductListForDay = new List<ProductInDietPlan>()
                }
                );
            }
            _dietPlanRepository.InsertDietPlan(newDietPlan);

            return newDietPlan;
        }

        public List<DailyDietPlan> ListDailyDietPlans(int id)
        {
            var dailyDietPlanList = _dietPlanRepository.ListDailyDietPlans(id).OrderBy(x=>x.DayNumber).ToList();
            return dailyDietPlanList;
        }

        public DailyDietPlan GetDailyDietPlan(int id, int dayNumber)
        {
            return ListDailyDietPlans(id).FirstOrDefault(d => d.DayNumber == dayNumber);
        }

        public List<ProductInDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber)
        {
            var dailyDietPlan = GetDailyDietPlan(id, dayNumber);
            var productList = _dietPlanRepository.ListProductsInDailyDietPlan(dailyDietPlan)
                .OrderBy(x => x.OrdinalNumber).ToList();
            return productList;
        }

        public void Edit(int id, DietPlan dietPlan)
        {
            var oldDailyDietPlanList = ListDailyDietPlans(id);

            dietPlan.DailyDietPlanList = new List<DailyDietPlan>();
            for (var i = 0; i < dietPlan.Duration.Days; i++)
            {
                dietPlan.DailyDietPlanList.Add(new DailyDietPlan()
                {
                    DayNumber = i + 1,
                    Date = dietPlan.StartDate + new TimeSpan(i, 0, 0, 0),
                    DietPlanId = id
                });
            }

            foreach (var daily in oldDailyDietPlanList.Where(daily => !dietPlan.DailyDietPlanList.Exists(x=>x.Date == daily.Date)))
            {
                _dietPlanRepository.DeleteDailyDietPlan(daily);
            }

            foreach (var daily in dietPlan.DailyDietPlanList.Where(daily =>
                !oldDailyDietPlanList.Exists(x => x.Date == daily.Date)))
            {
                _dietPlanRepository.AddDailyDietPlan(daily);
            }

            foreach (var oldDaily in oldDailyDietPlanList)
            {
                foreach (var newDaily in dietPlan.DailyDietPlanList)
                {
                    if (oldDaily.Date == newDaily.Date)
                    {
                        oldDaily.DayNumber = newDaily.DayNumber;
                        _dietPlanRepository.UpdateDailyDietPlan(oldDaily);
                    }
                }
            }

            dietPlan.DailyDietPlanList = null;

            _dietPlanRepository.UpdateDietPlan(dietPlan);
        }

        public void DeleteById(int id)
        {
            _dietPlanRepository.DeleteDietPlan(GetDietPlan(id));
        }
    }
}
