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
            Console.WriteLine("Id".PadRight(6) + "Name".PadRight(50) + "Quantity".PadRight(11) + "Portion".PadRight(11) 
                              + "Energy".PadRight(10) + "Fat".PadRight(8) + "Carbohydrates".PadRight(14) + "Sugars".PadRight(8)
                              + "Protein".PadRight(8) + "Salt".PadRight(8) + "Fiber".PadRight(8));
            Console.WriteLine("".PadRight(6) + "".PadRight(50) + "".PadRight(11) + "Quantity".PadRight(11) + "".PadRight(10)
                              + "".PadRight(8) + "".PadRight(14) + "".PadRight(8) + "".PadRight(8) + "".PadRight(8) + "".PadRight(8));
            Console.WriteLine("All values refer to 100g portion.");
            for (var i = 0; i < 142; i++) Console.Write("#");
            Console.WriteLine();
            Console.WriteLine();
            
            if (productList.Count <= 0) Console.WriteLine("There are no items to display.");
            foreach (var product in productList)
            {
                Console.WriteLine((product.ProductId + ".").PadRight(6) + product.ProductName.PadRight(50)
                                                                        + (product.Quantity + product.PortionUnit).PadRight(11)
                                                                        + (product.PortionQuantity + product.PortionUnit).PadRight(11)
                                                                        + (product.Energy + "kCal").PadRight(10) 
                                                                        + (product.Fat + "g").PadRight(8)
                                                                        + (product.Carbohydrates + "g").PadRight(14)
                                                                        + (product.Sugar + "g").PadRight(8) 
                                                                        + (product.Protein + "g").PadRight(8)
                                                                        + (product.Salt + "g").PadRight(8) 
                                                                        + (product.Fiber + "g").PadRight(8));
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}