using System;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class SearchCriteria
    {
        [StringLength(60, ErrorMessage = "Product name cannot be longer than 60 characters")]
        public string ProductName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Energy value must be a positive number")]
        public double? ExactEnergy { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Energy value must be a positive number")]
        public double? MinEnergy { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Energy value must be a positive number")]
        public double? MaxEnergy { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Fat value must be a positive number")]
        public double? ExactFat { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Fat value must be a positive number")]
        public double? MinFat { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Fat value must be a positive number")]
        public double? MaxFat { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Carbohydrates value must be a positive number")]
        public double? ExactCarbohydrates { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Carbohydrates value must be a positive number")]
        public double? MinCarbohydrates { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Carbohydrates value must be a positive number")]
        public double? MaxCarbohydrates { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Protein value must be a positive number")]
        public double? ExactProtein { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Protein value must be a positive number")]
        public double? MinProtein { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Protein value must be a positive number")]
        public double? MaxProtein { get; set; }
    }
}

