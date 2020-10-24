using BLL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public ProductService(IProductRepository productRepository, IDietPlanRepository dietPlanRepository, IProductInPlanService productInPlanService)
        {
            _productRepository = productRepository;
            _dietPlanRepository = dietPlanRepository;
            _productInPlanService = productInPlanService;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetProducts();
        }

        public Product GetById(int id)
        { 
            return _productRepository.GetProductById(id);
        }

        public void DeleteById(int id)
        {
            var product = GetById(id);
            DeleteFromExistingPlan(product);
            _productRepository.DeleteProduct(product);
        }

        public Product Create(Product newProduct)
        {
            _productRepository.InsertProduct(newProduct);
            return newProduct;
        }

        public void Update(int id, Product product)
        {
                var productToBeUpdated = GetById(id);
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
                productToBeUpdated.IsFavourite = false;
                _productRepository.UpdateProduct(productToBeUpdated);
        }

        public void DeleteFromFavorites(Product favorite)
        {
                var fav = _productRepository.GetProductById(favorite.ProductId);
                fav.IsFavourite = false;
                _productRepository.UpdateProduct(fav);
                _productRepository.Save();
        }

        public void AddToFavorites(Product favorite)
        {
                var fav = _productRepository.GetProductById(favorite.ProductId);
                fav.IsFavourite = true;
                _productRepository.UpdateProduct(fav);
                _productRepository.Save();
        }

        private void DeleteFromExistingPlan(Product productToBeDeleted)
        {
            var plans = _dietPlanRepository.ListAllDietPlans();
            var dailyLists = new List<DailyDietPlan>();
            foreach (var plan in plans)
            {
                var i = 1;
                while (i <= plan.DailyDietPlanList.Count)
                {
                    dailyLists.Add(_dietPlanRepository.GetDailyDietPlan(plan.Id, i));
                    i++;
                }
            }
            foreach (var dailyList in dailyLists)
            {
                var listOfProductsInDailyPlans = _dietPlanRepository.ListDbProductsInDailyDietPlan(dailyList);
                foreach (var item in listOfProductsInDailyPlans)
                {
                    if (item.ProductId == productToBeDeleted.ProductId)
                    {
                        _dietPlanRepository.DeleteProductInPlan(item);
                        _productInPlanService.CalculateDailyDietPlanCaloriesAndMacros(dailyList);
                    }
                }

            }
        }


        public List<Product> SearchByName(string name)
        {
            return _searchProductLogic.SearchByName(_productRepository.GetProducts(), name);
        }
        public List<Product> SearchByFat(double minfat, double maxfat)
        {
            return _searchProductLogic.SearchByFat(_productRepository.GetProducts(), minfat, maxfat);
        }
        public List<Product> SearchByCalories(double minenergy, double maxenergy)
        {
            return _searchProductLogic.SearchByCalories(_productRepository.GetProducts(), minenergy, maxenergy);
        }
        public List<Product> SearchByCarbohydrates(double mincarb, double maxcarb)
        {
            return _searchProductLogic.SearchByCarbohydrates(_productRepository.GetProducts(), mincarb, maxcarb);
        }
        public List<Product> SearchByProteins(double minprotein, double maxprotein)
        {
            return _searchProductLogic.SearchByProteins(_productRepository.GetProducts(), minprotein, maxprotein);
        }
    }
}
