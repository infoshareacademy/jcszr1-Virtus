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
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Witaj w programie do tworzenia planu dietetycznego!");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine();
            int userInput = 1;
            do
            {
                userInput = DisplayMenu();
                new ExecuteOption(userInput);
            } while (userInput != 6);
        }



        public static bool BetweenRanges(int a, int b, int number)
        {
            return (a <= number && number <= b);
        }
        static public int DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine();
            Console.WriteLine("1. Wyszukaj produkt po kaloryczności");
            Console.WriteLine("2. Wyszukaj produkt po nazwie");
            Console.WriteLine("3. Zawansowane wyszukiwanie produktu");
            Console.WriteLine("4. Wprowadź nowy produkt");
            Console.WriteLine("5. Stwórz plan dietetyczny");
            Console.WriteLine("6. Zakończ");
            Console.WriteLine();

            int _result=1;
            bool _itsNumeric;
            _itsNumeric = false;

            while (_itsNumeric == false)
            {
                _itsNumeric = int.TryParse(Console.ReadLine().ToString(), out _result);
            }

            while (!BetweenRanges(1, 6, _result))
            {
                _itsNumeric = int.TryParse(Console.ReadLine().ToString(), out _result);
            }

            return _result;
        }

        private class ExecuteOption
        {
            private int userInput;

            public ExecuteOption(int userInput)
            {
                switch (userInput)
                {
                    case 1:
                        new SearchProductConsoleInterface();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
