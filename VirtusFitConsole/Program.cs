using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLL;
using Ex3;
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
            
            SearchProduct newSearch = new SearchProduct();
            GetDataFromUserConsole newData = new GetDataFromUserConsole();
            List<Product> newList = new List<Product>();
            //newData.GetDataFromUser(out var minCaloriesValue, out var maxCaloriesValue, out var exactCaloriesValue);
            newData.GetDataFromUser(out var productName);
            //if (exactCaloriesValue != 0)
            //{
            //    newList = newSearch.SearchByCalories(ListOfProducts, exactCaloriesValue);
            //    DisplayProductList.DisplayList(ListOfProducts);
            //    DisplayProductList.DisplayList(newList);

            //}
            //else
            //{
            //    newList = newSearch.SearchByCalories(ListOfProducts, minCaloriesValue, maxCaloriesValue);
            //    DisplayProductList.DisplayList(ListOfProducts);
            //    DisplayProductList.DisplayList(newList);
            //}
            
            newList = newSearch.SearchByName(ListOfProducts, productName);
            DisplayProductList.DisplayList(ListOfProducts);
            DisplayProductList.DisplayList(newList);
        }
    }
}
