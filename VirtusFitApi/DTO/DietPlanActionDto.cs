using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Models;

namespace VirtusFitApi.DTO
{
    public class DietPlanActionDto
    {
        public int DietPlanId { get; set; }
        public int Length { get; set; }
        public int CaloriesPerDay { get; set; }
        public string Username { get; set; }
        public ActionType Action { get; set; }
        public DateTime Created { get; set; }
    }
}
