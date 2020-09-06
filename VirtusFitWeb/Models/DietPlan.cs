using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtusFitWeb.Models
{
    public class DietPlan
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Dictionary<string, int> Type { get; set; }
    }
}
