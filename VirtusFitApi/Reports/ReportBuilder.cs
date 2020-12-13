using System;
using System.Collections.Generic;
using System.Linq;
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

        public ReportBuilder(IPlanActionsRepository planActionsRepository, IProductActionsRepository productActionsRepository, IUserAccountActionsRepository userAccountActionsRepository)
        {
            _productActionsRepository = productActionsRepository;
            _planActionsRepository = planActionsRepository;
            _userAccountActionsRepository = userAccountActionsRepository;
        }

        public OverallReport CreateOverallReport()
        {
            var report = new OverallReport();

            report.CreatedUserAccounts = _userAccountActionsRepository.GetAllUserAccountActions()
                .Where(accountAction => accountAction.ActionType == UserAccountActionType.AccountCreated).ToList().Count;

            report.TotalLogonCount = _userAccountActionsRepository.GetAllUserAccountActions()
                .Where(accountAction => accountAction.ActionType == UserAccountActionType.SuccessfulLogonAttempt).ToList().Count;

            report.AddedProducts = _productActionsRepository.GetAllProductActions()
                .Where(productAction => productAction.Action == ActionType.AddedNewProduct).ToList().Count;

            report.RemovedProducts = _productActionsRepository.GetAllProductActions()
                .Where(productAction => productAction.Action == ActionType.RemovedProduct).ToList().Count;

            report.AddedPlans = _planActionsRepository.GetAllDietPlanActions().ToList().Count;

            report.ProductsAddedToFav = _productActionsRepository
                .GetAllProductActions().Count(productAction => productAction.Action == ActionType.ProductAddedToFavorites);

            report.ProductsRemovedFromFav = _productActionsRepository
                .GetAllProductActions().Count(productAction => productAction.Action == ActionType.ProductRemovedFromFavorites);

            report.ProductsAddedToPlans = _planActionsRepository.GetAllProductInPlanActions()
                .Count(productInPlanAction => productInPlanAction.Action == ActionType.AddedProductToExistingDailyPlan);

            report.SearchesDone = _productActionsRepository.GetAllSearchValueActions().Count +
                                  _productActionsRepository.GetAllSearchStringActions().Count;

            report.TopStringSearch = _productActionsRepository.GetAllSearchStringActions().GroupBy(x => x.SearchString)
                .Select(x => new {SearchString = x.Key, TimesAppeared = x.Count()}).ToList()
                .OrderByDescending(x => x.TimesAppeared).First().SearchString;

            report.AvgCaloriesSearch = _productActionsRepository.GetAllSearchValueActions()
                .Where(action => action.SearchType == SearchActionType.SearchByCalories).ToList()
                .Average(value => value.SearchValue);

            report.AvgCarbohydratesSearch = _productActionsRepository.GetAllSearchValueActions()
                .Where(action => action.SearchType == SearchActionType.SearchByCarbohydrates).ToList()
                .Average(value => value.SearchValue);

            report.AvgFatSearch = _productActionsRepository.GetAllSearchValueActions()
                .Where(action => action.SearchType == SearchActionType.SearchByFat).ToList()
                .Average(value => value.SearchValue);

            report.AvgProteinsSearch = _productActionsRepository.GetAllSearchValueActions()
                .Where(action => action.SearchType == SearchActionType.SearchByProtein).ToList()
                .Average(value => value.SearchValue);

            report.TopFavId = _productActionsRepository.GetAllProductActions()
                .Where(action => action.Action == ActionType.ProductAddedToFavorites).ToList()
                .GroupBy(action => action.ProductId)
                .Select(x => new {ProductId = x.Key, TimesAppeared = x.Count()}).ToList()
                .OrderByDescending(x => x.TimesAppeared).First().ProductId;

            report.AvgUserBmi = _planActionsRepository.GetAllBmiActions().Average(bmi => bmi.Bmi);

            report.AvgPlanLength = _planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Action == ActionType.AddedDietPlan).ToList().Average(action => action.Length);

            report.AvgPlanCalories = _planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Action == ActionType.AddedDietPlan).ToList()
                .Average(action => action.CaloriesPerDay);

            return report;
        }

        public OverallReport CreateDailyReport()
        {
            var date = DateTime.Today;
            var report = new OverallReport();

            report.CreatedUserAccounts = _userAccountActionsRepository.GetAllUserAccountActions()
                .Where(actions => actions.Created.Date == date.Date)
                .Where(accountAction => accountAction.ActionType == UserAccountActionType.AccountCreated).ToList().Count;

            report.TotalLogonCount = _userAccountActionsRepository.GetAllUserAccountActions()
                .Where(actions => actions.Created.Date == date.Date)
                .Where(accountAction => accountAction.ActionType == UserAccountActionType.SuccessfulLogonAttempt).ToList().Count;

            report.AddedProducts = _productActionsRepository.GetAllProductActions()
                .Where(actions => actions.Created.Date == date.Date)
                .Where(productAction => productAction.Action == ActionType.AddedNewProduct).ToList().Count;

            report.RemovedProducts = _productActionsRepository.GetAllProductActions()
                .Where(actions => actions.Created.Date == date.Date)
                .Where(productAction => productAction.Action == ActionType.RemovedProduct).ToList().Count;

            report.AddedPlans = _planActionsRepository.GetAllDietPlanActions()
                .Where(actions => actions.Created.Date == date.Date)
                .ToList().Count;

            report.ProductsAddedToFav = _productActionsRepository
                .GetAllProductActions().Where(actions => actions.Created.Date == date.Date)
                .Count(productAction => productAction.Action == ActionType.ProductAddedToFavorites);

            report.ProductsRemovedFromFav = _productActionsRepository
                .GetAllProductActions()
                .Where(actions => actions.Created.Date == date.Date)
                .Count(productAction => productAction.Action == ActionType.ProductRemovedFromFavorites);

            report.ProductsAddedToPlans = _planActionsRepository
                .GetAllProductInPlanActions()
                .Where(actions => actions.Created.Date == date.Date)
                .Count(productInPlanAction => productInPlanAction.Action == ActionType.AddedProductToExistingDailyPlan);

            report.SearchesDone = _productActionsRepository
                                      .GetAllSearchValueActions().Count(search => search.Created.Date == date.Date) +
                                  _productActionsRepository
                                      .GetAllSearchStringActions().Count(search => search.Created.Date == date.Date);

            report.TopStringSearch = _productActionsRepository.GetAllSearchStringActions()
                .Where(search => search.Created.Date == date.Date)
                .GroupBy(x => x.SearchString)
                .Select(x => new { SearchString = x.Key, TimesAppeared = x.Count() }).ToList()
                .OrderByDescending(x => x.TimesAppeared).First().SearchString;

            report.AvgCaloriesSearch = _productActionsRepository.GetAllSearchValueActions()
                .Where(search => search.Created.Date == date.Date)
                .Where(action => action.SearchType == SearchActionType.SearchByCalories).ToList()
                .Average(value => value.SearchValue);

            report.AvgCarbohydratesSearch = _productActionsRepository.GetAllSearchValueActions()
                .Where(search => search.Created.Date == date.Date)
                .Where(action => action.SearchType == SearchActionType.SearchByCarbohydrates).ToList()
                .Average(value => value.SearchValue);

            report.AvgFatSearch = _productActionsRepository.GetAllSearchValueActions()
                .Where(search => search.Created.Date == date.Date)
                .Where(action => action.SearchType == SearchActionType.SearchByFat).ToList()
                .Average(value => value.SearchValue);

            report.AvgProteinsSearch = _productActionsRepository.GetAllSearchValueActions()
                .Where(search => search.Created.Date == date.Date)
                .Where(action => action.SearchType == SearchActionType.SearchByProtein).ToList()
                .Average(value => value.SearchValue);

            report.TopFavId = _productActionsRepository.GetAllProductActions()
                .Where(search => search.Created.Date == date.Date)
                .Where(action => action.Action == ActionType.ProductAddedToFavorites)
                .GroupBy(action => action.ProductId)
                .Select(x => new { ProductId = x.Key, TimesAppeared = x.Count() }).ToList()
                .OrderByDescending(x => x.TimesAppeared).First().ProductId;

            report.AvgUserBmi = _planActionsRepository.GetAllBmiActions()
                .Where(bmi => bmi.Created.Date == date.Date)
                .ToList().Average(bmi => bmi.Bmi);

            report.AvgPlanLength = _planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Created.Date == date.Date)
                .Where(action => action.Action == ActionType.AddedDietPlan).ToList().Average(action => action.Length);

            report.AvgPlanCalories = _planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Created.Date == date.Date)
                .Where(action => action.Action == ActionType.AddedDietPlan).ToList()
                .Average(action => action.CaloriesPerDay);

            return report;
        }

        public UserReport CreateUserReport(string id)
        {
            var report = new UserReport();

            report.Created = _userAccountActionsRepository.GetAllUserAccountActionsById(id)
                .Where(action => action.ActionType == UserAccountActionType.AccountCreated)
                .Select(action => action.Created).FirstOrDefault();

            report.Username = id;

            report.LastLogon = _userAccountActionsRepository.GetAllUserAccountActionsById(id)
                .Where(action => action.ActionType == UserAccountActionType.AccountCreated)
                .Select(action => action.Created).OrderByDescending(action => action.Date).FirstOrDefault();

            report.TotalLogonCount = _userAccountActionsRepository.GetAllUserAccountActionsById(id)
                .Where(action => action.ActionType == UserAccountActionType.SuccessfulLogonAttempt)
                .ToList().Count;

            report.TotalAddedProducts = _productActionsRepository.GetAllProductActions()
                .Where(action => action.Username == id)
                .Where(action => action.Action == ActionType.AddedNewProduct)
                .ToList().Count;

            report.TotalAddedPlans = _planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Action == ActionType.AddedDietPlan)
                .Where(action => action.Username == id).ToList().Count;

            report.MostUsedProduct = _planActionsRepository.GetAllProductInPlanActions()
                .Where(action => action.Username == id)
                .Where(action => action.Action == ActionType.AddedProductToExistingDailyPlan)
                .GroupBy(action => action.ProductId)
                .Select(x => new {ProductId = x.Key, TimesAppeared = x.Count()})
                .OrderByDescending(x => x.TimesAppeared).FirstOrDefault().ProductId;

            report.AvgPlanCalories = _planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Username == id)
                .Where(action => action.Action == ActionType.AddedDietPlan)
                .Average(action => action.CaloriesPerDay);

            report.AvgPlanLength = _planActionsRepository.GetAllDietPlanActions()
                .Where(action => action.Username == id)
                .Where(action => action.Action == ActionType.AddedDietPlan)
                .Average(action => action.Length);

            report.TotalFav = _productActionsRepository
                .GetAllProductActions()
                .Where(action => action.Username == id)
                .Count(action => action.Action == ActionType.ProductAddedToFavorites);

            return report;
        }
    }
}
