using ProbabilisticAlgorithms.ComputingPi;
using ProbabilisticAlgorithms.EightQueens;
using ProbabilisticAlgorithms.MonteCarloIntegration;
using ProbabilisticAlgorithms.PrimeNumberTest;
using ProbabilisticAlgorithms.SearchArray;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProbabilisticAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var prompt = Prompt();
                if (!prompt) break;
                Console.Clear();
            }
        }

        static bool Prompt()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("What project would you like to execute?");
            Console.WriteLine();
            Console.WriteLine("1) Compute Pi");
            Console.WriteLine("2) Prime Number Test");
            Console.WriteLine("3) Random Search of Array");
            Console.WriteLine("4) Monte Carlo Integration");
            Console.WriteLine("5) Eight Queens");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Press a number, or <Escape> to exit: ");

            var result = Console.ReadKey();

            Console.Clear();

            switch (result.Key)
            {
                case ConsoleKey.Escape:
                    return false;
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                case ConsoleKey.Oem1:
                    RunPiComputer();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                case ConsoleKey.Oem2:
                    RunPrimeTest();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                case ConsoleKey.Oem3:
                    RunSearchArray();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                case ConsoleKey.Oem4:
                    RunMonteCarloIntegration();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                case ConsoleKey.Oem5:
                    RunEightQueens();
                    break;
                default:
                    break;
            }

            return true;
        }

        static void RunPiComputer()
        {
            Console.WriteLine();
            Console.Write("How many Pi's would you like to generate (Non-number to exit)? ");
            var piPrompt = Console.ReadLine();

            int piCount;
            if (!int.TryParse(piPrompt, out piCount)) return;

            Console.Write("How many throws per Pi (Non-number to exit)? ");
            var throwPrompt = Console.ReadLine();

            int throwCount;
            if (!int.TryParse(throwPrompt, out throwCount)) return;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Generating...");
            Console.WriteLine();
            Console.WriteLine();

            while (piCount > 0)
            {
                var computer = new PiComputer(throwCount);
                var pi = computer.GetPi();
                Console.WriteLine($"The value of Pi is: {pi}");
                piCount--;
            }

            ReturnToMenuWait();
        }

        static void RunPrimeTest()
        {
            Console.WriteLine();
            Console.WriteLine("Input one or more numbers for which you would like to run a prime number test...");
            Console.WriteLine();
            var potentiallyPrimeNumbers = GetNumbers();
            if (potentiallyPrimeNumbers.Length == 0)
            {
                Console.WriteLine("You didn't input any numbers, so I'm quitting.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Input one or more numbers you would like to use to test against your potentially prime number(s).");
            Console.WriteLine("To run an automatic test, do NOT input any test numbers.");
            var testNumbers = GetNumbers();

            Console.WriteLine();
            Console.WriteLine();

            var primeTestResults = new List<PrimeTestResult>();

            foreach (var potentiallyPrimeNumber in potentiallyPrimeNumbers)
            {
                Console.Write($"Checking {potentiallyPrimeNumber}... ");

                PrimeTest primeTest;
                try
                {
                    primeTest = new PrimeTest(potentiallyPrimeNumber);
                    var primeTestResult = new PrimeTestResult
                    {
                        PotentiallyPrimeNumber = potentiallyPrimeNumber,
                        IsPrime = primeTest.IsPrime(testNumbers)
                    };
                    primeTestResults.Add(primeTestResult);
                    Console.WriteLine("Done -> Recording results");
                } 
                catch
                {
                    Console.WriteLine("Skipping -> Number must be greater than 1!");
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            foreach (var result in primeTestResults)
            {
                var resultText = result.IsPrime
                        ? $"The number {result.PotentiallyPrimeNumber} is prime."
                        : $"The number {result.PotentiallyPrimeNumber} is NOT prime.";

                Console.WriteLine(resultText);
            }

            ReturnToMenuWait();
        }

        private static int[] GetNumbers()
        {
            Console.WriteLine();
            Console.WriteLine("You will be prompted to input numbers until you press <Enter> with no input.");
            Console.WriteLine();

            var numbers = new List<int>();
            while (true)
            {
                Console.Write("Input a number: ");
                var input = Console.ReadLine();
                if (input == "") break;

                int number;
                if (int.TryParse(input, out number))
                {
                    numbers.Add(number);
                }
                else
                {
                    Console.WriteLine("Invalid Number!");
                }
            }

            return numbers.ToArray();
        }

        private static void RunSearchArray()
        {
            Console.WriteLine();

            int searchCount = 100;
            while (true)
            {
                Console.Write("How many searches should be performed (default: 100)? ");
                var searchCountString = Console.ReadLine();
                if (searchCountString == "") break;
                if (int.TryParse(searchCountString, out searchCount)) break;
                Console.WriteLine();
                Console.WriteLine("Invalid Number!");
            }

            var results = new List<SearchResult>(searchCount);
            var search = new Search();
            search.BuildArray();

            for (int i=0; i<searchCount; i++)
            {
                var searchValue = search.GetRandomValueFromArray();
                results.Add(search.AttemptRandomSearch(searchValue));
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("SEARCH RESULTS");
            Console.WriteLine("--------------------------");
            Console.WriteLine();

            Console.WriteLine($"Number of searches:\t\t{searchCount}");
            Console.WriteLine($"Limit of guesses per search:\t{search.GuessLimit}");
            Console.WriteLine($"Average number of comparisons: \t{results.Average(r => r.ComparisonCount)}");
            Console.WriteLine($"Number of successful searches:\t{results.Count(r => r.IsSuccessful)}");
            Console.WriteLine($"Number of failed searches:\t{results.Count(r => !r.IsSuccessful)}");

            ReturnToMenuWait();
        }

        private static void RunMonteCarloIntegration()
        {
            Console.WriteLine();

            var function = FunctionType.Sine;
            var correctOptionSelection = false;
            while (!correctOptionSelection)
            {
                Console.Write("Select function type - (S)ine, (C)osine, (T)angent (default: Sine)? ");
                var selection = Console.ReadKey();
                switch (selection.Key)
                {
                    case ConsoleKey.S:
                        function = FunctionType.Sine;
                        correctOptionSelection = true;
                        break;
                    case ConsoleKey.C:
                        function = FunctionType.Cosine;
                        correctOptionSelection = true;
                        break;
                    case ConsoleKey.T:
                        function = FunctionType.Tangent;
                        correctOptionSelection = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid Function!");
                        break;
                }
            }

            Console.WriteLine();

            var randomPointCount = 100_000_000;
            while (true)
            {
                Console.Write("How many random points should be applied (default: 100,000,000)? ");
                var dartThrowString = Console.ReadLine();
                if (dartThrowString == "") break;
                if (int.TryParse(dartThrowString, out randomPointCount)) break;
                Console.WriteLine();
                Console.WriteLine("Invalid Number!");
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"Using Monte Carlo Mean Method to integrate function {function.ToDescription()} from 0 to 1...");
            var integrationByMean = new Integration();
            var resultOfIntegrationByMean = integrationByMean.GetIntegrationByRandomNumberMean(FunctionType.Sine, randomPointCount);

            Console.WriteLine($"Using Monte Carlo Dart Method to integrate function {function.ToDescription()}...");
            var integrationByDart = new Integration();
            var resultOfIntegrationByDart = integrationByDart.GetIntegrationByDartThrow(FunctionType.Sine, randomPointCount);

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"Result of Integration By Mean: {resultOfIntegrationByMean}");
            Console.WriteLine($"Result of Integration By Dart: {resultOfIntegrationByDart}");

            ReturnToMenuWait();
        }

        private static void RunEightQueens()
        {
            var eightQueens = new EightQueensSolver();
            eightQueens.DrawBoard();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Add between 1 and 7 queens to the board. At least 1 queen is required.");
            Console.WriteLine("Use the ARROW KEYS to move through the board.");
            Console.WriteLine("Use SPACEBAR or ENTER to add or remove a queen from the board.");
            Console.WriteLine("Press ESCAPE when done.");

            InteractWithUserForInitialQueenPlacement(eightQueens);

            if (eightQueens.QueenCount == 0)
            {
                Console.SetCursorPosition(0, 12 * eightQueens.BoardRowOffset);
                ReturnToMenuWait();
                return;
            }

            var result = eightQueens.Solve();

            Console.SetCursorPosition(0, 12 * eightQueens.BoardRowOffset);
            ReturnToMenuWait();
        }

        private static void InteractWithUserForInitialQueenPlacement(EightQueensSolver eightQueens)
        {
            var column = 0;
            var row = 0;

            var isAssigningQueens = true;
            var isShowingMaxQueenWarning = false;
            var isEscapePressedOnce = false;
            var stdOut = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            var memStream = new MemoryStream();
            var fakeWriter = new StreamWriter(memStream);
            Console.SetOut(fakeWriter);

            while (isAssigningQueens)
            {
                Console.SetCursorPosition((column + 1) * eightQueens.BoardColumnOffset, (row + 1) * eightQueens.BoardRowOffset);
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Escape && isEscapePressedOnce)
                {
                    break;
                }
                else if (isEscapePressedOnce || isShowingMaxQueenWarning)
                {
                    Console.SetOut(stdOut);
                    Console.SetCursorPosition(0, 0);
                    Console.Write(string.Join("", Enumerable.Repeat(" ", 80)));
                    Console.SetOut(fakeWriter);
                    isEscapePressedOnce = false;
                    isShowingMaxQueenWarning = false;
                    Console.SetCursorPosition((column + 1) * eightQueens.BoardColumnOffset, (row + 1) * eightQueens.BoardRowOffset);
                }

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        column = column > 0 ? --column : column;
                        break;
                    case ConsoleKey.UpArrow:
                        row = row > 0 ? --row : row;
                        break;
                    case ConsoleKey.RightArrow:
                        column = column < 7 ? ++column : column;
                        break;
                    case ConsoleKey.DownArrow:
                        row = row < 7 ? ++row : row;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        if (eightQueens.QueenCount == 7 && eightQueens.GetQueenState(column, row) == 0)
                        {
                            Console.SetOut(stdOut);
                            Console.SetCursorPosition(0, 0);
                            Console.Write("The number of queens may not exceed 7.");
                            Console.SetOut(fakeWriter);
                            isShowingMaxQueenWarning = true;
                        } 
                        else
                        {
                            eightQueens.SetQueen(column, row);
                            Console.SetOut(stdOut);
                            Console.Write(eightQueens.GetQueenState(column, row));
                            Console.SetOut(fakeWriter);
                        }
                        break;
                    case ConsoleKey.Escape:
                        if (eightQueens.QueenCount == 0)
                        {
                            Console.SetOut(stdOut);
                            Console.SetCursorPosition(0, 0);
                            Console.Write("At least 1 queen is required. Press ESCAPE again to cancel program.");
                            Console.SetOut(fakeWriter);
                            isEscapePressedOnce = true;
                        } else
                        {
                            isAssigningQueens = false;
                        }
                        break;
                }
            }

            fakeWriter.Dispose();
            memStream.Dispose();
            Console.SetOut(stdOut);
        }

        private static void ReturnToMenuWait()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
