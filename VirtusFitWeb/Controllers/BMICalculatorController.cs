using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtusFitWeb.Filters;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    [ExceptionFilter]
    [AllowAnonymous]
    public class BMICalculatorController : Controller
    {
        private readonly IBMICalculatorService _bmiCalculatorService;

        public BMICalculatorController(IBMICalculatorService bmiCalculatorService)
        {
            _bmiCalculatorService = bmiCalculatorService;
        }

        [HttpGet]
        public ActionResult Calculator()
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
