using BLL;
using BLL.Db_Models;
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
    public class ProductInPlanService : IProductInPlanService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDietPlanRepository _dietPlanRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductInPlanService(IProductRepository productRepository, IDietPlanRepository dietPlanRepository, IHttpClientFactory httpClientFactory)
        {
            _productRepository = productRepository;
            _dietPlanRepository = dietPlanRepository;
            _httpClientFactory = httpClientFactory;
        }

        private CreateProductInPlanAction CreateAction(ActionType type, int planId, int dailyPlanId, ProductInDietPlan product, string username)
        {
            var action = new CreateProductInPlanAction
            {
                Username = username,
                ProductId = product.Product.ProductId,
                ProductName = product.Product.ProductName,
                DailyDietPlanId = dailyPlanId,
                DietPlanId = planId,
                Action = type,
                Created = DateTime.UtcNow
            };
            return action;
        }

        public List<Product> GetProductList(string userId)
        {
            return _productRepository.GetProducts(userId);
        }

        public ProductInDietPlan GetProductToAdd(int id)
        {
            var product = _productRepository.GetProductById(id);
            var productToAdd = new ProductInDietPlan
            {
                Product = product
            };
            return productToAdd;
        }

        public Product GetProductFromList(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void AddProductToDailyDietPlan(int id, int dayNumber, ProductInDietPlan productToAdd, Product product, string user, string username)
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


            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.AddedProductToExistingDailyPlan, dailyToAddTo.DietPlanId, dailyToAddTo.Id, productToAdd, username);

            client.PostAsync("https://localhost:5001/VirtusFit/plan/productinplan",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

            CalculateDailyDietPlanCaloriesAndMacros(dailyToAddTo);
        }

        public void CalculateDailyDietPlanCaloriesAndMacros(DailyDietPlan dailyToCalculate)
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

        public void EditProductInDailyDietPlan(int id, int dayNumber, ProductInDietPlan editedProduct, int currentProductOrdinalNumber, string user, string username)
        {
            var dailyDietPlan = _dietPlanRepository.GetDailyDietPlan(id, dayNumber);
            var oldProduct = _dietPlanRepository.GetProductFromDailyDietPlan(dailyDietPlan,currentProductOrdinalNumber);
            oldProduct.PortionSize = editedProduct.PortionSize;
            oldProduct.NumberOfPortions = editedProduct.NumberOfPortions;
            oldProduct.TotalCalories = editedProduct.Product.Energy * editedProduct.PortionSize *
                editedProduct.NumberOfPortions / 100;
                
            _dietPlanRepository.UpdateProductInPlan(oldProduct);

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.EditedProductInExistingDailyPlan, id, dailyDietPlan.DietPlanId, editedProduct, username);

            client.PostAsync("https://localhost:5001/VirtusFit/plan/productinplan",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

            CalculateDailyDietPlanCaloriesAndMacros(dailyDietPlan);
        }

        public void DeleteProductFromPlan(int id, int dayNumber, int ordinalNumber, string user, string username)
        {
            var currentDailyDietPlan = _dietPlanRepository.GetDailyDietPlan(id, dayNumber);
            var productToDelete = _dietPlanRepository.GetProductFromDailyDietPlan(currentDailyDietPlan, ordinalNumber);

            var productToAddToApi = GetProductFromDietPlan(id, dayNumber, ordinalNumber);

            _dietPlanRepository.DeleteProductInPlan(productToDelete);

            var newProductList = _dietPlanRepository.ListDbProductsInDailyDietPlan(currentDailyDietPlan);
            var newOrdinalNumber = 1;
            foreach (var product in newProductList)
            {
                product.OrdinalNumber = newOrdinalNumber;
                _dietPlanRepository.UpdateProductInPlan(product);
                newOrdinalNumber++;
            }

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.RemovedProductFromExistingDailyPlan, id, currentDailyDietPlan.DietPlanId, productToAddToApi, username);

            client.PostAsync("https://localhost:5001/VirtusFit/plan/productinplan",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

            CalculateDailyDietPlanCaloriesAndMacros(currentDailyDietPlan);
        }

    }
}
