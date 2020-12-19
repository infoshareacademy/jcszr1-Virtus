using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Reports.Models;

namespace VirtusFitWeb.Services
{
    public interface IReportService
    {
        Task <OverallReport> FetchOverallReport();
        Task <OverallReport> FetchDateSpecifiedReport(DateTime start, DateTime finish);
        Task<UserReport> FetchUserReport(string username);
        List<string> ListOfUsernames();
    }
}
