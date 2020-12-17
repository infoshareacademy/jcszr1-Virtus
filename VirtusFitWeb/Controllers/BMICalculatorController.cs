using System.Diagnostics.Tracing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
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
            throw new AdalException();

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
