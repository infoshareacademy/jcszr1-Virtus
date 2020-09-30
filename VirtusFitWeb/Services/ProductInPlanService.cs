using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtusFitWeb.Services
{
    public class ProductInPlanService : IProductInPlanService
    {
        private readonly IDietPlanService _dietPlanService;
        private readonly List<Product> _products = ProductLoader.GetProductsFromFile();

        public ProductInPlanService(IDietPlanService dietPlanService)
        {
            _dietPlanService = dietPlanService;
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

        public void AddProductToDailyDietPlan(int id, int dayNumber, ProductInDietPlan productToAdd, Product product)
        {
            var dailyToAddTo = _dietPlanService.GetDailyDietPlan(id, dayNumber);

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
            var dailyPlanToCalc = _dietPlanService.GetDailyDietPlan(id, dayNumber);
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

        public ProductInDietPlan GetProductFromDietPlan(int id, int dayNumber, int ordinalNumber)
        {
            return this._dietPlanService.GetDailyDietPlan(id, dayNumber).ProductListForDay[ordinalNumber - 1];
        }

        public void EditProductInDailyDietPlan(int id, int dayNumber, ProductInDietPlan editedProduct, int currentProductOrdinalNumber)
        {
            var oldProduct = this._dietPlanService.GetDailyDietPlan(id, dayNumber).ProductListForDay[currentProductOrdinalNumber - 1];
            var newProduct = new ProductInDietPlan()
            {
                OrdinalNumber = oldProduct.OrdinalNumber,
                Product = oldProduct.Product,
                PortionSize = editedProduct.PortionSize,
                NumberOfPortions = editedProduct.NumberOfPortions,
                TotalCalories = oldProduct.Product.Energy * editedProduct.PortionSize * editedProduct.NumberOfPortions / 100
            };

            this._dietPlanService.GetDailyDietPlan(id, dayNumber).ProductListForDay[currentProductOrdinalNumber - 1] = newProduct;

            CalculateDailyDietPlanCaloriesAndMacros(id, dayNumber);
        }

        public void DeleteProductFromPlan(int id, int dayNumber, int ordinalNumber)
        {
            var currentDailyDietPlan = this._dietPlanService.GetDailyDietPlan(id, dayNumber);
            currentDailyDietPlan.ProductListForDay.Remove(GetProductFromDietPlan(id, dayNumber, ordinalNumber));
            var newOrdinalNumber = 0;
            foreach (var product in currentDailyDietPlan.ProductListForDay)
            {
                product.OrdinalNumber = newOrdinalNumber + 1;
                newOrdinalNumber++;
            }

            CalculateDailyDietPlanCaloriesAndMacros(id, dayNumber);
        }

    }
}
