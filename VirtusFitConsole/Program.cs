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

            foreach (var item in ProductLoader.GetProductsFromFile())
            {
                ListOfProducts.Add(item);
            }

            foreach (var item in ListOfProducts)
            {
                Console.WriteLine(item.ProductName + item.ProductId + item.PortionQuantity + item.ProductWeight + item.PortionUnit + item.Energy + item.Fat + item.Fiber + item.Sugar);
            }
        }
    }
}
