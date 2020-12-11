using BLL;
using BLL.Db_Models;
using System.Collections.Generic;

namespace VirtusFitWeb.DAL
{
    public interface IDietPlanRepository
    {
        List<DietPlan> ListAllDietPlans(string userId);
        DietPlan GetDietPlanById(int id);
        DailyDietPlan GetDailyDietPlan(int id, int dayNumber);
        void InsertDietPlan(DietPlan dietPlan);
        void DeleteDietPlan(DietPlan dietPlan);
        void UpdateDietPlan(DietPlan dietPlan);
        List<DailyDietPlan> ListDailyDietPlans(int id);
        List<ProductInDietPlan> ListProductsInDailyDietPlan(DailyDietPlan dailyDietPlan);
        void AddDailyDietPlan(DailyDietPlan dailyDietPlan);
        void UpdateDailyDietPlan(DailyDietPlan dailyDietPlan);
        void DeleteDailyDietPlan(DailyDietPlan dailyDietPlan);
        ProductInDietPlanDb GetProductFromDailyDietPlan(DailyDietPlan dailyDietPlan, int ordinalNumber);
        List<ProductInDietPlanDb> ListDbProductsInDailyDietPlan(DailyDietPlan dailyDietPlan);
        void AddProductInPlan(ProductInDietPlanDb product);
        void UpdateProductInPlan(ProductInDietPlanDb productInDietPlanDb);
        void DeleteProductInPlan(ProductInDietPlanDb productInDietPlanDb);
    }
}
