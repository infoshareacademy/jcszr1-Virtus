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

            try
            {
                foreach (var item in ProductLoader.productsFromJson)
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

        }
    }
}
