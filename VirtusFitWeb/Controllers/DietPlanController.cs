using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VirtusFitWeb.Models;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class DietPlanController : Controller
    {
        private readonly IDietPlanService _dietPlanService;

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
            ViewBag.DailyDietPlan = _dietPlanService.GetDailyDietPlan(id, dayNumber);
            return View(_dietPlanService.GetProductList());
        }

        public ActionResult AddProductToPlan(int productId)
        {
            var productToAdd = _dietPlanService.GetProductToAdd(productId);
            return View(productToAdd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductToPlan(DailyDietPlan dailyDietPlan, ProductOnDietPlan productToAdd)
        {
            if (!ModelState.IsValid)
            {
                return View(productToAdd);
            }
            try
            {
                _dietPlanService.AddProductToDailyDietPlan(dailyDietPlan, productToAdd);
                return RedirectToAction(nameof(DailyProductList));
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
        public ActionResult Create(IFormCollection collection)
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

        // GET: DietPlanController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DietPlanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
