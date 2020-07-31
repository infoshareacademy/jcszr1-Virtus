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

        public static void AddNewProduct(int _id, string _productName, int _productWeight, int _energy, double _fat, double _saturatesInFat, double _carbohydrates, double _sugarsInCarbohydrates,  double _protein, double _salt, bool _isVegetarian)
        {
            //Product newProduct2 = new Product(_id, _productName, _productWeight, _energy, _fat, _saturatesInFat, _carbohydrates, _sugarsInCarbohydrates, _protein, _salt, _isVegetarian); - CANNOT CREATE VIA CONSTRUCTOR, WHY?!
            Product newProduct = new Product{productId = _id, productName = _productName, productWeight = _productWeight, energy = _energy, fat = _fat, saturatesInFat = _saturatesInFat, carbohydrates = _carbohydrates, sugarsInCarbohydrates = _sugarsInCarbohydrates, protein = _protein, salt = _salt, isVegetarian = _isVegetarian};
            listOfProducts.Add(newProduct);
            // listOfProducts.Add(newProduct2);

        }
    }// 1, "Apple", 3, 4, 5, 2, 1, 4,2, 7, true
}
