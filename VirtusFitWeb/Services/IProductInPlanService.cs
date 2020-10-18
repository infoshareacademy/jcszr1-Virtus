using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IProductInPlanService
    {
        List<Product> GetProductList();
        ProductInDietPlan GetProductToAdd(int id);
        void AddProductToDailyDietPlan(int id, int dayNumber, ProductInDietPlan productToAdd, Product product);
        Product GetProductFromList(int id);
        ProductInDietPlan GetProductFromDietPlan(int id, int dayNumber, int ordinalNumber);
        void EditProductInDailyDietPlan(int id, int dayNumber, ProductInDietPlan editedProduct,
            int currentProductOrdinalNumber);
        void DeleteProductFromPlan(int id, int dayNumber, int ordinalNumber);
    }
}
