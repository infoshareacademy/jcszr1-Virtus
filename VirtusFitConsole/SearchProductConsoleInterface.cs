using BLL;
using System;
using System.Collections.Generic;

namespace VirtusFitConsole
{
    class SearchProductConsoleInterface
    {
        private static readonly string[] SearchOptions = { "Search by name", "Search by calories", "Search by fat", "Search by carbohydrates", "Search by protein" };
        private static int _currentLine;

        private string DisplayOptions(int cursorPos)
        {

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, cursorPos);
            for (int i = 0; i < SearchOptions.GetLength(0); i++)
            {
                if (i == _currentLine)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(SearchOptions[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                if (_currentLine == SearchOptions.GetLength(0) - 1)
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
                    _currentLine = SearchOptions.GetLength(0) - 1;
                }
                else
                {
                    _currentLine--;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                return SearchOptions[_currentLine];
            }
            return "";
        }

        private readonly SearchProductLogic _newSearch = new SearchProductLogic();
        private readonly GetDataFromUserConsole _newData = new GetDataFromUserConsole();
        public void SearchProductInterface(List<Product> productList)
        {
            Console.WriteLine();
            Console.WriteLine("Choose the type of searching:");
            var cursorPos = Console.CursorTop;
            string userDecision;
            do
            {
                userDecision = DisplayOptions(cursorPos);
            } while (userDecision != "Search by name" && userDecision != "Search by calories" && userDecision != "Search by fat" && userDecision != "Search by carbohydrates" && userDecision != "Search by protein");

            switch (userDecision)
            {
                case "Search by name":
                    {
                        _newData.GetDataFromUser(out var productName);
                        var searchList = _newSearch.SearchByName(productList, productName);
                        DisplayProductList.DisplayList(searchList);
                        break;
                    }
                case "Search by calories":
                    {
                        var searchValue = _newData.GetDataFromUser(out var minValue, out var maxValue);
                        if (searchValue != 0)
                        {
                            var searchList = _newSearch.SearchByCalories(productList, searchValue);
                            DisplayProductList.DisplayList(searchList);
                        }
                        else
                        {
                            var searchList = _newSearch.SearchByCalories(productList, minValue, maxValue);
                            DisplayProductList.DisplayList(searchList);
                        }
                        break;
                    }
                case "Search by fat":
                    {
                        string macro = "fat";
                        var searchValue = _newData.GetDataFromUser(macro, out var minValue, out var maxValue);
                        if (searchValue != 0)
                        {
                            var searchList = _newSearch.SearchByFat(productList, searchValue);
                            DisplayProductList.DisplayList(searchList);
                        }
                        else
                        {
                            var searchList = _newSearch.SearchByFat(productList, minValue, maxValue);
                            DisplayProductList.DisplayList(searchList);
                        }
                        break;
                    }
                case "Search by carbohydrates":
                {
                    string macro = "carbohydrates";
                    var searchValue = _newData.GetDataFromUser(macro, out var minValue, out var maxValue);
                    if (searchValue != 0)
                    {
                        var searchList = _newSearch.SearchByCarbohydrates(productList, searchValue);
                        DisplayProductList.DisplayList(searchList);
                    }
                    else
                    {
                        var searchList = _newSearch.SearchByCarbohydrates(productList, minValue, maxValue);
                        DisplayProductList.DisplayList(searchList);
                    }
                    break;
                }
                case "Search by protein":
                {
                    string macro = "protein";
                    var searchValue = _newData.GetDataFromUser(macro, out var minValue, out var maxValue);
                    if (searchValue != 0)
                    {
                        var searchList = _newSearch.SearchByProtein(productList, searchValue);
                        DisplayProductList.DisplayList(searchList);
                    }
                    else
                    {
                        var searchList = _newSearch.SearchByProtein(productList, minValue, maxValue);
                        DisplayProductList.DisplayList(searchList);
                    }
                    break;
                }
            }
        }
    }
}