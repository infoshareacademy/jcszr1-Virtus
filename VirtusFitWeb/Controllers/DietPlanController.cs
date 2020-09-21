﻿using BLL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VirtusFitWeb.Logic;
using VirtusFitWeb.Models;

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

        // GET: DietPlanController/Details/5
        public ActionResult Details(int id)
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

            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DailyPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
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
            var model = new AddProductToPlanViewModel()
            {
                ProductToAdd = _dietPlanService.GetProductToAdd(productId)
            };

            var dailyDietPlan = this._dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DietPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductToPlan(int id, int dayNumber, AddProductToPlanViewModel productToAddToPlan, int productId)
        {
            if (!ModelState.IsValid)
            {
                return View(productToAddToPlan);
            }
            try
            {
                var product = _dietPlanService.GetProductFromList(productId);
                var productToAdd = productToAddToPlan.ProductToAdd;
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

        // GET: DietPlanController/Delete/5
        public ActionResult Delete(int id)
        {
            var dietPlan = _dietPlanService.GetDietPlan(id);
            return View(dietPlan);
        }

        // POST: DietPlanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DietPlan dietPlanToDelete)
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

    }
}
