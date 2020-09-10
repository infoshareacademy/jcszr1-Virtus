using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class ProductOnDietPlan
    {
        public Product Product { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int PortionSize { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfPortions { get; set; }
    }
}
