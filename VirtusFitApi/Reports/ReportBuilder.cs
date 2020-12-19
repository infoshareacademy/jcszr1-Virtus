using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;
using VirtusFitApi.DAL;
using VirtusFitApi.Models;
using VirtusFitApi.Reports.Models;

namespace VirtusFitApi.Reports
{
    public class ReportBuilder : IReportBuilder
    {
        private readonly IPlanActionsRepository _planActionsRepository;
        private readonly IProductActionsRepository _productActionsRepository;
        private readonly IUserAccountActionsRepository _userAccountActionsRepository;

        public ReportBuilder(IPlanActionsRepository planActionsRepository,
            IProductActionsRepository productActionsRepository,
            IUserAccountActionsRepository userAccountActionsRepository)
        {
            _productActionsRepository = productActionsRepository;
            _planActionsRepository = planActionsRepository;
            _userAccountActionsRepository = userAccountActionsRepository;
        }

        public OverallReport CreateOverallReport()
        {
            var report = new OverallReport();

            if (_userAccountActionsRepository
                .GetAllUserAccountActions().Any(accountAction =>
                    accountAction.ActionType == UserAccountActionType.AccountCreated) == true)
            {
                report.CreatedUserAccounts = _userAccountActionsRepository.GetAllUserAccountActions()
                    .Where(accountAction => accountAction.ActionType == UserAccountActionType.AccountCreated).ToList()
                    .Count;
            }

            if (_userAccountActionsRepository
                    .GetAllUserAccountActions()
                    .Any(accountAction => accountAction.ActionType == UserAccountActionType.SuccessfulLogonAttempt) ==
                true)
            {
                report.TotalLogonCount = _userAccountActionsRepository.GetAllUserAccountActions()
                    .Where(accountAction => accountAction.ActionType == UserAccountActionType.SuccessfulLogonAttempt)
                    .ToList().Count;
            }

            if (_productActionsRepository
                    .GetAllProductActions().Any(productAction => productAction.Action == ActionType.AddedNewProduct) ==
                true)
            {
                report.AddedProducts = _productActionsRepository.GetAllProductActions()
                    .Where(productAction => productAction.Action == ActionType.AddedNewProduct).ToList().Count;
            }

            if (_productActionsRepository.GetAllProductActions()
                .Where(productAction => productAction.Action == ActionType.RemovedProduct).ToList().Any() == true)
            {
                report.RemovedProducts = _productActionsRepository.GetAllProductActions()
                    .Where(productAction => productAction.Action == ActionType.RemovedProduct).ToList().Count;
            }

            if (_planActionsRepository.GetAllDietPlanActions()
                .ToList().Any() == true)
            {
                report.AddedPlans = _planActionsRepository.GetAllDietPlanActions()
                    .ToList().Count;
            }

            report.ProductsAddedToFav = _productActionsRepository
                .GetAllProductActions()
                .Count(productAction => productAction.Action == ActionType.ProductAddedToFavorites);

            report.ProductsRemovedFromFav = _productActionsRepository
                .GetAllProductActions()
                .Count(productAction => productAction.Action == ActionType.ProductRemovedFromFavorites);

            report.ProductsAddedToPlans = _planActionsRepository
                .GetAllProductInPlanActions()
                .Count(productInPlanAction => productInPlanAction.Action == ActionType.AddedProductToExistingDailyPlan);

            report.SearchesDone = _productActionsRepository
                                      .GetAllSearchValueActions().Count() +
                                  _productActionsRepository
                                      .GetAllSearchStringActions().Count();


            if (_productActionsRepository
                .GetAllSearchStringActions().Any())
            {
                report.TopStringSearch = _productActionsRepository.GetAllSearchStringActions()
                    .GroupBy(x => x.SearchString)
                    .Select(x => new {SearchString = x.Key, TimesAppeared = x.Count()}).ToList()
                    .OrderByDescending(x => x.TimesAppeared).First().SearchString;
            }

            if (_productActionsRepository
                .GetAllSearchValueActions()
                .Any(action => action.SearchType == SearchActionType.SearchByCalories) == true)
            {
                report.AvgCaloriesSearch = _productActionsRepository.GetAllSearchValueActions()
                    .Where(action => action.SearchType == SearchActionType.SearchByCalories).ToList()
                    .Average(value => value.SearchValue);
            }

            if (_productActionsRepository
                .GetAllSearchValueActions()
                .Any(action => action.SearchType == SearchActionType.SearchByCarbohydrates) == true)
            {
                report.AvgCarbohydratesSearch = _productActionsRepository.GetAllSearchValueActions()
                    .Where(action => action.SearchType == SearchActionType.SearchByCarbohydrates).ToList()
                    .Average(value => value.SearchValue);
            }

            if (_productActionsRepository
                .GetAllSearchValueActions()
                .Any(action => action.SearchType == SearchActionType.SearchByFat) == true)
            {
                report.AvgFatSearch = _productActionsRepository.GetAllSearchValueActions()
                    .Where(action => action.SearchType == SearchActionType.SearchByFat).ToList()
                    .Average(value => value.SearchValue);
            }

            if (_productActionsRepository
                .GetAllSearchValueActions()
                .Any(action => action.SearchType == SearchActionType.SearchByProtein) == true)
            {
                report.AvgProteinsSearch = _productActionsRepository.GetAllSearchValueActions()
                    .Where(action => action.SearchType == SearchActionType.SearchByProtein).ToList()
                    .Average(value => value.SearchValue);
            }

            if (_productActionsRepository
                .GetAllProductActions()
                .Any(action => action.Action == ActionType.ProductAddedToFavorites) == true)
            {
                report.TopFavId = _productActionsRepository.GetAllProductActions()
                    .Where(action => action.Action == ActionType.ProductAddedToFavorites)
                    .GroupBy(action => action.ProductId)
                    .Select(x => new {ProductId = x.Key, TimesAppeared = x.Count()}).ToList()
                    .OrderByDescending(x => x.TimesAppeared).First().ProductId;
            }

            if (_planActionsRepository
                .GetAllBmiActions().Any() == true)
            {
                report.AvgUserBmi = _planActionsRepository.GetAllBmiActions()
                    .ToList().Average(bmi => bmi.Bmi);
            }

            if (_planActionsRepository
                .GetAllDietPlanActions()
                .Any(action => action.Action == ActionType.AddedDietPlan) == true)
            {
                report.AvgPlanLength = _planActionsRepository.GetAllDietPlanActions()
                    .Where(action => action.Action == ActionType.AddedDietPlan).ToList()
                    .Average(action => action.Length);
            }

            if (_planActionsRepository
                .GetAllDietPlanActions()
                .Any(action => action.Action == ActionType.AddedDietPlan) == true)
            {
                report.AvgPlanCalories = _planActionsRepository.GetAllDietPlanActions()
                    .Where(action => action.Action == ActionType.AddedDietPlan).ToList()
                    .Average(action => action.CaloriesPerDay);
            }

            return report;
        }

