﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtusFitApi.Reports;
using VirtusFitApi.Reports.Models;

namespace VirtusFitApi.Controllers
{
    [Route("VirtusFit")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportBuilder _builder;

        public ReportController(IReportBuilder reportBuilder)
        {
            _builder = reportBuilder;
        }

        [HttpGet("report/overall")]
        public ActionResult<OverallReport> OverallReport()
        {
            var report = _builder.CreateOverallReport();
            return Accepted(report);
        }

        [HttpGet("report/daily")]
        public ActionResult<OverallReport> DailyReport([FromHeader]string start,[FromHeader]string finish)
        {

            var report = _builder.CreateDailyReport(Convert.ToDateTime(start), Convert.ToDateTime(finish));
            return Accepted(report);
        }

        [HttpGet("report/user/{username}")]
        public ActionResult<UserReport> UserReport(string username)
        {
            var report = _builder.CreateUserReport(username);
            return Accepted(report);
        }
    }
}
