using BLL;
using System.Collections.Generic;
using System.Linq;
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

        public List<Product> GetAll(string userId)
        {
            return _productRepository.GetProducts(userId);
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

        public Product Create(Product newProduct, string userId)
        {
            newProduct.ProductNo = GetAll(userId).Max(p => p.ProductNo) + 1;
            _productRepository.InsertProduct(newProduct);
            return newProduct;
        }

        public void Update(int id, Product product)
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
            UpdateProductInExistingPlan(productToBeUpdated);

        }

        public void DeleteFromFavorites(Product favorite)
        {
            var fav = _productRepository.GetProductById(favorite.ProductId);
            fav.IsFavorite = false;
            _productRepository.UpdateProduct(fav);
            _productRepository.Save();
        }

        public void AddToFavorites(Product favorite)
        {
            var fav = _productRepository.GetProductById(favorite.ProductId);
            fav.IsFavorite = true;
            _productRepository.UpdateProduct(fav);
            _productRepository.Save();
        }

        private void UpdateProductInExistingPlan(Product productToBeUpdated)
        {
            var plans = _dietPlanRepository.ListAllDietPlans();
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


        private void DeleteFromExistingPlan(Product productToBeDeleted)
        {
            var plans = _dietPlanRepository.ListAllDietPlans();
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


        public List<Product> SearchByName(string name, string userId)
        {
            return _searchProductLogic.SearchByName(_productRepository.GetProducts(userId), name);
        }
        public List<Product> SearchByFat(double minfat, double maxfat, string userId)
        {
            return _searchProductLogic.SearchByFat(_productRepository.GetProducts(userId), minfat, maxfat);
        }
        public List<Product> SearchByCalories(double minenergy, double maxenergy, string userId)
        {
            return _searchProductLogic.SearchByCalories(_productRepository.GetProducts(userId), minenergy, maxenergy);
        }
        public List<Product> SearchByCarbohydrates(double mincarb, double maxcarb, string userId)
        {
            return _searchProductLogic.SearchByCarbohydrates(_productRepository.GetProducts(userId), mincarb, maxcarb);
        }
        public List<Product> SearchByProteins(double minprotein, double maxprotein, string userId)
        {
            return _searchProductLogic.SearchByProteins(_productRepository.GetProducts(userId), minprotein, maxprotein);
        }
    }
}