        public OverallReport CreateDailyReport(DateTime start, DateTime finish)
        {

            var report = new OverallReport();

            if (_userAccountActionsRepository
                .GetAllUserAccountActions()
                .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date).Any(accountAction => accountAction.ActionType == UserAccountActionType.AccountCreated) == true)
            {
                report.CreatedUserAccounts = _userAccountActionsRepository.GetAllUserAccountActions()
                    .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                    .Where(accountAction => accountAction.ActionType == UserAccountActionType.AccountCreated).ToList().Count;
            }

            if (_userAccountActionsRepository
                .GetAllUserAccountActions()
                .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                .Any(accountAction => accountAction.ActionType == UserAccountActionType.SuccessfulLogonAttempt) == true)
            {
                report.TotalLogonCount = _userAccountActionsRepository.GetAllUserAccountActions()
                    .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                    .Where(accountAction => accountAction.ActionType == UserAccountActionType.SuccessfulLogonAttempt).ToList().Count;
            }

            if (_productActionsRepository
                .GetAllProductActions()
                .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date).Any(productAction => productAction.Action == ActionType.AddedNewProduct)==true)
            {
                report.AddedProducts = _productActionsRepository.GetAllProductActions()
                    .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                    .Where(productAction => productAction.Action == ActionType.AddedNewProduct).ToList().Count;
            }

            if (_productActionsRepository.GetAllProductActions()
                .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                .Where(productAction => productAction.Action == ActionType.RemovedProduct).ToList().Any() == true)
            {
                report.RemovedProducts = _productActionsRepository.GetAllProductActions()
                    .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                    .Where(productAction => productAction.Action == ActionType.RemovedProduct).ToList().Count;
            }

            if (_planActionsRepository.GetAllDietPlanActions()
                .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                .ToList().Any() == true)
            {
                report.AddedPlans = _planActionsRepository.GetAllDietPlanActions()
                    .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                    .ToList().Count;
            }

            report.ProductsAddedToFav = _productActionsRepository
                .GetAllProductActions().Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                .Count(productAction => productAction.Action == ActionType.ProductAddedToFavorites);

