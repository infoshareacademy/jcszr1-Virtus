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
            Console.WriteLine("1. Simple product search");
            Console.WriteLine("2. Advanced product search");
            Console.WriteLine("3. Edit product");
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
                        new SearchProductConsoleInterface().SearchProductInterface(ListOfProducts);
                        break;
                    case 2:
                        //
                        break;
                    case 3:
                        try
                        {
                            foreach (var item in ProductLoader.GetProductsFromFile())
                            {
                                ListOfProducts.Add(item);
                            }

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
                        new EditDataFromConsoleInterface().EditProductInterface();
                        break;
                    case 4:
                        //
                        break;
                    case 5:
                        //
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
