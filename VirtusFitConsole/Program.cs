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
            int userInput = 0;
            do
            {
                userInput = DisplayMenu();
            } while (userInput != 6);
            /*try
            {
                foreach (var item in ProductLoader.GetProductsFromFile())
                {
                    ListOfProducts.Add(item);
                }

                 foreach (var item in ListOfProducts)
                {
                    Console.WriteLine(item.ProductName + item.ProductId + item.PortionQuantity + item.PortionUnit + item.Energy + item.Fat + item.Fiber + item.Sugar);
                }

                DisplayProductList.DisplayList(ListOfProducts);
                EditDataFromConsoleInterface testInterface = new EditDataFromConsoleInterface();
                testInterface.EditProductInterface();   //EditProductInterface(ListOfProducts);

                DisplayProductList.DisplayList(ListOfProducts);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message} \nPress any key.");
                Console.ReadKey();
            } */

        }
        static public int DisplayMenu()
        {
            Console.WriteLine("Wybierz opcję");
            Console.WriteLine();
            Console.WriteLine("1. Wyszukaj produkt po kaloryczności");
            Console.WriteLine("2. Wyszukaj produkt po nazwie");
            Console.WriteLine("3. Zawansowane wyszukiwanie produktu");
            Console.WriteLine("4. Wprowadź nowy produkt");
            Console.WriteLine("5. Stwórz plan dietetyczny");
            Console.WriteLine("6. Zakończ");
            
            int result=1;
            bool itsNumeric=false;
            
            while (itsNumeric == false)
            {
                itsNumeric = int.TryParse(Console.ReadLine().ToString(), out result);
            }

            return result;
        }
    }
}
