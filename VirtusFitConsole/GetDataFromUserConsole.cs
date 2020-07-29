using System;
using System.Collections.Generic;


namespace Ex3
{
    class GetDataFromUserConsole
    {
        public void GetDataFromUser(out string productName)
        {
            productName = GetSearchByNameData();
        }
        public void GetDataFromUser(out int minCaloriesValue, out int maxCaloriesValue, out int exactCaloriesValue)
        {
            exactCaloriesValue = GetSearchByCaloriesData(out minCaloriesValue, out maxCaloriesValue);
        }

        private string GetSearchByNameData()
        {
            Console.Write("Enter product name or a part of its name: ");
            var productName = Console.ReadLine();
            return productName;
        }
        
        public int GetSearchByCaloriesData(out int minValue, out int maxValue)
        {
            minValue = 0;
            maxValue = 0;
            var searchValue = 0;
            Console.WriteLine("Would You like to search by exact Calories value or by range?");
            var cursorPos = Console.CursorTop;
            var userDecision = "";
            do
            {
                userDecision = DisplayOptions(cursorPos);
            } while (userDecision != "Search by value" && userDecision != "Search by range");

            try
            {
                if (userDecision == "Search by value")
                {
                    Console.Write("Enter Calories value: ");
                    var valueEntered = int.TryParse(Console.ReadLine(), out searchValue);
                    if (valueEntered) return searchValue;
                    throw new ArgumentException("Calories value must be a valid number.");
                }

                if (userDecision == "Search by range")
                {
                    Console.Write("Enter min value: ");
                    var minValueEntered = int.TryParse(Console.ReadLine(), out minValue);
                    Console.Write("Enter max value: ");
                    var maxValueEntered = int.TryParse(Console.ReadLine(), out maxValue);
                    if (minValueEntered && maxValueEntered) return searchValue;
                    throw new ArgumentException("Calories value must be a valid number.");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                throw;
            }

            return searchValue;
        }

        private static readonly string[] _searchByCaloriesOptions = { "Search by value", "Search by range" };
        private static int _currentLine = 0;

        public string DisplayOptions(int cursorPos)
        {
            
            Console.SetCursorPosition(0, cursorPos);
            for (int i = 0; i < _searchByCaloriesOptions.GetLength(0); i++)
            {
                if (i == _currentLine)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(_searchByCaloriesOptions[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                if (_currentLine == _searchByCaloriesOptions.GetLength(0) - 1)
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
                    _currentLine = _searchByCaloriesOptions.GetLength(0) - 1;
                }
                else
                {
                    _currentLine--;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                return _searchByCaloriesOptions[_currentLine];
            }
            return "";
        }
    }




}