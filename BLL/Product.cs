using System;

namespace BLL
{
    public class Product
    {
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
    }
}
