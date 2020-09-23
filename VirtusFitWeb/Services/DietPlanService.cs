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
                        ProductListForDay = new List<ProductInDietPlan>()
                    },
                    new DailyDietPlan()
                    {
                        DietPlanId = 1,
                        DayNumber = 2,
                        Date = new DateTime(2020,09,11),
                        ProductListForDay = new List<ProductInDietPlan>()
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

        public ProductInDietPlan GetProductToAdd(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            var productToAdd = new ProductInDietPlan()
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
            var highestId = _dietPlans.Select(dietPlan => dietPlan.Id).Max();
            newDietPlan.Id = highestId + 1;
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

        public void AddProductToDailyDietPlan(int id, int dayNumber, ProductInDietPlan productToAdd, Product product)
        {
            var dailyToAddTo = GetDailyDietPlan(id, dayNumber);

            dailyToAddTo.ProductListForDay.Add(new ProductInDietPlan
            {
                OrdinalNumber = dailyToAddTo.ProductListForDay.Count + 1,
                Product = product,
                PortionSize = productToAdd.PortionSize,
                NumberOfPortions = productToAdd.NumberOfPortions,
                TotalCalories = product.Energy * productToAdd.PortionSize * productToAdd.NumberOfPortions / 100
            });

            CalculateDailyDietPlanCaloriesAndMacros(id, dayNumber);
        }

        private void CalculateDailyDietPlanCaloriesAndMacros(int id, int dayNumber)
        {
            var dailyPlanToCalc = GetDailyDietPlan(id, dayNumber);
            dailyPlanToCalc.CaloriesSum = 0;
            dailyPlanToCalc.FatSum = 0;
            dailyPlanToCalc.CarbohydratesSum = 0;
            dailyPlanToCalc.ProteinSum = 0;

            foreach (var product in dailyPlanToCalc.ProductListForDay)
            {
                dailyPlanToCalc.CaloriesSum += product.Product.Energy * product.PortionSize * product.NumberOfPortions / 100;
                dailyPlanToCalc.FatSum += product.Product.Fat * product.PortionSize * product.NumberOfPortions / 100;
                dailyPlanToCalc.CarbohydratesSum += product.Product.Carbohydrates * product.PortionSize * product.NumberOfPortions / 100;
                dailyPlanToCalc.ProteinSum += product.Product.Protein * product.PortionSize * product.NumberOfPortions / 100;
            }
            dailyPlanToCalc.FatSum = Math.Round(dailyPlanToCalc.FatSum, 2);
            dailyPlanToCalc.CarbohydratesSum = Math.Round(dailyPlanToCalc.CarbohydratesSum, 2);
            dailyPlanToCalc.ProteinSum = Math.Round(dailyPlanToCalc.ProteinSum, 2);
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

        public ProductInDietPlan GetProductFromDietPlan(int id, int dayNumber, int ordinalNumber)
        {
            return this.GetDailyDietPlan(id, dayNumber).ProductListForDay[ordinalNumber - 1];
        }

        public void EditProductInDailyDietPlan(int id, int dayNumber, ProductInDietPlan editedProduct, int currentProductOrdinalNumber)
        {
            var oldProduct = this.GetDailyDietPlan(id, dayNumber).ProductListForDay[currentProductOrdinalNumber - 1];
            var newProduct = new ProductInDietPlan()
            {
                OrdinalNumber = oldProduct.OrdinalNumber,
                Product = oldProduct.Product,
                PortionSize = editedProduct.PortionSize,
                NumberOfPortions = editedProduct.NumberOfPortions,
                TotalCalories = oldProduct.Product.Energy * editedProduct.PortionSize * editedProduct.NumberOfPortions / 100
            };

            this.GetDailyDietPlan(id, dayNumber).ProductListForDay[currentProductOrdinalNumber - 1] = newProduct;

            CalculateDailyDietPlanCaloriesAndMacros(id, dayNumber);
        }

        public void DeleteProductFromPlan(int id, int dayNumber, int ordinalNumber)
        {
            var currentDailyDietPlan = _dietPlans[id - 1].DailyDietPlanList[dayNumber - 1];
            currentDailyDietPlan.ProductListForDay.Remove(GetProductFromDietPlan(id, dayNumber, ordinalNumber));
            var newOrdinalNumber = 0;
            foreach (var product in currentDailyDietPlan.ProductListForDay)
            {
                product.OrdinalNumber = newOrdinalNumber + 1;
                newOrdinalNumber++;
            }
        }

    }
}
