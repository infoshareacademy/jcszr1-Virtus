using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtusFitWeb.Logic
{
    public class DietPlanService : IDietPlanService
    {
        private static List<DietPlan> _dietPlans = new List<DietPlan>
        {
            new DietPlan()
            {
                StartDate = new DateTime(2020,09,10),
                EndDate = new DateTime(2020,09,11),
                Id = 1,
                CaloriesPerDay = 2150,
                DailyDietPlanList = new List<DailyDietPlan>()
                {
                    new DailyDietPlan()
                    {
                        DietPlanId = 1,
                        DayNumber = 1,
                        Date = new DateTime(2020,09,10),
                        ProductListForDay = new List<ProductOnDietPlan>()
                    }, 
                    new DailyDietPlan()
                    {
                        DietPlanId = 1,
                        DayNumber = 2,
                        Date = new DateTime(2020,09,11),
                        ProductListForDay = new List<ProductOnDietPlan>()
                    }
                }
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

        public Product GetProductFromList(int id)
        {
            return _products.FirstOrDefault(p => p.ProductId == id);
        }

        public DietPlan Create(DietPlan newDietPlan)
        {
            newDietPlan.Id = _dietPlans.Count() + 1;
            newDietPlan.DailyDietPlanList = new List<DailyDietPlan>();

            for (int i = 0; i < newDietPlan.Duration.Days; i++)
            {
                newDietPlan.DailyDietPlanList.Add(new DailyDietPlan()
                    {
                        DietPlanId = newDietPlan.Id,
                        DayNumber = i + 1,
                        ProductListForDay = new List<ProductOnDietPlan>()
                    }
                );
            }
            _dietPlans.Add(newDietPlan);

            return newDietPlan;
        }

        public IEnumerable<DailyDietPlan> ListDailyDietPlans(int id)
        {
            return GetDietPlan(id).DailyDietPlanList;
        }

        public DailyDietPlan GetDailyDietPlan(int id, int dayNumber)
        {
            return ListDailyDietPlans(id).FirstOrDefault(d => d.DayNumber == dayNumber);
        }

        public IEnumerable<ProductOnDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber)
        {
            return _dietPlans[id - 1].DailyDietPlanList[dayNumber - 1].ProductListForDay;
        }

        public void AddProductToDailyDietPlan(int id, int dayNumber, ProductOnDietPlan productToAdd, Product product)
        {
            _dietPlans[id - 1].DailyDietPlanList[dayNumber - 1].ProductListForDay.Add(new ProductOnDietPlan
            {
                Product = product,
                PortionSize = productToAdd.PortionSize,
                NumberOfPortions = productToAdd.NumberOfPortions
            });
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
