using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using BLL;

namespace VirtusFitConsole
{
    class EditDataFromConsole
    {
        public void MenuDataEditOption()
        {
            Console.WriteLine("Provide the ID of the product that you would like to update");
            string productID = Console.ReadLine();
            int productIndex;

            foreach (var item in Program.ListOfProducts)
            {
                if (item.ProductId == Convert.ToInt32(productID))
                {
                    productIndex = Program.ListOfProducts.IndexOf(item, Convert.ToInt32(productID));
                    Product productToBeUpdated = Program.ListOfProducts[productIndex];
                    UpdateData(productToBeUpdated);
                }
            }

        }

        public void UpdateData(Product productToBeUpdated)
        {
            Console.Clear();
            Console.WriteLine("What would you like to update?");
            Console.WriteLine("A. Product name");
            Console.WriteLine("B. Energy ");
            Console.WriteLine("C. Fat");
            Console.WriteLine("D. Carbohydrates");
            Console.WriteLine("E. Proteins");
            Console.WriteLine("F. Salt");
            Console.WriteLine("G. Fiber");
            Console.WriteLine("H. Sugar");
            Console.WriteLine("I. Product quantity");
            Console.WriteLine("J. Portion quantity");
            Console.WriteLine("K. Portion unit");
            Console.WriteLine("Press keys A-K to choose your option: ");
            var userChoice = Console.ReadKey();
            string newStringValue;
            int newIntValue;

            switch (userChoice.Key)
            {
                case ConsoleKey.A:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new product name: ");
                    newStringValue = Console.ReadLine();
                    productToBeUpdated.ProductName = newStringValue;
                    Console.WriteLine("Product name has been updated!");
                    break;
                }
                case ConsoleKey.B:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new energy value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.Energy = newIntValue;
                    Console.WriteLine("Product energy value has been updated!");
                    break;
                }
                case ConsoleKey.C:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new fat value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.Fat = newIntValue;
                    Console.WriteLine("Product fat value has been updated!");
                    break;
                }
                case ConsoleKey.D:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new carbohydrates value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.Carbohydrates = newIntValue;
                    Console.WriteLine("Product carbohydrates value has been updated!");
                    break;
                }
                case ConsoleKey.E:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new proteins value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.Protein = newIntValue;
                    Console.WriteLine("Product proteins value has been updated!");
                    break;
                }
                case ConsoleKey.F:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new salt value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.Salt = newIntValue;
                    Console.WriteLine("Product salt value has been updated!");
                    break;
                }
                case ConsoleKey.G:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new fiber value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.Fiber = newIntValue;
                    Console.WriteLine("Product fiber value has been updated!");
                    break;
                }
                case ConsoleKey.H:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new sugar value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.Sugar = newIntValue;
                    Console.WriteLine("Product salt value has been updated!");
                    break;
                }
                case ConsoleKey.I:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new product quantity value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.Quantity = newIntValue;
                    Console.WriteLine("Product product quantity value has been updated!");
                    break;
                }
                case ConsoleKey.J:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new portion quantity value: ");
                    newIntValue = Convert.ToInt32(Console.ReadLine());
                    productToBeUpdated.PortionQuantity = newIntValue;
                    Console.WriteLine("Product portion quantity value has been updated!");
                    break;
                }
                case ConsoleKey.K:
                {
                    Console.Clear();
                    Console.WriteLine("Please provide new portion unit value: ");
                    newStringValue = Console.ReadLine();
                    productToBeUpdated.PortionUnit = newStringValue;
                    Console.WriteLine("Product portion quantity value has been updated!");
                    break;
                }
            }
        }
    }
}
