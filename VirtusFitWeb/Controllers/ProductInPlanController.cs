using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using VirtusFitWeb.Filters;
using VirtusFitWeb.Models;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    [ExceptionFilter]
    public class ProductInPlanController : Controller
    {
        private readonly IProductInPlanService _productInPlanService;
        private readonly IDietPlanService _dietPlanService;
        private readonly IFavoriteService _favoriteService;
        private readonly IProductService _productService;

        public string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Username => User.FindFirstValue(ClaimTypes.Name);

        public ProductInPlanController(IProductInPlanService productInPlanService, IDietPlanService dietPlanService,
            IFavoriteService favoriteService, IProductService productService)
        {
            _productInPlanService = productInPlanService;
            _dietPlanService = dietPlanService;
            _favoriteService = favoriteService;
            _productService = productService;
        }
        
        public ActionResult ProductsToAdd(int id, int dayNumber)
        {
            var model = new ProductsToAddViewModel()
            {
                ProductList = _productInPlanService.GetProductList(UserId)
            };

            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo;
                model.DietPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
                model.Date = dailyDietPlan.Date.ToShortDateString();
            }

            var dietPlan = _dietPlanService.GetDietPlan(id);
            if (dietPlan != null)
            {
                model.CaloriesPerDay = dietPlan.CaloriesPerDay;
            }

            return View(model);
        }
        
        public IActionResult AddToFavorites(int productId, int planId, int dayNumber)
        {
            var favorite = _productService.GetById(productId);
            _favoriteService.AddToFavorites(favorite, UserId, Username);
            return RedirectToAction("ProductsToAdd", "productInPlan",
                new { id = planId, dayNumber = dayNumber });
        }

        
        public IActionResult DeleteFromFavorites(int productId, int planId, int dayNumber)
        {
            var favorite = _productService.GetById(productId);
            _favoriteService.DeleteFromFavorites(favorite, UserId, Username);
            return RedirectToAction("ProductsToAdd", "productInPlan",
                new { id = planId, dayNumber = dayNumber });
        }
        public ActionResult FavoritesToAdd(int id, int dayNumber)
        {
            var model = new ProductsToAddViewModel()
            {
                ProductList = _favoriteService.GetAll(UserId)
            };

            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo;
                model.DietPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
                model.Date = dailyDietPlan.Date.ToShortDateString();
            }

            var dietPlan = _dietPlanService.GetDietPlan(id);
            if (dietPlan != null)
            {
                model.CaloriesPerDay = dietPlan.CaloriesPerDay;
            }

            return View(model);
        }

        public ActionResult AddProductToPlan(int productId, int id, int dayNumber)
        {
            var model = new ProductInPlanViewModel()
            {
                ProductInPlan = _productInPlanService.GetProductToAdd(productId)
            };

            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo;
                model.DietPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
                model.Date = dailyDietPlan.Date.ToShortDateString();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductToPlan(int id, int dayNumber, ProductInPlanViewModel productInPlanModel, int productId)
        {
            var user = UserId;
            var username = Username;
            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                productInPlanModel.DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo;
                productInPlanModel.DietPlanId = dailyDietPlan.DietPlanId;
                productInPlanModel.DayNumber = dailyDietPlan.DayNumber;
                productInPlanModel.Date = dailyDietPlan.Date.ToShortDateString();
                productInPlanModel.ProductInPlan.Product = _productInPlanService.GetProductFromList(productId);
            }
            if (!ModelState.IsValid)
            {
                return View(productInPlanModel);
            }
            try
            {
                var product = _productInPlanService.GetProductFromList(productId);
                var productToAdd = productInPlanModel.ProductInPlan;
                _productInPlanService.AddProductToDailyDietPlan(id, dayNumber, productToAdd, product, user, username);
                return RedirectToAction("DailyProductList", "DietPlan",
                    new { id = id, dayNumber = dayNumber });
            }
            catch
            {
                return View(productInPlanModel);
            }
        }
        
        // GET: DietPlanController/Edit/5
        public ActionResult EditProductInPlan(int id, int dayNumber, int ordinalNumber)
        {
            var model = new ProductInPlanViewModel()
            {
                ProductInPlan = _productInPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber)
            };

            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo;
                model.DietPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
                model.Date = dailyDietPlan.Date.ToShortDateString();
            }
            
            return View(model);
        }

        // POST: DietPlanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductInPlan(int id, int dayNumber, ProductInPlanViewModel editedProductModel,
            int currentProductOrdinalNumber)
        {
            var user = UserId;
            var username = Username;
            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            var dailyDietPlansProduct = _dietPlanService.ListProductsOnDailyDietPlan(id, dayNumber)
                .FirstOrDefault(x => x.OrdinalNumber == currentProductOrdinalNumber);
            if (dailyDietPlansProduct == null) return View(editedProductModel);
            if (dailyDietPlan != null)
            {
                editedProductModel.DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo;
                editedProductModel.DietPlanId = dailyDietPlan.DietPlanId;
                editedProductModel.DayNumber = dailyDietPlan.DayNumber;
                editedProductModel.Date = dailyDietPlan.Date.ToShortDateString();
                editedProductModel.ProductInPlan.Product = dailyDietPlansProduct.Product;
                editedProductModel.ProductInPlan.OrdinalNumber = currentProductOrdinalNumber;
            }

            if (!ModelState.IsValid)
            {
                return View(editedProductModel);
            }
            try
            {
                var editedProduct = editedProductModel.ProductInPlan;
                _productInPlanService.EditProductInDailyDietPlan(id, dayNumber, editedProduct, currentProductOrdinalNumber, user, username);
                return RedirectToAction("DailyProductList", "DietPlan",
                    new { id, dayNumber });
            }
            catch
            {
                return View(editedProductModel);
            }
        }

        public ActionResult ProductDetails(int id, int dayNumber, int ordinalNumber)
        {
            var productInPlan = _productInPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber);
            var model = new ProductInPlanViewModel()
            {
                DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo,
                DietPlanId = id,
                DayNumber = dayNumber,
                ProductInPlan = productInPlan,
                Date = _dietPlanService.GetDailyDietPlan(id,dayNumber).Date.ToShortDateString()
            };
            return View(model);
        }

        // GET: DietPlanController/DeletePlan/5
        public ActionResult DeleteProduct(int id, int dayNumber, int ordinalNumber)
        {
            var productInPlan = _productInPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber);
            var model = new ProductInPlanViewModel()
            {
                DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo,
                DietPlanId = id,
                DayNumber = dayNumber,
                ProductInPlan = productInPlan,
                Date = _dietPlanService.GetDailyDietPlan(id, dayNumber).Date.ToShortDateString()
            };
            return View(model);
        }

        // POST: DietPlanController/DeletePlan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, int dayNumber, int ordinalNumber, ProductInPlanViewModel productToDelete )
        {
            var user = UserId;
            var username = Username;
            try
            {
                _productInPlanService.DeleteProductFromPlan(id, dayNumber, ordinalNumber, user, username);

                return RedirectToAction("DailyProductList", "DietPlan",
                    new { id, dayNumber });
            }
            catch
            {
                return View(productToDelete);
            }
        }
    }
}
