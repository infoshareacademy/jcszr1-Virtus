using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;

namespace VirtusFitConsole
{
    class EditDataFromConsoleInterface
    {
        private static readonly string[] EditOptions = { "Product name", "Energy", "Fat", "Carbohydrates", "Proteins", "Salt", "Fiber", "Sugar", "Product quantity", "Portion quantity", "Portion unit" };
        private static int _currentLine;

        private string DisplayOptions(int cursorPos)
        {

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, cursorPos);
            for (int i = 0; i < EditOptions.GetLength(0); i++)
            {
                if (i == _currentLine)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(EditOptions[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                if (_currentLine == EditOptions.GetLength(0) - 1)
                {
                    _currentLine = 0;
                }
                else
                {
                    _currentLine++;
                }
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                if (_currentLine <= 0)
                {
                    _currentLine = EditOptions.GetLength(0) - 1;
                }
                else
                {
                    _currentLine--;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                return EditOptions[_currentLine];
            }
            return "";
        }

        public void EditProductInterface()
        {
            Console.WriteLine("Provide the ID of the product that you would like to update");
            string productId = Console.ReadLine();
            var productToBeUpdated = Program.ListOfProducts.Where(product => product.ProductId.Equals(Convert.ToInt32(productId))).ToList();
            
            
            
            
            
            
            
            
            
            
            
            
            if (productToBeUpdated[0] == null)
            {
                throw new ArgumentException("The product could not be found.");
            }
            string newStringValue;
            int newIntValue;
            Console.WriteLine("What would you like to update?");
            var cursorPos = Console.CursorTop;
            string userDecision;
            do
            {
                userDecision = DisplayOptions(cursorPos);
            } while (userDecision != "Product name" && userDecision != "Energy" && userDecision != "Fat" && userDecision != "Carbohydrates" && userDecision != "Proteins" && userDecision != "Salt" && userDecision != "Fiber" && userDecision != "Sugar" && userDecision != "Product quantity" && userDecision != "Portion quantity" && userDecision != "Portion unit"  );

            Console.Clear();
            switch (userDecision)
            {
                case "Product name":
                    {
                        Console.WriteLine("Please provide new product name: ");
                        newStringValue = Console.ReadLine();
                        productToBeUpdated[0].ProductName = newStringValue;
                        Console.WriteLine("Product name has been updated!");
                        break;
                    }
                case "Energy":
                    {
                        Console.WriteLine("Please provide new energy value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The energy value must be a number higher then or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].Energy = newIntValue;
                        Console.WriteLine("Product energy value has been updated!");
                        break;
                    }
                case "Fat":
                    {
                        Console.WriteLine("Please provide new fat value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The fat value must be a number higher than or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].Fat = newIntValue;
                        Console.WriteLine("Product fat value has been updated!");
                        break;
                    }
                case "Carbohydrates":
                    {
                        Console.WriteLine("Please provide new carbohydrates value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The carbohydrates value must be a number higher than or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].Carbohydrates = newIntValue;
                        Console.WriteLine("Product carbohydrates value has been updated!");
                        break;
                    }
                case "Protein":
                    {
                        Console.WriteLine("Please provide new protein value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The protein value must be a number higher than or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].Protein = newIntValue;
                        Console.WriteLine("Product protein value has been updated!");
                        break;
                    }
                case "Salt":
                    {
                        Console.WriteLine("Please provide new salt value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The salt value must be a number higher than or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].Salt = newIntValue;
                        Console.WriteLine("Product protein value has been updated!");
                        break;
                    }
                case "Fiber":
                    {
                        Console.WriteLine("Please provide new fiber value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The fiber value must be a number higher than or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].Fiber = newIntValue;
                        Console.WriteLine("Product fiber value has been updated!");
                        break;
                    }
                case "Sugar":
                    {
                        Console.WriteLine("Please provide new sugar value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The salt value must be a number higher than or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].Sugar = newIntValue;
                        Console.WriteLine("Product sugar value has been updated!");
                        break;
                    }
                case "Product quantity":
                    {
                        Console.WriteLine("Please provide new product quantity value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The product quantity value must be a number higher than or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].Quantity = newIntValue;
                        Console.WriteLine("Product protein value has been updated!");
                        break;
                    }
                case "Portion quantity":
                    {
                        Console.WriteLine("Please provide new portion quantity value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        while (newIntValue < 0 || valueEntered == false)
                        {
                            Console.WriteLine("The portion quantity value must be a number higher than or equal zero");
                            valueEntered = int.TryParse(Console.ReadLine(), out newIntValue);
                        }
                        productToBeUpdated[0].PortionQuantity = newIntValue;
                        Console.WriteLine("Product portion quantity value has been updated!");
                        break;
                    }
                case "Portion unit":
                    {
                        string[] units = {"g", "ml"};
                        Console.WriteLine("Please provide new portion unit value, g for grams or ml for mililiters");
                        newStringValue = Console.ReadLine();
                        while (newStringValue != "g" && newStringValue != "ml")
                        {
                            Console.WriteLine("Please use either g or ml");
                            newStringValue = Console.ReadLine();
                        }
                        productToBeUpdated[0].PortionUnit = newStringValue;
                        Console.WriteLine("Product portion quantity value has been updated!");
                        break;
                    }
            }
        }
    }
}
