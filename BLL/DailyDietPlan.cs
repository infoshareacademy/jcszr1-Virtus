using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtusFitWeb.Models
{
    public class DailyDietPlan
    {
        public int Number { get; set; }

        public DateTime Date { get; set; }
        
        public int CaloriesSum { get; set; }
        public int FatSum { get; set; }
        public int CarbohydratesSum { get; set; }
        public int ProteinSum { get; set; }

        public List<Product> ProductListForDay { get; set; }
    }
}
