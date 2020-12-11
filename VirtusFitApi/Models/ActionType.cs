using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public enum ActionType
    {
        AddedNewProduct = 0,
        RemovedProduct,
        ProductAddedToFavorites,
        ProductRemovedFromFavorites,
        EditedProduct,
        AddedDietPlan,
        RemovedDietPlan,
        EditedDietPlan,
        AddedProductToExistingDailyPlan,
        RemovedProductFromExistingDailyPlan,
        EditedProductInExistingDailyPlan
    }

}
