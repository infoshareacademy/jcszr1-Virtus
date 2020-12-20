using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Reports.Models;

namespace VirtusFitApi.Reports
{
    public interface IReportBuilder
    {
        public OverallReport CreateOverallReport();
        public OverallReport CreateDailyReport(DateTime start, DateTime finish);
        public UserReport CreateUserReport(string id);
    }
}
