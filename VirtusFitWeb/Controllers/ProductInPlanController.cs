using BLL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VirtusFitWeb.Models;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class ProductInPlanController : Controller
    {
        private readonly IProductInPlanService _productInPlanService;
        private readonly IDietPlanService _dietPlanService;

        public ProductInPlanController(IProductInPlanService productInPlanService, IDietPlanService dietPlanService)
        {
            _productInPlanService = productInPlanService;
            _dietPlanService = dietPlanService;
        }
        
        public ActionResult ProductsToAdd(int id, int dayNumber)
        {
            var model = new ProductsToAddViewModel()
            {
                ProductList = this._productInPlanService.GetProductList()
            };

            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DietPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
            }
            
            return View(model);
        }

        public ActionResult AddProductToPlan(int productId, int id, int dayNumber)
        {
            var model = new ProductInPlanViewModel()
            {
                ProductInPlan = _productInPlanService.GetProductToAdd(productId)
            };

            var dailyDietPlan = this._dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
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
            var dailyDietPlan = this._dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
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
                _productInPlanService.AddProductToDailyDietPlan(id, dayNumber, productToAdd, product);
                return RedirectToAction("DailyProductList", "DietPlan",
                    new { id = id, dayNumber = dayNumber });
            }
            catch
            {
                return View();
            }
        }
        
        // GET: DietPlanController/Edit/5
        public ActionResult EditProductInPlan(int id, int dayNumber, int ordinalNumber)
        {
            var model = new ProductInPlanViewModel()
            {
                ProductInPlan = _productInPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber)
            };

            var dailyDietPlan = this._dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
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
            var dailyDietPlan = this._dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                editedProductModel.DietPlanId = dailyDietPlan.DietPlanId;
                editedProductModel.DayNumber = dailyDietPlan.DayNumber;
                editedProductModel.Date = dailyDietPlan.Date.ToShortDateString();
                editedProductModel.ProductInPlan.Product = dailyDietPlan.ProductListForDay[currentProductOrdinalNumber - 1].Product;
                editedProductModel.ProductInPlan.OrdinalNumber = currentProductOrdinalNumber;
            }

            if (!ModelState.IsValid)
            {
                return View(editedProductModel);
            }
            try
            {
                var editedProduct = editedProductModel.ProductInPlan;
                _productInPlanService.EditProductInDailyDietPlan(id, dayNumber, editedProduct, currentProductOrdinalNumber);
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
            var productInPlan = this._productInPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber);
            var model = new ProductInPlanViewModel()
            {
                DietPlanId = id,
                DayNumber = dayNumber,
                ProductInPlan = productInPlan,
                Date = this._dietPlanService.GetDailyDietPlan(id,dayNumber).Date.ToShortDateString()
            };
            return View(model);
        }

        // GET: DietPlanController/DeletePlan/5
        public ActionResult DeleteProduct(int id, int dayNumber, int ordinalNumber)
        {
            var productInPlan = this._productInPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber);
            var model = new ProductInPlanViewModel()
            {
                DietPlanId = id,
                DayNumber = dayNumber,
                ProductInPlan = productInPlan,
                Date = this._dietPlanService.GetDailyDietPlan(id, dayNumber).Date.ToShortDateString()
            };
            return View(model);
        }

        // POST: DietPlanController/DeletePlan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, int dayNumber, int ordinalNumber, ProductInPlanViewModel productToDelete )
        {
            try
            {
                _productInPlanService.DeleteProductFromPlan(id, dayNumber, ordinalNumber);

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
