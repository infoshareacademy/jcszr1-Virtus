using System;
using System.Collections.Generic;


namespace BLL
{
    public class Product
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductWeight { get; set; }
        public int Energy { get; set; }
        public double Fat { get; set; }
        public double SaturatesInFat { get; set; }
        public double Carbohydrates { get; set; }
        public double SugarsInCarbohydrates { get; set; }
        public double Protein { get; set; }
        public double Salt { get; set; }
        public bool IsVegetarian { get; set; }
        public Product(int productId, string productName, int productWeight, int energy, double fat, double saturatesInFat, double carbohydrates, double sugarsInCarbohydrates, double protein, double salt,
            bool isVegetarian)
        {
            this.ProductId = productId;
           this.ProductName = productName;
           this.ProductWeight = productWeight;
           this.Energy = energy;
           this.Fat = fat;
           this.SaturatesInFat = saturatesInFat;
           this.Carbohydrates = carbohydrates;
           this.SugarsInCarbohydrates = sugarsInCarbohydrates;
           this.Protein = protein;
           this.Salt = salt;
           this.IsVegetarian = isVegetarian;
        }

        public Product()
        {
        }
    }
}
