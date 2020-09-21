using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Logic
{
    public interface IDietPlanService
    {
        IEnumerable<DietPlan> ListAll();
        DietPlan GetDietPlan(int id);
        List<Product> GetProductList();
        ProductOnDietPlan GetProductToAdd(int id);
        void Edit(int id, DietPlan dietPlan);
        void DeleteById(int id);
        DietPlan Create(DietPlan newDietPlan);
        List<DailyDietPlan> ListDailyDietPlans(int id);
        DailyDietPlan GetDailyDietPlan(int id, int dayNumber);
        List<ProductOnDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber);
        void AddProductToDailyDietPlan(int id, int dayNumber, ProductOnDietPlan productToAdd, Product product);
        Product GetProductFromList(int id);
    }
}
