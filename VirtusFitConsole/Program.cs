using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLL;
using Console = System.Console;

namespace VirtusFitConsole
{
    class Program
    {
        public static List<Product> ListOfProducts = new List<Product>();
        static void Main(string[] args)
        {
            ProductLoader.GetProductsFromFile();
            Console.WriteLine(">>>Welcome in dietetic planer!<<<");
            Console.WriteLine();
            int userInput = 1;
            do
            {
                userInput = DisplayMenu();
                new ExecuteOption(userInput);
            } while (userInput != 6);
        }



        public static bool BetweenRanges(int a, int b, int number)
        {
            return (a <= number && number <= b);
        }
        static public int DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose option:");
            Console.WriteLine();
            Console.WriteLine("1. Search for a product by calorific value");
            Console.WriteLine("2. Search for a product by name");
            Console.WriteLine("3. Advanced product search");
            Console.WriteLine("4. Enter a new product");
            Console.WriteLine("5. Create a diet plan");
            Console.WriteLine("6. Exit");
            Console.WriteLine();

            int _result;
            bool _itsNumeric;
 
            _itsNumeric = int.TryParse(Console.ReadLine().ToString(), out _result);
            while (_itsNumeric == false)
            {
                Console.WriteLine("Enter number between 1 and 6");
                _itsNumeric = int.TryParse(Console.ReadLine().ToString(), out _result);
            }

            while (!BetweenRanges(1, 6, _result))
            {
                Console.WriteLine("Enter number between 1 and 6");
                _itsNumeric = int.TryParse(Console.ReadLine().ToString(), out _result);
            }

            return _result;
        }

        private class ExecuteOption
        {
            private int userInput;

            public ExecuteOption(int userInput)
            {
                switch (userInput)
                {
                    case 1:
                        new SearchProductConsoleInterface();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
