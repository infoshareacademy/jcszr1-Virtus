using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BLL
{
    public class ProductLoader
    {
        public static readonly List<Product> _staticListOfProducts = new List<Product>
        {
          new Product {ProductId = 1, ProductName = "something1", ProductWeight = 2, Energy = 1, SaturatesInFat = 20, Carbohydrates = 32, Fat = 5, SugarsInCarbohydrates = 35, Protein = 61, Salt = 8, IsVegetarian = true},
          new Product {ProductId = 2, ProductName = "something2", ProductWeight = 1, Energy = 9, SaturatesInFat = 70, Carbohydrates = 84, Fat = 75, SugarsInCarbohydrates = 80, Protein = 8, Salt = 20, IsVegetarian = false},
          new Product {ProductId = 3, ProductName = "something3", ProductWeight = 7, Energy = 5, SaturatesInFat = 37, Carbohydrates = 99, Fat = 535, SugarsInCarbohydrates = 50, Protein = 23, Salt = 1, IsVegetarian = true},
          new Product {ProductId = 4, ProductName = "something4", ProductWeight = 8, Energy = 90, SaturatesInFat = 213, Carbohydrates = 16, Fat = 15, SugarsInCarbohydrates = 156, Protein = 78, Salt = 3, IsVegetarian = false},
         new Product {ProductId = 5, ProductName = "something5", ProductWeight = 9, Energy = 11, SaturatesInFat = 11, Carbohydrates = 3, Fat = 25, SugarsInCarbohydrates = 119, Protein = 120, Salt = 66, IsVegetarian = true},
        };

        public static List<Product> GetProductsFromFile()
        {
            //TODO: Create actual logic for loading products from file.
            return _staticListOfProducts;
        }
    }
}
//public int productId { get; set; }
//public string productName { get; set; }
//public int productWeight { get; set; }
//public int energy { get; set; }
//public double fat { get; set; }
//public double saturatesInFat { get; set; }
//public double carbohydrates { get; set; }
//public double sugarsInCarbohydrates { get; set; }
//public double protein { get; set; }
//public double salt { get; set; }
//public bool isVegetarian { get; set; }
