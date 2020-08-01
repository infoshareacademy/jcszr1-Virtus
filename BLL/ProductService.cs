using System;
using System.Collections.Generic;

namespace BLL
{
    public static class ProductService
    {
        public static List<Product> listOfProducts = new List<Product>();
     
       public static void addStaticList()
       {
           listOfProducts.AddRange(ProductLoader._staticListOfProducts);
       }

        public static void AddNewProduct(int id, string productName, int productWeight, int energy, double fat, double saturatesInFat, double carbohydrates, double sugarsInCarbohydrates,  double protein, double salt, bool isVegetarian)
        {
            Product newProduct = new Product(id, productName, productWeight, energy, fat, saturatesInFat, carbohydrates, sugarsInCarbohydrates, protein, salt, isVegetarian); 
            listOfProducts.Add(newProduct);
        }
    }
}
