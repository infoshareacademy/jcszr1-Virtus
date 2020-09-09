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

        private readonly List<Product> _products = ProductLoader.GetProductsFromFile();

        public IEnumerable<DietPlan> ListAll()
        {
            return _dietPlans;
        }

        public DietPlan GetDietPlan(int id)
        {
            return _dietPlans.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProductList()
        {
            return _products;
        }

        public ProductOnDietPlan GetProductToAdd(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            var productToAdd = new ProductOnDietPlan()
            {
                Product = product
            };
            return productToAdd;
        }


        public DietPlan Create(DietPlan newDietPlan)
        {
            newDietPlan.Id = _dietPlans.Max(plan => plan.Id) + 1;
            _dietPlans.Add(newDietPlan);

            return newDietPlan;
        }

        public IEnumerable<DailyDietPlan> ListDailyDietPlans(int id)
        {
            
            GetDietPlan(id).DailyDietPlanList = new List<DailyDietPlan>();
            
            for (int i = 0; i < GetDietPlan(id).Duration.Days; i++)
            {
                GetDietPlan(id).DailyDietPlanList.Add(new DailyDietPlan()
                    {
                        DietPlanId = id, DayNumber = i + 1
                    }
                );
            }

            return GetDietPlan(id).DailyDietPlanList;
        }
        public DailyDietPlan GetDailyDietPlan(int id, int dayNumber)
        {
            return ListDailyDietPlans(id).FirstOrDefault(d => d.DayNumber == dayNumber);
        }

        public IEnumerable<ProductOnDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber)
        {
            var dailyPlan = GetDailyDietPlan(id, dayNumber);
            dailyPlan.ProductListForDay = new List<ProductOnDietPlan>();

            return dailyPlan.ProductListForDay;
        }

        public void AddProductToDailyDietPlan(DailyDietPlan dailyDietPlan, ProductOnDietPlan productToAdd)
        {
            dailyDietPlan.ProductListForDay = new List<ProductOnDietPlan> {productToAdd};
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
