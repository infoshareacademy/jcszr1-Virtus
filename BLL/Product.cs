using System;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Product name cannot be longer than 60 characters")]
        public string ProductName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Energy value must be a positive number")]
        public int Energy { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Fat value must be a positive number")]
        public double Fat { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Carbohydrates value must be a positive number")]
        public double Carbohydrates { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Protein value must be a positive number")]
        public double Protein { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Salt value must be a positive number")]
        public double Salt { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Fiber value must be a positive number")]
        public int Fiber { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Sugar value must be a positive number")]
        public int Sugar { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Product quantity must be a positive number")]
        public int Quantity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Portion quantity must be a positive number")]
        public int PortionQuantity { get; set; }
        [Required]
        public string PortionUnit { get; set; }
        public bool IsFavourite { get; set; }
    }
}

