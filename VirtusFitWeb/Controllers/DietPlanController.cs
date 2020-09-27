using BLL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VirtusFitWeb.Models;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class DietPlanController : Controller
    {
        private readonly IDietPlanService _dietPlanService;
        private readonly DateValidator _validator = new DateValidator();

        public DietPlanController(IDietPlanService dietPlanService)
        {
            _dietPlanService = dietPlanService;
        }
        // GET: DietPlanController
        public ActionResult Index()
        {
            return View(_dietPlanService.ListAll().ToList());
        }

        // GET: DietPlanController/DayList/5
        public ActionResult DayList(int id)
        {
            var model = new PlanDetailsViewModel
            {
                DailyDietPlans = this._dietPlanService.ListDailyDietPlans(id)
            };

            var dietPlan = _dietPlanService.GetDietPlan(id);
            if (dietPlan != null)
            {
                model.DietPlanId = dietPlan.Id;
                model.PlanCaloriesPerDay = dietPlan.CaloriesPerDay;
            }

            return View(model);
        }

        public ActionResult DailyProductList(int id, int dayNumber)
        {
            var model = new DailyProductListViewModel()
            {
                ProductListForDay = this._dietPlanService.ListProductsOnDailyDietPlan(id, dayNumber)
            };

            var dailyDietPlan = this._dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DietPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
                model.TotalCalories = dailyDietPlan.CaloriesSum;
                model.Date = dailyDietPlan.Date.ToShortDateString();
            }

            var dietPlan = this._dietPlanService.GetDietPlan(id);
            if (dietPlan != null)
            {
                model.CaloriesPerDay = dietPlan.CaloriesPerDay;
            }
            
            return View(model);
        }

        public ActionResult ProductsToAdd(int id, int dayNumber)
        {
            var model = new ProductsToAddViewModel()
            {
                ProductList = this._dietPlanService.GetProductList()
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
                ProductInPlan = _dietPlanService.GetProductToAdd(productId)
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
                productInPlanModel.ProductInPlan.Product = _dietPlanService.GetProductFromList(productId);
            }
            if (!ModelState.IsValid)
            {
                return View(productInPlanModel);
            }
            try
            {
                var product = _dietPlanService.GetProductFromList(productId);
                var productToAdd = productInPlanModel.ProductInPlan;
                _dietPlanService.AddProductToDailyDietPlan(id, dayNumber, productToAdd, product);
                return RedirectToAction("DailyProductList", "DietPlan",
                    new { id = id, dayNumber = dayNumber });
            }
            catch
            {
                return View();
            }
        }

        // GET: DietPlanController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DietPlanController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DietPlan newDietPlan)
        {
            var dateValidation = _validator.Validate(newDietPlan);
            dateValidation.AddToModelState(ModelState, null);

            if (!dateValidation.IsValid)
            {
                return View(newDietPlan);
            }

            if (!ModelState.IsValid)
            {
                return View(newDietPlan);
            }

            try
            {
                _dietPlanService.Create(newDietPlan);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DietPlanController/Edit/5
        public ActionResult Edit(int id)
        {
            var dietPlanToEdit = _dietPlanService.GetDietPlan(id);
            return View(dietPlanToEdit);
        }

        // POST: DietPlanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DietPlan dietPlan)
        {
            var dateValidation = _validator.Validate(dietPlan);
            dateValidation.AddToModelState(ModelState, null);

            if (!dateValidation.IsValid)
            {
                return View(dietPlan);
            }

            if (!ModelState.IsValid)
            {
                return View(dietPlan);
            }

            try
            {
                _dietPlanService.Edit(id, dietPlan);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(dietPlan);
            }
        }

        // GET: DietPlanController/DeletePlan/5
        public ActionResult DeletePlan(int id)
        {
            var dietPlan = _dietPlanService.GetDietPlan(id);
            return View(dietPlan);
        }

        // POST: DietPlanController/DeletePlan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlan(int id, DietPlan dietPlanToDelete)
        {
            try
            {
                _dietPlanService.DeleteById(id);
                return RedirectToAction(nameof(Index));
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
                ProductInPlan = _dietPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber)
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
                _dietPlanService.EditProductInDailyDietPlan(id, dayNumber, editedProduct, currentProductOrdinalNumber);
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
            var productInPlan = this._dietPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber);
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
            var productInPlan = this._dietPlanService.GetProductFromDietPlan(id, dayNumber, ordinalNumber);
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
                _dietPlanService.DeleteProductFromPlan(id, dayNumber, ordinalNumber);

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
