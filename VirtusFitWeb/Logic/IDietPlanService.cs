using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Logic
{
    public interface IDietPlanService
    {
        IEnumerable<DietPlan> ListAll();
        DietPlan GetDietPlan(int id);
        IEnumerable<Product> GetProductList();
        ProductOnDietPlan GetProductToAdd(int id);
        bool Edit(int id, DietPlan dietPlan);
        bool DeleteById(int id);
        DietPlan Create(DietPlan newDietPlan);
        IEnumerable<DailyDietPlan> ListDailyDietPlans(int id);
        public DailyDietPlan GetDailyDietPlan(int id, int dayNumber);
        public IEnumerable<ProductOnDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber);
        public void AddProductToDailyDietPlan(int id, int dayNumber, ProductOnDietPlan productToAdd, Product product);
        public Product GetProductFromList(int id);
    }
}
