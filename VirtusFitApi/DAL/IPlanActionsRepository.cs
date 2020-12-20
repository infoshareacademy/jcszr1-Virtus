using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Models;

namespace VirtusFitApi.DAL
{
    public interface IPlanActionsRepository
    {
        List<DietPlanAction> GetAllDietPlanActions();
        DietPlanAction GetDietPlanActionById(int id);
        bool AddDietPlanAction(DietPlanAction action);
        List<ProductInPlanAction> GetAllProductInPlanActions();
        ProductInPlanAction GetProductInPlanActionById(int id);
        bool AddProductInPlanAction(ProductInPlanAction action);
        List<BmiAction> GetAllBmiActions();
        BmiAction GetBmiActionById(int id);
        bool AddBmiAction(BmiAction action);
    }
}
