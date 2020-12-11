using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public class CreateDietPlanAction
    {
        public int DietPlanId { get; set; }
        public string Username { get; set; }
        public int CaloriesPerDay { get; set; }
        public int Length { get; set; }
        public ActionType Action { get; set; }
        public DateTime Created { get; set; }
    }
}
