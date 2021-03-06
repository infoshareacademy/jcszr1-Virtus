﻿using BLL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using VirtusFitWeb.Filters;
using VirtusFitWeb.Models;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    [ExceptionFilter]
    public class DietPlanController : Controller
    {
        private readonly IDietPlanService _dietPlanService;
        private readonly DateValidator _validator = new DateValidator();

        public string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Username => User.FindFirstValue(ClaimTypes.Name);

        public DietPlanController(IDietPlanService dietPlanService)
        {
            _dietPlanService = dietPlanService;
        }

        // GET: DietPlanController
        public ActionResult Index()
        {
            return View(_dietPlanService.ListAllDietPlans(UserId).ToList());
        }

        // GET: DietPlanController/DayList/5
        public ActionResult DayList(int id)
        {
            var model = new PlanDetailsViewModel
            {
                DailyDietPlans = _dietPlanService.ListDailyDietPlans(id)
            };

            var dietPlan = _dietPlanService.GetDietPlan(id);
            if (dietPlan != null)
            {
                model.DietPlanNo = dietPlan.PlanNo;
                model.DietPlanId = dietPlan.Id;
                model.PlanCaloriesPerDay = dietPlan.CaloriesPerDay;
            }

            return View(model);
        }

        public ActionResult DailyProductList(int id, int dayNumber)
        {
            var model = new DailyProductListViewModel()
            {
                ProductListForDay = _dietPlanService.ListProductsOnDailyDietPlan(id, dayNumber)
            };

            var dailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            if (dailyDietPlan != null)
            {
                model.DietPlanNo = _dietPlanService.GetDietPlan(id).PlanNo;
                model.DietPlanId = dailyDietPlan.DietPlanId;
                model.DayNumber = dailyDietPlan.DayNumber;
                model.TotalCalories = dailyDietPlan.CaloriesSum;
                model.Date = dailyDietPlan.Date.ToShortDateString();
            }

            var dietPlan = _dietPlanService.GetDietPlan(id);
            if (dietPlan != null)
            {
                model.CaloriesPerDay = dietPlan.CaloriesPerDay;
            }
            
            return View(model);
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
                var username = Username;
                newDietPlan.UserId = UserId;
                _dietPlanService.Create(newDietPlan, username);
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
                var username = Username;
                _dietPlanService.Edit(id, dietPlan, username);
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
                var userId = UserId;
                var username = Username;

                _dietPlanService.DeleteById(id, userId, username);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
