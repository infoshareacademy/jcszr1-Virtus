using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Models
{
    public class ProductsToAddViewModel
    {
        public int DietPlanId { get; set; }
        public int DietPlanNo { get; set; }
        public int DayNumber { get; set; }
        public string Date { get; set; }
        public int CaloriesPerDay { get; set; }
        public List<Product> ProductList { get; set; }
    }
}