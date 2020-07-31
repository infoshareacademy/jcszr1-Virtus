using System;
using System.Collections.Generic;


namespace BLL
{
    public class Product
    {
<<<<<<< HEAD
        private int productId;
        private string productName { get; set; }
        private int productWeight;
        private int energy { get; set; }
        private double fat;
        private double saturatesInFat;
        private double carbonhydrates;
        private double sugarsInCarbonhydrates;
        private double protein { get; set; }
        private double salt;
        private bool isVegetarian;

        public Product(string _productName, int Energy, double protein)
        {
        }
=======
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
>>>>>>> master
    }
}
