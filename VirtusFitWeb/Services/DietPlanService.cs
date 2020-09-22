using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtusFitWeb.Services
{
    public class DietPlanService : IDietPlanService
    {
        private readonly List<DietPlan> _dietPlans = new List<DietPlan>
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

        public List<Product> GetProductList()
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
                    Date = newDietPlan.StartDate + new TimeSpan(i, 0, 0, 0),
                    ProductListForDay = new List<ProductOnDietPlan>()
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

        public List<ProductOnDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber)
        {
            return _dietPlans[id - 1].DailyDietPlanList[dayNumber - 1].ProductListForDay;
        }

        public void AddProductToDailyDietPlan(int id, int dayNumber, ProductOnDietPlan productToAdd, Product product)
        {
            var dailyToAddTo = _dietPlans[id - 1].DailyDietPlanList[dayNumber - 1];

            dailyToAddTo.ProductListForDay.Add(new ProductOnDietPlan
            {
                Product = product,
                PortionSize = productToAdd.PortionSize,
                NumberOfPortions = productToAdd.NumberOfPortions
            });

            dailyToAddTo.CaloriesSum += product.Energy * productToAdd.PortionSize / 100 * productToAdd.NumberOfPortions;
            dailyToAddTo.FatSum += product.Fat * productToAdd.PortionSize / 100 * productToAdd.NumberOfPortions;
            dailyToAddTo.FatSum = Math.Round(dailyToAddTo.FatSum, 2);
            dailyToAddTo.CarbohydratesSum += product.Carbohydrates * productToAdd.PortionSize / 100 * productToAdd.NumberOfPortions;
            dailyToAddTo.CarbohydratesSum = Math.Round(dailyToAddTo.CarbohydratesSum, 2);
            dailyToAddTo.ProteinSum += product.Protein * productToAdd.PortionSize / 100 * productToAdd.NumberOfPortions;
            dailyToAddTo.ProteinSum = Math.Round(dailyToAddTo.ProteinSum, 2);

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
                    DayNumber = i+1,
                    Date = editedDietPlan.StartDate + new TimeSpan(i,0,0,0),
                    ProductListForDay = new List<ProductOnDietPlan>()
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
