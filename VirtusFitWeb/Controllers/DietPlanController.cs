using BLL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VirtusFitWeb.Logic;

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
            ViewBag.DietPlanId = _dietPlanService.GetDietPlan(id).Id;
            ViewBag.Calories = _dietPlanService.GetDietPlan(id).CaloriesPerDay;
            return View(_dietPlanService.ListDailyDietPlans(id));
        }

        public ActionResult DailyProductList(int id, int dayNumber)
        {
            ViewBag.DietPlanId = _dietPlanService.GetDietPlan(id).Id;
            ViewBag.DayNumber = _dietPlanService.GetDailyDietPlan(id,dayNumber).DayNumber;
            return View(_dietPlanService.ListProductsOnDailyDietPlan(id,dayNumber));
        }

        public ActionResult ProductsToAdd(int id, int dayNumber)
        {
            ViewBag.DietPlanId = id;
            ViewBag.DayNumber = dayNumber;
            return View(_dietPlanService.GetProductList());
        }

        public ActionResult AddProductToPlan(int productId, int id, int dayNumber)
        {
            ViewBag.DietPlanId = id;
            ViewBag.DayNumber = dayNumber;
            var productToAdd = _dietPlanService.GetProductToAdd(productId);
            return View(productToAdd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductToPlan(int id, int dayNumber, ProductOnDietPlan productToAdd, int productId)
        {
            if (!ModelState.IsValid)
            {
                return View(productToAdd);
            }
            try
            {
                var product = _dietPlanService.GetProductFromList(productId);
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
            return View();
        }

        // POST: DietPlanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
    }
}
