using System;
using System.Collections.Generic;
using BLL;
using System.Runtime.CompilerServices;
using Console = System.Console;

namespace VirtusFitConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductService.addStaticList();

            foreach (var item in ProductLoader.GetProductsFromFile())
            {
               // ListOfProducts.Add(item);
            }

            foreach (var product in ProductService.listOfProducts)
            {
                Console.WriteLine(product.ProductId + product.ProductName);
            }

            AddProductFromConsole(); 
            AddProductFromConsole();
            foreach (var product in ProductService.listOfProducts)
            {
                Console.WriteLine(product.ProductId + product.ProductName);
            }


            foreach (var item in ListOfProducts)
            {
                Console.WriteLine(item.ProductName + item.ProductId + item.PortionQuantity + item.ProductWeight + item.PortionUnit + item.Energy + item.Fat + item.Fiber + item.Sugar);
            }

            
            SearchProductConsoleInterface testSearch = new SearchProductConsoleInterface();
            testSearch.SearchProductInterface(ListOfProducts);

        }

        private static int ID = 5;
        public static void AddProductFromConsole()
        {
            try
            {
                Console.WriteLine("Product name:");
                string productName = Console.ReadLine();
                Console.WriteLine("Product weight:");
                int productWeight = int.Parse(Console.ReadLine());
                Console.WriteLine("Energy:");
                int energy = int.Parse(Console.ReadLine());
                Console.WriteLine("Fat:");
                double fat = double.Parse(Console.ReadLine());
                Console.WriteLine("Saturates in fats:");
                double saturatesInFat = double.Parse(Console.ReadLine());
                Console.WriteLine("Carbohydrates:");
                double carbohydrates = double.Parse(Console.ReadLine());
                Console.WriteLine("Sugars in carbohydrates:");
                double sugarsInCarbohydrates = double.Parse(Console.ReadLine());
                Console.WriteLine("Protein:");
                double protein = double.Parse(Console.ReadLine());
                Console.WriteLine("Salt:");
                double salt = double.Parse(Console.ReadLine());
                Console.WriteLine("Is the product vegetarian? Y/N");
                string isVegetarianString = Console.ReadLine().ToUpper();
                bool isVegetarian = false;  
                if (isVegetarianString == "Y")
                {
                    isVegetarian = true;
                }
                else if (isVegetarianString =="N")
                {
                    isVegetarian = false;  
                }
                else
                {
                    throw (new ArgumentException("Incorrect input, only 'Y' or 'N' is acceptable."));
                }
                ID++;
                ProductService.AddNewProduct(ID, productName, productWeight, energy, fat, saturatesInFat, carbohydrates, sugarsInCarbohydrates, protein, salt, isVegetarian);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
        
    }

}
