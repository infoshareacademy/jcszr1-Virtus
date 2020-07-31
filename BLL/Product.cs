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
        public Product()
        {
        }
        public Product(int _productId, string _productName, int _productWeight, int _energy, double _fat, double _saturatesInFat, double _carbohydrates, double _sugarsInCarbohydrates, double _protein, double _salt,
            bool _IsVegetarian)
        {
            _productId = productId;
            _productName = productName;
            _productWeight = productWeight;
            _energy = energy;
            _fat = fat;
            _saturatesInFat = saturatesInFat;
            _carbohydrates = carbohydrates;
            _sugarsInCarbohydrates = sugarsInCarbohydrates;
            _protein = protein;
            _salt = salt;
            _IsVegetarian = isVegetarian;

        }
    }
}
