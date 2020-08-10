using BLL;
using System;
using Console = System.Console;

namespace VirtusFitConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductService.addStaticList();

           // foreach (var item in ProductLoader.GetProductsFromFile())
            {
               // ListOfProducts.Add(item);
            }

         //  foreach (var product in ProductService.listOfProducts)
         //  {
         //      Console.WriteLine(product.ProductId + product.ProductName);
         //  }

            AddProductFromConsole(); 
            AddProductFromConsole();
            foreach (var product in ProductService.listOfProducts)
            {
                Console.WriteLine(product.ProductId + product.ProductName + product.PortionQuantity + product.PortionUnit);
            }


         //   foreach (var item in ListOfProducts)
         //   {
         //       Console.WriteLine(item.ProductName + item.ProductId + item.PortionQuantity + item.ProductWeight + item.PortionUnit + item.Energy + item.Fat + item.Fiber + item.Sugar);
         //   }
         //
         //   
         //   SearchProductConsoleInterface testSearch = new SearchProductConsoleInterface();
         //   testSearch.SearchProductInterface(ListOfProducts);

        }

        private static int ID = 5;
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
                Console.WriteLine($"Energy in 100{portionUnit}:");
                int energy = int.Parse(Console.ReadLine());
                Console.WriteLine($"Fat in 100{portionUnit}");
                double fat = double.Parse(Console.ReadLine());
                Console.WriteLine($"Carbohydrates in 100{portionUnit}:");
                double carbohydrates = double.Parse(Console.ReadLine());
                Console.WriteLine($"Protein in 100{portionUnit}:");
                double protein = double.Parse(Console.ReadLine());
                Console.WriteLine($"Salt in 100{portionUnit}");
                double salt = double.Parse(Console.ReadLine());
                Console.WriteLine($"Fiber in 100{portionUnit}:");
                int fiber = int.Parse(Console.ReadLine());
                Console.WriteLine($"Sugar in 100{portionUnit}:");
                int sugar = int.Parse(Console.ReadLine());
                ID++;
                ProductService.AddNewProduct(ID, productName, portionUnit, quantity, portionQuantity, energy, fat, carbohydrates, protein, sugar, salt, fiber);
            }
            //int productId, string productName, string portionUnit, int quantity,  int portionQuantity, int energy, double fat, double carbohydrates, double protein, int sugar, double salt, int fiber)
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
        
    }

}
