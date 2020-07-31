using System;
using System.Collections.Generic;
using BLL;

namespace VirtusFitConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            AddProductFromConsole();
        }
        public static void AddProductFromConsole()
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
            bool isVegetarian = Boolean.Parse(Console.ReadLine());

            ProductService.AddNewProduct();
        }

    }

}
////private int productId;
//private string productName { get; set; }
//private int productWeight;
//private int energy { get; set; }
//private double fat;
//private double saturatesInFat;
//private double carbonhydrates;
//private double sugarsInCarbonhydrates;
//private double protein { get; set; }
//private double salt;
//private bool isVegetarian;
