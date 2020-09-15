using BLL;
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
            var loader = new ProductLoader();

            try
            {
                foreach (var item in loader.GetProductsFromFile())
                {
                    ListOfProducts.Add(item);
                }

                Console.WindowWidth = 150;
                bool terminate = false;
                while (terminate == false)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to VirtusFit application.");
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Available options:");
                    Console.WriteLine("1. Display full list of products.");
                    Console.WriteLine("2. Search for products.");
                    Console.WriteLine("3. Add new product.");
                    Console.WriteLine("4. Edit product.");
                    Console.WriteLine("5. Prepare diet plan.");
                    Console.WriteLine("6. Close application.");
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Please choose your action:");
                    string userChoice = (Console.ReadLine());
                    if (userChoice != "1" && userChoice != "2" && userChoice != "3" && userChoice != "4" && userChoice != "5" && userChoice != "6")
                    {
                        Console.WriteLine("Incorrect input");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                    else
                    {
                        int _userChoice = int.Parse(userChoice);
                        switch (_userChoice)
                        {
                            case 1:
                                DisplayProductList.DisplayList(ListOfProducts);
                                break;
                            case 2:
                                SearchProductConsoleInterface newSearch = new SearchProductConsoleInterface();
                                newSearch.SearchProductInterface(ListOfProducts);
                                break;
                            case 3:
                                AddProductFromConsole();
                                break;
                            case 4:
                                DisplayProductList.DisplayList(ListOfProducts);
                                EditDataFromConsoleInterface testInterface = new EditDataFromConsoleInterface();
                                testInterface.EditProductInterface();
                                break;
                            case 5:
                                Console.WriteLine("There will be a super cool diet planner");
                                Console.WriteLine();
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;
                            case 6:
                                terminate = true;
                                break;
                        }
                    }
                   
                }
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
                Console.WriteLine("Weight unit (g for gramms/ml for mililiters):");
                string portionUnit = Console.ReadLine().ToLower();
                if (portionUnit != "g" && portionUnit !="ml")
                {
                    throw (new ArgumentException("Incorrect input, only 'g' or 'ml' is acceptable."));
                }
                Console.WriteLine("Product weight:");
                int quantity = int.Parse(Console.ReadLine());
                if (quantity < 0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                Console.WriteLine("Portion weight:");
                int portionQuantity = int.Parse(Console.ReadLine()); 
                if (portionQuantity < 0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                if (portionQuantity > quantity)
                {
                    throw (new ArgumentException("Portion weight cannot be bigger that the Product weight"));
                }
                Console.WriteLine($"Energy (kCal) in 100{portionUnit}:");
                int energy = int.Parse(Console.ReadLine());
                if (energy <0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                Console.WriteLine($"Fat in 100{portionUnit}");
                double fat = double.Parse(Console.ReadLine());
                if (fat > 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                if (fat < 0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                Console.WriteLine($"Carbohydrates in 100{portionUnit}:");
                double carbohydrates = double.Parse(Console.ReadLine());
                if (fat + carbohydrates > 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                if (carbohydrates < 0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                Console.WriteLine($"Protein in 100{portionUnit}:");
                double protein = double.Parse(Console.ReadLine());
                if (fat + carbohydrates + protein> 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                if (protein < 0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                Console.WriteLine($"Salt in 100{portionUnit}");
                double salt = double.Parse(Console.ReadLine());
                if (salt + fat + carbohydrates + protein > 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                if (salt < 0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                Console.WriteLine($"Fiber in 100{portionUnit}:");
                int fiber = int.Parse(Console.ReadLine());
                if (fiber + salt + fat + carbohydrates + protein > 100)
                {
                    throw (new ArgumentException("The weight of macro elements cannot be bigger that 100g"));
                }
                if (fiber < 0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                Console.WriteLine($"Sugar in 100{portionUnit}:");
                int sugar = int.Parse(Console.ReadLine());
                if (sugar>carbohydrates)
                {
                    throw (new ArgumentException("The weight of sugars cannot be bigger that weight of carbohydrates"));
                }
                if (sugar < 0)
                {
                    throw (new ArgumentException("The input cannot have negative value"));
                }
                ID++;
                ProductService.AddNewProduct(ID, productName, portionUnit, quantity, portionQuantity, energy, fat, carbohydrates, protein, sugar, salt, fiber, ListOfProducts);
                Console.WriteLine($"Product {productName} been added to the product list.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.WriteLine();
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
        
    }

}
