using System;


namespace VirtusFitConsole
{
    class GetDataFromUserConsole
    {
        public void GetDataFromUser(out string productName)
        {
            productName = GetSearchByNameData();
        }
        public int GetDataFromUser(out int minCaloriesValue, out int maxCaloriesValue)
        {
            var exactCaloriesValue = GetSearchByCaloriesData(out minCaloriesValue, out maxCaloriesValue);
            return exactCaloriesValue;
        }

        public double GetDataFromUser(string macro, out double minMacroValue, out double maxMacroValue)
        {
            var exactMacroValue = GetSearchByMacrosData(macro, out minMacroValue, out maxMacroValue);
            return exactMacroValue;
        }

        private string GetSearchByNameData()
        {
            Console.Write("Enter product name or a part of its name: ");
            var productName = Console.ReadLine();
            return productName;
        }

        private int GetSearchByCaloriesData(out int minValue, out int maxValue)
        {
            minValue = 0;
            maxValue = 0;
            Console.WriteLine("Would You like to search by exact Calories value or by range?");
            var cursorPos = Console.CursorTop;
            string userDecision;
            do
            {
                userDecision = DisplayOptions(cursorPos);
            } while (userDecision != "Search by value" && userDecision != "Search by range");

            do
            {
                try
                {
                    if (userDecision == "Search by value")
                    {
                        Console.Write("Enter Calories value: ");
                        var valueEntered = int.TryParse(Console.ReadLine(), out int searchValue);
                        if (valueEntered) return searchValue;
                        throw new ArgumentException("Calories value must be a valid number.");
                    }

                    if (userDecision == "Search by range")
                    {
                        Console.Write("Enter min value: ");
                        var minValueEntered = int.TryParse(Console.ReadLine(), out minValue);
                        Console.Write("Enter max value: ");
                        var maxValueEntered = int.TryParse(Console.ReadLine(), out maxValue);
                        if (minValueEntered && maxValueEntered) return 0;
                        throw new ArgumentException("Calories value must be a valid number.");
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to continue. Or ESC to leave.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape) Environment.Exit(0);
                }
            } while (true);
        }

        private double GetSearchByMacrosData(string macro, out double minValue, out double maxValue)
        {
            minValue = 0;
            maxValue = 0;
            Console.WriteLine($"Would You like to search by exact {macro} value or by range?");
            var cursorPos = Console.CursorTop;
            string userDecision;
            do
            {
                userDecision = DisplayOptions(cursorPos);
            } while (userDecision != "Search by value" && userDecision != "Search by range");

            do
            {
                try
                {
                    if (userDecision == "Search by value")
                    {
                        Console.Write("Enter Calories value: ");
                        var valueEntered = double.TryParse(Console.ReadLine(), out double searchValue);
                        if (valueEntered) return searchValue;
                        throw new ArgumentException("The value must be a valid number.");
                    }

                    if (userDecision == "Search by range")
                    {
                        Console.Write("Enter min value: ");
                        var minValueEntered = double.TryParse(Console.ReadLine(), out minValue);
                        Console.Write("Enter max value: ");
                        var maxValueEntered = double.TryParse(Console.ReadLine(), out maxValue);
                        if (minValueEntered && maxValueEntered) return 0;
                        throw new ArgumentException("The value must be a valid number.");
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to continue. Or ESC to leave.");
                    if (Console.ReadKey().Key == ConsoleKey.Escape) Environment.Exit(0);
                }
            } while (true);
        }

        private static readonly string[] SearchByValueOptions = { "Search by value", "Search by range" };
        private static int _currentLine;

        private string DisplayOptions(int cursorPos)
        {

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, cursorPos);
            for (int i = 0; i < SearchByValueOptions.GetLength(0); i++)
            {
                if (i == _currentLine)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(SearchByValueOptions[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                if (_currentLine == SearchByValueOptions.GetLength(0) - 1)
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
                    _currentLine = SearchByValueOptions.GetLength(0) - 1;
                }
                else
                {
                    _currentLine--;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                return SearchByValueOptions[_currentLine];
            }
            return "";
        }
    }




}