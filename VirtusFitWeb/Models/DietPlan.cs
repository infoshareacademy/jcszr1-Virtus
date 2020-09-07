﻿using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtusFitWeb.Models
{
    public class DietPlan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter start date.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter end date.")]
        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }
        [Required(ErrorMessage = "Enter daily calories amount.")]
        public int CaloriesPerDay { get; set; }
        public Dictionary<DateTime, List<Product>> ProductListForDay { get; set; }
    }
}
