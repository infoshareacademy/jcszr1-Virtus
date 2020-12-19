using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> FetchAllTimeReport()
        {
            var model = await _reportService.FetchOverallReport();
            return View(model);
        }
        public IActionResult FetchDateSpecifiedReport()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> FetchDateSpecifiedReportResult(ReportParameters parameters)
        {
            var model = await _reportService.FetchDateSpecifiedReport(parameters.StartDate, parameters.FinishDate);
            return View(model);
        }

        [HttpGet]
        public IActionResult FetchUserReport()
        {
            //var users = _reportService.ListOfUsernames();
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> FetchUserReportResult(string username)
        {
            var model = await _reportService.FetchUserReport(username);
            return View(model);
        }
    }
}
