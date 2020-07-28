using System;
using System.Collections.Generic;
using BLL;

namespace VirtusFitConsole
{
    static class DisplayProductList
    {
        public static void DisplayList(List<Product> _productList)
        {


            Console.WriteLine();
            Console.WriteLine("Id".PadRight(4) + "Name".PadRight(15) + "Energy".PadRight(10) + "Fat".PadRight(8)
                              + "Saturates".PadRight(10) + "Carbohydrates".PadRight(14) + "Sugars In".PadRight(14)
                              + "Protein".PadRight(8) + "Salt".PadRight(8) + "Vegetarian".PadRight(8));
            Console.WriteLine("".PadRight(4) + "".PadRight(15) + "".PadRight(10) + "".PadRight(8) + "In Fat".PadRight(10)
                              + "".PadRight(14) + "Carbohydrates".PadRight(14) + "".PadRight(8) + "".PadRight(8) + "".PadRight(8));
            Console.WriteLine("All values refer to 100g portion.");
            for (int i = 0; i < 95; i++) Console.Write("#");
            Console.WriteLine();
            Console.WriteLine();
            
            foreach (var product in _productList)
            {
                Console.WriteLine((product.ProductId + ".").PadRight(4) + product.ProductName.PadRight(15)
                                                                        + (product.Energy + "kcal").PadRight(10) + (product.Fat + "g").PadRight(8)
                                                                        + (product.SaturatesInFat + "g").PadRight(8) + (product.Carbohydrates + "g").PadRight(13)
                                                                        + (product.SugarsInCarbohydrates + "g").PadRight(13) + (product.Protein + "g").PadRight(8)
                                                                        + (product.Salt + "g").PadRight(8) + (product.IsVegetarian + "g").PadRight(8));
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}