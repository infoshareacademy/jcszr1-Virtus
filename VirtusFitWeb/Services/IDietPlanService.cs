using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IDietPlanService
    {
        IEnumerable<DietPlan> ListAll();
        DietPlan GetDietPlan(int id);
        List<Product> GetProductList();
        ProductInDietPlan GetProductToAdd(int id);
        void Edit(int id, DietPlan dietPlan);
        void DeleteById(int id);
        DietPlan Create(DietPlan newDietPlan);
        List<DailyDietPlan> ListDailyDietPlans(int id);
        DailyDietPlan GetDailyDietPlan(int id, int dayNumber);
        List<ProductInDietPlan> ListProductsOnDailyDietPlan(int id, int dayNumber);
        void AddProductToDailyDietPlan(int id, int dayNumber, ProductInDietPlan productToAdd, Product product);
        Product GetProductFromList(int id);
        ProductInDietPlan GetProductFromDietPlan(int id, int dayNumber, int ordinalNumber);
        void EditProductInDailyDietPlan(int id, int dayNumber, ProductInDietPlan editedProduct,
            int currentProductOrdinalNumber);
        void DeleteProductFromPlan(int id, int dayNumber, int ordinalNumber);
    }
}
