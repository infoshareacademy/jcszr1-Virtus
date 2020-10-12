using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.AspNetCore.Mvc;
using VirtusFitWeb.Models;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class BMICalculatorController : Controller
    {
        private readonly IBMICalculatorService _bmiCalculatorService;

        public BMICalculatorController(IBMICalculatorService bmiCalculatorService)
        {
            _bmiCalculatorService = bmiCalculatorService;
        }

        [HttpGet]
        public ActionResult Calculator ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BmiResult(double height, double weight)
        {
            double bmi = _bmiCalculatorService.CalculateBMI(height, weight);
            return View(bmi);
        }

    }
}
