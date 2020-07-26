using System;
using System.Collections.Generic;


namespace BLL
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int productWeight { get; set; }
        public int energy { get; set; }
        public double fat { get; set; }
        public double saturatesInFat { get; set; }
        public double carbohydrates { get; set; }
        public double sugarsInCarbohydrates { get; set; }
        public double protein { get; set; }
        public double salt { get; set; }
        public bool isVegetarian { get; set; }
    }
}