            report.ProductsRemovedFromFav = _productActionsRepository
                .GetAllProductActions()
                .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                .Count(productAction => productAction.Action == ActionType.ProductRemovedFromFavorites);

            report.ProductsAddedToPlans = _planActionsRepository
                .GetAllProductInPlanActions()
                .Where(actions => actions.Created.Date >= start.Date && actions.Created.Date <= finish.Date)
                .Count(productInPlanAction => productInPlanAction.Action == ActionType.AddedProductToExistingDailyPlan);

            report.SearchesDone = _productActionsRepository
                                      .GetAllSearchValueActions().Count(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date) +
                                  _productActionsRepository
                                      .GetAllSearchStringActions().Count(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date);
            

            if (_productActionsRepository
                .GetAllSearchStringActions().Any(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date)==true)
            {
                report.TopStringSearch = _productActionsRepository.GetAllSearchStringActions()
                    .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date)
                    .GroupBy(x => x.SearchString)
                    .Select(x => new { SearchString = x.Key, TimesAppeared = x.Count() }).ToList()
                    .OrderByDescending(x => x.TimesAppeared).First().SearchString;
            }

            if (_productActionsRepository
                .GetAllSearchValueActions()
                .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date).Any(action => action.SearchType == SearchActionType.SearchByCalories) == true)
            {
                report.AvgCaloriesSearch = _productActionsRepository.GetAllSearchValueActions()
                    .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date)
                    .Where(action => action.SearchType == SearchActionType.SearchByCalories).ToList()
                    .Average(value => value.SearchValue);
            }

            if (_productActionsRepository
                .GetAllSearchValueActions()
                .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date).Any(action => action.SearchType == SearchActionType.SearchByCarbohydrates)==true)
            {
                report.AvgCarbohydratesSearch = _productActionsRepository.GetAllSearchValueActions()
                    .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date)
                    .Where(action => action.SearchType == SearchActionType.SearchByCarbohydrates).ToList()
                    .Average(value => value.SearchValue);
            }

            if (_productActionsRepository
                .GetAllSearchValueActions()
                .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date).Any(action => action.SearchType == SearchActionType.SearchByFat)==true)
            {
                report.AvgFatSearch = _productActionsRepository.GetAllSearchValueActions()
                    .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date)
                    .Where(action => action.SearchType == SearchActionType.SearchByFat).ToList()
                    .Average(value => value.SearchValue);
            }

            if (_productActionsRepository
                .GetAllSearchValueActions()
                .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date).Any(action => action.SearchType == SearchActionType.SearchByProtein)==true)
            {
                report.AvgProteinsSearch = _productActionsRepository.GetAllSearchValueActions()
                    .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date)
                    .Where(action => action.SearchType == SearchActionType.SearchByProtein).ToList()
                    .Average(value => value.SearchValue);
            }

            if (_productActionsRepository
                .GetAllProductActions()
                .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date).Any(action => action.Action == ActionType.ProductAddedToFavorites)==true)
            {
                report.TopFavId = _productActionsRepository.GetAllProductActions()
                    .Where(search => search.Created.Date >= start.Date && search.Created.Date <= finish.Date)
                    .Where(action => action.Action == ActionType.ProductAddedToFavorites)
                    .GroupBy(action => action.ProductId)
                    .Select(x => new { ProductId = x.Key, TimesAppeared = x.Count() }).ToList()
                    .OrderByDescending(x => x.TimesAppeared).First().ProductId;
            }

            if (_planActionsRepository
                .GetAllBmiActions().Any(bmi => bmi.Created.Date >= start.Date && bmi.Created.Date <= finish.Date)==true)
            {
                report.AvgUserBmi = _planActionsRepository.GetAllBmiActions()
                    .Where(bmi => bmi.Created.Date >= start.Date && bmi.Created.Date <= finish.Date)
                    .ToList().Average(bmi => bmi.Bmi);
            }

            if (_planActionsRepository
                .GetAllDietPlanActions()
                .Where(action => action.Created.Date >= start.Date && action.Created.Date <= finish.Date).Any(action => action.Action == ActionType.AddedDietPlan)==true)
            {
                report.AvgPlanLength = _planActionsRepository.GetAllDietPlanActions()
                    .Where(action => action.Created.Date >= start.Date && action.Created.Date <= finish.Date)
                    .Where(action => action.Action == ActionType.AddedDietPlan).ToList().Average(action => action.Length);
            }

            if (_planActionsRepository
                .GetAllDietPlanActions()
                .Where(action => action.Created.Date >= start.Date && action.Created.Date <= finish.Date).Any(action => action.Action == ActionType.AddedDietPlan)==true)
            {
                report.AvgPlanCalories = _planActionsRepository.GetAllDietPlanActions()
                    .Where(action => action.Created.Date >= start.Date && action.Created.Date <= finish.Date)
                    .Where(action => action.Action == ActionType.AddedDietPlan).ToList()
                    .Average(action => action.CaloriesPerDay);
            }

            return report;
        }

        public UserReport CreateUserReport(string username)
        {
            var report = new UserReport();

            report.Created = _userAccountActionsRepository.GetAllUserAccountActionsById(username)
                .Where(action => action.ActionType == UserAccountActionType.AccountCreated)
                .Select(action => action.Created).FirstOrDefault();

            report.Username = username;


            if (_userAccountActionsRepository
                .GetAllUserAccountActionsById(username).Any(action => action.ActionType == UserAccountActionType.SuccessfulLogonAttempt))
            {
                report.LastLogon = _userAccountActionsRepository.GetAllUserAccountActionsById(username)
                    .Where(action => action.ActionType == UserAccountActionType.SuccessfulLogonAttempt)
                    .Select(action => action.Created).Last();
            }

            report.TotalLogonCount = _userAccountActionsRepository.GetAllUserAccountActionsById(username)
                .Where(action => action.ActionType == UserAccountActionType.SuccessfulLogonAttempt)
                .ToList().Count;

            report.State = GetAccountStatus(username);

            report.LastPasswordChange = _userAccountActionsRepository
                .GetAllUserAccountActionsById(username).Last(action => action.ActionType == UserAccountActionType.PasswordChanged).Created;

            report.TotalAddedProducts = _productActionsRepository.GetAllProductActions()
                .Where(action => action.Username == username)
                .Where(action => action.Action == ActionType.AddedNewProduct)
                .ToList().Count;

            report.TotalAddedPlans = _planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Action == ActionType.AddedDietPlan)
                .Where(action => action.Username == username).ToList().Count;

            if (_planActionsRepository.GetAllProductInPlanActions()
                .Where(action => action.Username == username)
                .Where(action => action.Action == ActionType.AddedProductToExistingDailyPlan).Any() == true)
            {
                report.MostUsedProduct = _planActionsRepository.GetAllProductInPlanActions()
                    .Where(action => action.Username == username)
                    .Where(action => action.Action == ActionType.AddedProductToExistingDailyPlan)
                    .GroupBy(action => action.ProductId)
                    .Select(x => new { ProductId = x.Key, TimesAppeared = x.Count() })
                    .OrderByDescending(x => x.TimesAppeared).First().ProductId;
            }

            if (_planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Username == username)
                .Where(action => action.Action == ActionType.AddedDietPlan).Any() == true)
            {
                report.AvgPlanCalories = _planActionsRepository.GetAllDietPlanActions()
                    .Where(action => action.Username == username)
                    .Where(action => action.Action == ActionType.AddedDietPlan)
                    .Average(action => action.CaloriesPerDay);
            }

            if (_planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Username == username)
                .Where(action => action.Action == ActionType.AddedDietPlan).Any() == true)
            {

                report.AvgPlanLength = _planActionsRepository.GetAllDietPlanActions()
                    .Where(action => action.Username == username)
                    .Where(action => action.Action == ActionType.AddedDietPlan)
                    .Average(action => action.Length);
            }

            report.TotalFav = _productActionsRepository
                .GetAllProductActions()
                .Where(action => action.Username == username)
                .Count(action => action.Action == ActionType.ProductAddedToFavorites);

            return report;
        }

        private string GetAccountStatus(string username)
        {
            var status = "";

            if (username == "admin@admin.ad")
            {
                status = "Active";
                return status;
            }

            var lastUserAction = _userAccountActionsRepository
                .GetAllUserAccountActionsById(username)
                .Where(action =>
                    action.ActionType == UserAccountActionType.UserDeleted ||
                    action.ActionType == UserAccountActionType.UserUnlocked ||
                    action.ActionType == UserAccountActionType.UserLocked).ToList();


            switch (lastUserAction.Last().ActionType)
            {
                case UserAccountActionType.UserUnlocked:
                    status = "Active";
                    break;
                case UserAccountActionType.UserDeleted:
                    status = "Deleted";
                    break;
                case UserAccountActionType.UserLocked:
                    status = "Locked";
                    break;
            }


            return status;
        }
    }
}
