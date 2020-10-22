using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Db_Models;
using VirtusFitWeb.DAL;

namespace VirtusFitWeb.Services
{
    public class ProductInPlanService : IProductInPlanService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDietPlanRepository _dietPlanRepository;

        public ProductInPlanService(IProductRepository productRepository, IDietPlanRepository dietPlanRepository)
        {
            _productRepository = productRepository;
            _dietPlanRepository = dietPlanRepository;
        }

        public List<Product> GetProductList()
        {
            return _productRepository.GetProducts();
        }

        public ProductInDietPlan GetProductToAdd(int id)
        {
            var product = GetProductList().FirstOrDefault(p => p.ProductId == id);
            var productToAdd = new ProductInDietPlan()
            {
                Product = product
            };
            return productToAdd;
        }

        public Product GetProductFromList(int id)
        {
            return GetProductList().FirstOrDefault(p => p.ProductId == id);
        }

        public void AddProductToDailyDietPlan(int id, int dayNumber, ProductInDietPlan productToAdd, Product product)
        {
            var dailyToAddTo = _dietPlanRepository.GetDailyDietPlan(id,dayNumber);
            
            var productToDb = new ProductInDietPlanDb
            {
                OrdinalNumber = _dietPlanRepository.ListProductsInDailyDietPlan(dailyToAddTo).Count + 1,
                ProductId = product.ProductId,
                PortionSize = productToAdd.PortionSize,
                NumberOfPortions = productToAdd.NumberOfPortions,
                TotalCalories = product.Energy * productToAdd.PortionSize * productToAdd.NumberOfPortions / 100,
                DailyDietPlanId = dailyToAddTo.Id
            };

            _dietPlanRepository.AddProductInPlan(productToDb);

            CalculateDailyDietPlanCaloriesAndMacros(dailyToAddTo);
        }

        private void CalculateDailyDietPlanCaloriesAndMacros(DailyDietPlan dailyToCalculate)
        {
            dailyToCalculate.CaloriesSum = 0;
            dailyToCalculate.FatSum = 0;
            dailyToCalculate.CarbohydratesSum = 0;
            dailyToCalculate.ProteinSum = 0;

            var productListForDaily = _dietPlanRepository.ListProductsInDailyDietPlan(dailyToCalculate);

            foreach (var product in productListForDaily)
            {
                dailyToCalculate.CaloriesSum += product.Product.Energy * product.PortionSize * product.NumberOfPortions / 100;
                dailyToCalculate.FatSum += product.Product.Fat * product.PortionSize * product.NumberOfPortions / 100;
                dailyToCalculate.CarbohydratesSum += product.Product.Carbohydrates * product.PortionSize * product.NumberOfPortions / 100;
                dailyToCalculate.ProteinSum += product.Product.Protein * product.PortionSize * product.NumberOfPortions / 100;
            }
            dailyToCalculate.FatSum = Math.Round(dailyToCalculate.FatSum, 2);
            dailyToCalculate.CarbohydratesSum = Math.Round(dailyToCalculate.CarbohydratesSum, 2);
            dailyToCalculate.ProteinSum = Math.Round(dailyToCalculate.ProteinSum, 2);

            _dietPlanRepository.UpdateDailyDietPlan(dailyToCalculate);
        }

        public ProductInDietPlan GetProductFromDietPlan(int id, int dayNumber, int ordinalNumber)
        {
            var currentDailyDietPlan = _dietPlanRepository.GetDailyDietPlan(id, dayNumber);
            return _dietPlanRepository.ListProductsInDailyDietPlan(currentDailyDietPlan)
                .FirstOrDefault(x => x.OrdinalNumber == ordinalNumber);
        }

        public void EditProductInDailyDietPlan(int id, int dayNumber, ProductInDietPlan editedProduct, int currentProductOrdinalNumber)
        {
            var dailyDietPlan = _dietPlanRepository.GetDailyDietPlan(id, dayNumber);
            var oldProduct = _dietPlanRepository.GetProductFromDailyDietPlan(dailyDietPlan,currentProductOrdinalNumber);
            oldProduct.PortionSize = editedProduct.PortionSize;
            oldProduct.NumberOfPortions = editedProduct.NumberOfPortions;
            oldProduct.TotalCalories = editedProduct.Product.Energy * editedProduct.PortionSize *
                editedProduct.NumberOfPortions / 100;
                
            _dietPlanRepository.UpdateProductInPlan(oldProduct);

            CalculateDailyDietPlanCaloriesAndMacros(dailyDietPlan);
        }

        public void DeleteProductFromPlan(int id, int dayNumber, int ordinalNumber)
        {
            var currentDailyDietPlan = _dietPlanRepository.GetDailyDietPlan(id, dayNumber);
            var productToDelete = _dietPlanRepository.GetProductFromDailyDietPlan(currentDailyDietPlan, ordinalNumber);
            
            _dietPlanRepository.DeleteProductInPlan(productToDelete);

            var newProductList = _dietPlanRepository.ListDbProductsInDailyDietPlan(currentDailyDietPlan);
            var newOrdinalNumber = 1;
            foreach (var product in newProductList)
            {
                product.OrdinalNumber = newOrdinalNumber;
                _dietPlanRepository.UpdateProductInPlan(product);
                newOrdinalNumber++;
            }

            CalculateDailyDietPlanCaloriesAndMacros(currentDailyDietPlan);
        }

    }
}
