using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [DisplayName("Product No.")]
        public int ProductNo { get; set; }
        [Required]
        [DisplayName("Product Name")]
        [StringLength(60, ErrorMessage = "Product name cannot be longer than 60 characters")]
        public string ProductName { get; set; }
        [Required]
        [Range(0,int.MaxValue, ErrorMessage = "Value cannot be less than 0")]
        public int Energy { get; set; }
        [Required]
        [Range(0,100, ErrorMessage = "Value must be within  the range of (0 - 100)")]
        public double Fat { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Value must be within  the range of (0 - 100)")]
        public double Carbohydrates { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Value must be within  the range of (0 - 100)")]
        public double Protein { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Value must be within  the range of (0 - 100)")]
        public double Salt { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Value must be within  the range of (0 - 100)")]
        public int Fiber { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Value must be within  the range of (0 - 100)")]
        public int Sugar { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be more than 0.")]
        public int Quantity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be more than 0.")]
        [DisplayName("Portion Quantity")]
        public int PortionQuantity { get; set; }
        [Required]
        [DisplayName("Portion Unit")]
        public string PortionUnit { get; set; }
        [DisplayName("Favorite")]
        public bool IsFavorite { get; set; }
        public string UserId { get; set; }
    }
}

