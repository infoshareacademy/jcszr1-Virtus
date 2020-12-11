using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public enum SearchActionType
    {
        SearchByName = 0,
        SearchByFat,
        SearchByCalories,
        SearchByCarbohydrates,
        SearchByProtein
    }
}
