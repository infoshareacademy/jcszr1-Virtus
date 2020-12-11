using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public class CreateProductInPlanAction
    {
        public int DietPlanId { get; set; }
        public int DailyDietPlanId { get; set; }
        public int ProductId { get; set; }
        public string Username { get; set; }
        public string ProductName { get; set; }
        public ActionType Action { get; set; }
        public DateTime Created { get; set; }
    }
}
