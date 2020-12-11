using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using VirtusFitApi.Models;
using VirtusFitWeb.DAL;

namespace VirtusFitWeb.Services
{
    public class DietPlanService : IDietPlanService
    {
        private readonly IDietPlanRepository _dietPlanRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public DietPlanService(IDietPlanRepository dietPlanRepository, IHttpClientFactory httpClientFactory)
        {
            _dietPlanRepository = dietPlanRepository;
            _httpClientFactory = httpClientFactory;
        }

        private CreateDietPlanAction CreateAction(ActionType type, int planId, int length, string userId, int calories)
        {
            var action = new CreateDietPlanAction
            {
                Username = userId,
                DietPlanId = planId,
                Length = length,
                CaloriesPerDay = calories,
                Action = type,
                Created = DateTime.UtcNow
            };
            return action;
        }

        public List<DietPlan> ListAllDietPlans(string userId)
        {
            return _dietPlanRepository.ListAllDietPlans(userId);
        }

        public DietPlan GetDietPlan(int id)
        {
            return _dietPlanRepository.GetDietPlanById(id);
        }

        public DietPlan Create(DietPlan newDietPlan, string username)
        {
            newDietPlan.DailyDietPlanList = new List<DailyDietPlan>();
            if (ListAllDietPlans(newDietPlan.UserId).Count == 0) newDietPlan.PlanNo = 1;
            else newDietPlan.PlanNo = ListAllDietPlans(newDietPlan.UserId).Max(d=>d.PlanNo) + 1;

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

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.AddedDietPlan, newDietPlan.Id, newDietPlan.DailyDietPlanList.Count, username, newDietPlan.CaloriesPerDay);
            client.PostAsync("https://localhost:5001/VirtusFit/plan/dietplan",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

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

        public void Edit(int id, DietPlan dietPlan, string username)
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

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.EditedDietPlan, dietPlan.Id, dietPlan.DailyDietPlanList.Count, username, dietPlan.CaloriesPerDay);
            client.PostAsync("https://localhost:5001/VirtusFit/plan/dietplan",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

            _dietPlanRepository.UpdateDietPlan(dietPlan);
        }

        public void DeleteById(int id, string userid, string username)
        {
            var length = Convert.ToInt32(GetDietPlan(id).Duration);
            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.RemovedDietPlan, id, length, username, GetDietPlan(id).CaloriesPerDay);
            client.PostAsync("https://localhost:5001/VirtusFit/plan/dietplan",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            _dietPlanRepository.DeleteDietPlan(GetDietPlan(id));
        }
    }
}
