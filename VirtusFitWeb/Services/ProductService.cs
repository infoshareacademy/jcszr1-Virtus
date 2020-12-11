using System;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using VirtusFitApi.Models;
using VirtusFitWeb.DAL;
using IProductRepository = VirtusFitWeb.DAL.IProductRepository;

namespace VirtusFitWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDietPlanRepository _dietPlanRepository;
        private readonly IProductInPlanService _productInPlanService;
        private readonly SearchProductLogic _searchProductLogic = new SearchProductLogic();
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IProductRepository productRepository, IDietPlanRepository dietPlanRepository, IProductInPlanService productInPlanService, IHttpClientFactory httpClientFactory)
        {
            _productRepository = productRepository;
            _dietPlanRepository = dietPlanRepository;
            _productInPlanService = productInPlanService;
            _httpClientFactory = httpClientFactory;
        }

        public List<Product> GetAll(string userId)
        {
            return _productRepository.GetProducts(userId);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void DeleteById(int id, string userId, string username)
        {
            var product = GetById(id);
            DeleteFromExistingPlan(product, userId);
            _productRepository.DeleteProduct(product);

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.RemovedProduct, product.ProductId, product.ProductName, username);
            client.PostAsync("https://localhost:5001/VirtusFit/product",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
        }

        public Product Create(Product newProduct, string userId, string username)
        {
            newProduct.ProductNo = GetAll(userId).Max(p => p.ProductNo) + 1;
            _productRepository.InsertProduct(newProduct);

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.AddedNewProduct, newProduct.ProductId, newProduct.ProductName, username);
            client.PostAsync("https://localhost:5001/VirtusFit/product",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

            return newProduct;
        }

        public void Update(int id, Product product, string userId, string username)
        {
            var productToBeUpdated = GetById(id);
            productToBeUpdated.ProductNo = product.ProductNo;
            productToBeUpdated.ProductName = product.ProductName;
            productToBeUpdated.Energy = product.Energy;
            productToBeUpdated.Fat = product.Fat;
            productToBeUpdated.Carbohydrates = product.Carbohydrates;
            productToBeUpdated.Protein = product.Protein;
            productToBeUpdated.Salt = product.Salt;
            productToBeUpdated.Fiber = product.Fiber;
            productToBeUpdated.Sugar = product.Sugar;
            productToBeUpdated.Quantity = product.Quantity;
            productToBeUpdated.PortionQuantity = product.PortionQuantity;
            productToBeUpdated.PortionUnit = product.PortionUnit;
            _productRepository.UpdateProduct(productToBeUpdated);
            UpdateProductInExistingPlan(productToBeUpdated, userId);

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.EditedProduct, productToBeUpdated.ProductId, productToBeUpdated.ProductName, username);
            client.PostAsync("https://localhost:5001/VirtusFit/product",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

        }

        public void DeleteFromFavorites(Product favorite, string userId, string username)
        {
            var fav = _productRepository.GetProductById(favorite.ProductId);
            fav.IsFavorite = false;
            _productRepository.UpdateProduct(fav);
            _productRepository.Save();

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.ProductRemovedFromFavorites, fav.ProductId, fav.ProductName, username);
            client.PostAsync("https://localhost:5001/VirtusFit/product",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
        }

        public void AddToFavorites(Product favorite, string userId, string username)
        {
            var fav = _productRepository.GetProductById(favorite.ProductId);
            fav.IsFavorite = true;
            _productRepository.UpdateProduct(fav);
            _productRepository.Save();

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.ProductAddedToFavorites, fav.ProductId, fav.ProductName, username);
            client.PostAsync("https://localhost:5001/VirtusFit/product",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
        }

        private void UpdateProductInExistingPlan(Product productToBeUpdated, string userId)
        {
            var plans = _dietPlanRepository.ListAllDietPlans(userId);
            var dailyList = new List<DailyDietPlan>();
            foreach (var plan in plans)
            {
                var dailyListInPlan = _dietPlanRepository.ListDailyDietPlans(plan.Id);
                foreach (var daily in dailyListInPlan)
                {
                    dailyList.Add(daily);
                }
            }
            foreach (var daily in dailyList)
            {
                var listOfProductsInDailyPlans = _dietPlanRepository.ListDbProductsInDailyDietPlan(daily);
                foreach (var item in listOfProductsInDailyPlans)
                {
                    if (item.ProductId == productToBeUpdated.ProductId)
                    {
                        var oldProduct = _dietPlanRepository.GetProductFromDailyDietPlan(daily, item.OrdinalNumber);
                        oldProduct.TotalCalories = productToBeUpdated.Energy * oldProduct.PortionSize *
                            oldProduct.NumberOfPortions / 100;
                        _dietPlanRepository.UpdateProductInPlan(oldProduct);
                        _productInPlanService.CalculateDailyDietPlanCaloriesAndMacros(daily);
                    }
                }
            }

        }


        private void DeleteFromExistingPlan(Product productToBeDeleted, string userId)
        {
            var plans = _dietPlanRepository.ListAllDietPlans(userId);
            var dailyList = new List<DailyDietPlan>();
            foreach (var plan in plans)
            {
                var dailyListInPlan = _dietPlanRepository.ListDailyDietPlans(plan.Id);
                foreach (var daily in dailyListInPlan)
                {
                    dailyList.Add(daily);
                }
            }
            foreach (var daily in dailyList)
            {
                var listOfProductsInDailyPlans = _dietPlanRepository.ListDbProductsInDailyDietPlan(daily);
                foreach (var item in listOfProductsInDailyPlans)
                {
                    if (item.ProductId == productToBeDeleted.ProductId)
                    {
                        _dietPlanRepository.DeleteProductInPlan(item);
                        _productInPlanService.CalculateDailyDietPlanCaloriesAndMacros(daily);
                    }
                }

            }
        }


        public List<Product> SearchByName(string name, string userId, string username)
        {
            var client = _httpClientFactory.CreateClient();
            var action = CreateSearchStringAction(SearchActionType.SearchByName, name, username);
            client.PostAsync("https://localhost:5001/VirtusFit/search/string",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

            return _searchProductLogic.SearchByName(_productRepository.GetProducts(userId), name);
        }
        public List<Product> SearchByFat(double minfat, double maxfat, string userId, string username)
        {
            var avg = (minfat + maxfat) / 2;
            var client = _httpClientFactory.CreateClient();
            var action = CreateSearchValueAction(SearchActionType.SearchByFat, avg, username);
            client.PostAsync("https://localhost:5001/VirtusFit/search/value",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            return _searchProductLogic.SearchByFat(_productRepository.GetProducts(userId), minfat, maxfat);
        }
        public List<Product> SearchByCalories(double minenergy, double maxenergy, string userId, string username)
        {
            var avg = (minenergy + maxenergy) / 2;
            var client = _httpClientFactory.CreateClient();
            var action = CreateSearchValueAction(SearchActionType.SearchByCalories, avg, username);
            client.PostAsync("https://localhost:5001/VirtusFit/search/value",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            return _searchProductLogic.SearchByCalories(_productRepository.GetProducts(userId), minenergy, maxenergy);
        }
        public List<Product> SearchByCarbohydrates(double mincarb, double maxcarb, string userId, string username)
        {
            var avg = (mincarb + maxcarb) / 2;
            var client = _httpClientFactory.CreateClient();
            var action = CreateSearchValueAction(SearchActionType.SearchByCarbohydrates, avg, username);
            client.PostAsync("https://localhost:5001/VirtusFit/search/value",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            return _searchProductLogic.SearchByCarbohydrates(_productRepository.GetProducts(userId), mincarb, maxcarb);
        }
        public List<Product> SearchByProteins(double minprotein, double maxprotein, string userId, string username)
        {
            var avg = (minprotein + maxprotein) / 2;
            var client = _httpClientFactory.CreateClient();
            var action = CreateSearchValueAction(SearchActionType.SearchByProtein, avg, username);
            client.PostAsync("https://localhost:5001/VirtusFit/search/value",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            return _searchProductLogic.SearchByProteins(_productRepository.GetProducts(userId), minprotein, maxprotein);
        }

        private CreateProductAction CreateAction(ActionType type, int productId, string name, string username)
        {
            var action = new CreateProductAction
            {
                Username = username,
                ProductId = productId,
                ProductName = name,
                Action = type,
                Created = DateTime.UtcNow
            };

            return action;
        }

        private CreateSearchStringAction CreateSearchStringAction(SearchActionType type, string searchString, string username)
        {
            var action = new CreateSearchStringAction
            {
                Username = username,
                Created = DateTime.UtcNow,
                SearchType = type,
                SearchString = searchString
            };

            return action;
        }

        private CreateSearchValueAction CreateSearchValueAction(SearchActionType type, double avg, string username)
        {
            var action = new CreateSearchValueAction
            {
                Username = username,
                Created = DateTime.UtcNow,
                SearchType = type,
                SearchValue = avg
            };

            return action;
        }
    }
}
