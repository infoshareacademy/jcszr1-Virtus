using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BLL
{
    public class ProductLoader
    {
        private static readonly List<Product> _staticListOfProducts = new List<Product>
        {
            new Product {productId = 1, productName = "something1", productWeight = 2, energy = 1, saturatesInFat = 20, carbohydrates = 32, fat = 5, sugarsInCarbohydrates = 35, protein = 61, salt = 8, isVegetarian = true},
            new Product {productId = 2, productName = "something2", productWeight = 1, energy = 9, saturatesInFat = 70, carbohydrates = 84, fat = 75, sugarsInCarbohydrates = 80, protein = 8, salt = 20, isVegetarian = false},
            new Product {productId = 3, productName = "something3", productWeight = 7, energy = 5, saturatesInFat = 37, carbohydrates = 99, fat = 535, sugarsInCarbohydrates = 50, protein = 23, salt = 1, isVegetarian = true},
            new Product {productId = 4, productName = "something4", productWeight = 8, energy = 90, saturatesInFat = 213, carbohydrates = 16, fat = 15, sugarsInCarbohydrates = 156, protein = 78, salt = 3, isVegetarian = false},
            new Product {productId = 5, productName = "something5", productWeight = 9, energy = 11, saturatesInFat = 11, carbohydrates = 3, fat = 25, sugarsInCarbohydrates = 119, protein = 120, salt = 66, isVegetarian = true},
        };

        public static List<Product> GetProductsFromFile()
        {
            //TODO: Create actual logic for loading products from file.
            return _staticListOfProducts;
        }
    }
}
