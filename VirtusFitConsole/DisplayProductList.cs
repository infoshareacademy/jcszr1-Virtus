using System;
using System.Collections.Generic;
using BLL;

namespace VirtusFitConsole
{
    static class DisplayProductList
    {
        public static void DisplayList(List<Product> productList)
        {


            Console.WriteLine();
            Console.WriteLine("Id".PadRight(4) + "Name".PadRight(15) + "Energy".PadRight(10) + "Fat".PadRight(8)
                              + "Saturates".PadRight(10) + "Carbohydrates".PadRight(14) + "Sugars In".PadRight(14)
                              + "Protein".PadRight(8) + "Salt".PadRight(8) + "Vegetarian".PadRight(8));
            Console.WriteLine("".PadRight(4) + "".PadRight(15) + "".PadRight(10) + "".PadRight(8) + "In Fat".PadRight(10)
                              + "".PadRight(14) + "Carbohydrates".PadRight(14) + "".PadRight(8) + "".PadRight(8) + "".PadRight(8));
            Console.WriteLine("All values refer to 100g portion.");
            for (var i = 0; i < 99; i++) Console.Write("#");
            Console.WriteLine();
            Console.WriteLine();
            
            if (productList.Count <= 0) Console.WriteLine("There are no items to display.");
            foreach (var product in productList)
            {
          //     Console.WriteLine((product.productId + ".").PadRight(4) + product.productName.PadRight(15)
          //                                                             + (product.energy + "kcal").PadRight(10) + (product.fat + "g").PadRight(8)
          //                                                             + (product.saturatesInFat + "g").PadRight(10) + (product.carbohydrates + "g").PadRight(14)
          //                                                             + (product.sugarsInCarbohydrates + "g").PadRight(14) + (product.protein + "g").PadRight(8)
          //                                                             + (product.salt + "g").PadRight(8) + product.isVegetarian.ToString().PadRight(8));
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}