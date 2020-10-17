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
        public int Energy { get; set; }
        [Required]
        public double Fat { get; set; }
        [Required]
        public double Carbohydrates { get; set; }
        [Required]
        public double Protein { get; set; }
        [Required]
        public double Salt { get; set; }
        [Required]
        public int Fiber { get; set; }
        [Required]
        public int Sugar { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int PortionQuantity { get; set; }
        [Required]
        public string PortionUnit { get; set; }
        public bool IsFavourite { get; set; }
    }
}

