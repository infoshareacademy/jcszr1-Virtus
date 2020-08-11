﻿using BLL;
using System;
using System.Collections.Generic;
using Console = System.Console;

namespace VirtusFitConsole
{
    class Program
    {
        public static List<Product> ListOfProducts = new List<Product>();

        static void Main(string[] args)
        {

            try
            {
                foreach (var item in ProductLoader.GetProductsFromFile())
                {
                    ListOfProducts.Add(item);
                }

                //foreach (var item in ListOfProducts)
                //{
                //    Console.WriteLine(item.ProductName + item.ProductId + item.PortionQuantity + item.PortionUnit + item.Energy + item.Fat + item.Fiber + item.Sugar);
                //}
                DisplayProductList.DisplayList(ListOfProducts);
                AddProductFromConsole();

                DisplayProductList.DisplayList(ListOfProducts);
                EditDataFromConsoleInterface testInterface = new EditDataFromConsoleInterface();
                testInterface.EditProductInterface();

                DisplayProductList.DisplayList(ListOfProducts);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message} \nPress any key.");
                Console.ReadKey();
            }
        }

        private static int ID = 9000;
        public static void AddProductFromConsole()
        {
            try
            {
                Console.WriteLine("Product name:");
                string productName = Console.ReadLine();
                Console.WriteLine("Weight unit (G for gramms/ML for mililiters):");
                string portionUnit = Console.ReadLine().ToUpper();
                if (portionUnit != "G" && portionUnit !="ML")
                {
                    throw (new ArgumentException("Incorrect input, only 'G' or 'ML' is acceptable."));
                }
                Console.WriteLine("Product weight:");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Portion weight:");
                int portionQuantity = int.Parse(Console.ReadLine());
                if (portionQuantity > quantity)
                {
                    throw (new ArgumentException("Portion weight cannot be bigger that the Product weight"));
                }
                Console.WriteLine($"Energy (kCal) in 100{portionUnit}:");
                int energy = int.Parse(Console.ReadLine());
                Console.WriteLine($"Fat in 100{portionUnit}");
                double fat = double.Parse(Console.ReadLine());
                if (fat > 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                Console.WriteLine($"Carbohydrates in 100{portionUnit}:");
                double carbohydrates = double.Parse(Console.ReadLine());
                if (fat + carbohydrates > 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                Console.WriteLine($"Protein in 100{portionUnit}:");
                double protein = double.Parse(Console.ReadLine());
                if (fat + carbohydrates + protein> 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                Console.WriteLine($"Salt in 100{portionUnit}");
                double salt = double.Parse(Console.ReadLine());
                if (salt + fat + carbohydrates + protein > 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                Console.WriteLine($"Fiber in 100{portionUnit}:");
                int fiber = int.Parse(Console.ReadLine());
                if (fiber + salt + fat + carbohydrates + protein > 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                Console.WriteLine($"Sugar in 100{portionUnit}:");
                int sugar = int.Parse(Console.ReadLine());
                if (sugar>carbohydrates)
                {
                    throw (new ArgumentException("The weight of sugars cannot be bigger that weight of carbohydrates"));
                }
                ID++;
                ProductService.AddNewProduct(ID, productName, portionUnit, quantity, portionQuantity, energy, fat, carbohydrates, protein, sugar, salt, fiber, ListOfProducts);
            }
            //int productId, string productName, string portionUnit, int quantity,  int portionQuantity, int energy, double fat, double carbohydrates, double protein, int sugar, double salt, int fiber)
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
        
    }

}
