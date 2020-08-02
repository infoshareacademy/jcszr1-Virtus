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

                SearchProductConsoleInterface testSearch = new SearchProductConsoleInterface();
                testSearch.SearchProductInterface(ListOfProducts);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message} \nPress any key.");
                Console.ReadKey();
            }

        }
    }
}
