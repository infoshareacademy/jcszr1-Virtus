﻿using BLL;

namespace VirtusFitWeb.Models
{
    public class ProductOnDietPlan
    {
        public Product Product { get; set; }
        public int PortionSize { get; set; }
        public double NumberOfPortions { get; set; }
    }
}
