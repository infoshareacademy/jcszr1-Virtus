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
            return View(_dietPlanService.ListAllDietPlans().ToList());
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
                model.DietPlanId = dailyDietPlan.Id;
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
    }
}
