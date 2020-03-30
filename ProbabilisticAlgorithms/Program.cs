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
            Console.WriteLine("Prime Number Test");
            Console.WriteLine();

            bool isAutomatic;
            while (true)
            {
                Console.WriteLine();
                Console.Write("(A)utomatically generate number list, or (m)anually input numbers (A/m)? ");
                var response = Console.ReadKey();
                if (response.Key == ConsoleKey.A)
                {
                    isAutomatic = true;
                    break;
                }
                else if (response.Key == ConsoleKey.M)
                {
                    isAutomatic = false;
                    break;
                }
            }

            Number[] Numbers;
            var primeTestResults = new List<PrimeTestResult>();

            if (isAutomatic)
            {
                int numberCount;
                while (true)
                {
                    Console.WriteLine();
                    Console.Write("How many non-prime numbers would you like to test (Test contains 30 Prime Numbers)? ");
                    var response = Console.ReadLine();
                    if (int.TryParse(response, out numberCount)) break;
                    Console.WriteLine("Invalid number!");
                }
                var numberGen = new NumberGenerator(numberCount);
                numberGen.BuildList();
                Numbers = numberGen.Numbers.ToArray();
            } 
            else
            {
                Console.WriteLine();
                Console.WriteLine("Input one or more numbers for which you would like to run a prime number test:");
                Console.WriteLine();

                Numbers = GetNumbers();
                if (Numbers.Length == 0)
                {
                    Console.WriteLine("You didn't input any numbers, so I'm quitting.");
                    return;
                }
            }

            Console.WriteLine();
            Console.WriteLine();            

            foreach (var number in Numbers)
            {
                Console.Write($"Checking {number.Value}... ");

                PrimeTest primeTest;
                try
                {
                    primeTest = new PrimeTest(number.Value);
                    var primeTestResult = new PrimeTestResult
                    {
                        PotentiallyPrimeNumber = number.Value,
                        IsPrime = primeTest.IsPrime(),
                        MatchesSpecification = number.IsPrime.HasValue ? (primeTest.IsPrime() == number.IsPrime.Value) as bool? : null
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
                var matchesSpecification = result.MatchesSpecification.HasValue 
                    ? result.MatchesSpecification.Value 
                        ? "  CORRECT!" 
                        : "  INCORRECT!" 
                    : "";

                var resultText = result.IsPrime
                        ? $"The number {result.PotentiallyPrimeNumber} is prime." + matchesSpecification
                        : $"The number {result.PotentiallyPrimeNumber} is NOT prime." + matchesSpecification;

                Console.WriteLine(resultText);
            }

            ReturnToMenuWait();
        }

        private static Number[] GetNumbers()
        {
            Console.WriteLine();
            Console.WriteLine("You will be prompted to input numbers until you press ENTER with no input.");
            Console.WriteLine();

            var numbers = new List<long>();
            while (true)
            {
                Console.Write("Input a number: ");
                var input = Console.ReadLine();
                if (input == "") break;

                long number;
                if (long.TryParse(input, out number))
                {
                    numbers.Add(number);
                }
                else
                {
                    Console.WriteLine("Invalid Number!");
                }
            }

            return numbers.Select(n => new Number(n)).ToArray();
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

            Console.WriteLine("Add between 1 and 7 queens to the board. (At least 1 queen is required.)");
            Console.WriteLine("Use the ARROW KEYS to move through the board.");
            Console.WriteLine("Use SPACEBAR to add or remove a queen from the board.");
            Console.WriteLine("Press ENTER when done.");

            InteractWithUserForInitialQueenPlacement(eightQueens);

            if (eightQueens.QueenCount == 0)
            {
                Console.SetCursorPosition(0, 12 * eightQueens.BoardRowOffset);
                ReturnToMenuWait();
                return;
            }

            var isSolved = eightQueens.Solve();

            eightQueens.DrawBoard();
            Console.WriteLine();
            Console.WriteLine();

            if (isSolved) Console.WriteLine($"A solution was found in {eightQueens.LastRunTime.TotalMilliseconds}ms. See above.");
            else Console.WriteLine("There is no solution.");

            Console.SetCursorPosition(0, 9 * eightQueens.BoardRowOffset);
            ReturnToMenuWait();
        }

        private static void InteractWithUserForInitialQueenPlacement(EightQueensSolver eightQueens)
        {
            var column = 0;
            var row = 0;

            var isAssigningQueens = true;
            var isShowingWarning = false;
            var stdOut = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            var memStream = new MemoryStream();
            var fakeWriter = new StreamWriter(memStream);
            Console.SetOut(fakeWriter);

            while (isAssigningQueens)
            {
                Console.SetCursorPosition((column + 1) * eightQueens.BoardColumnOffset, (row + 1) * eightQueens.BoardRowOffset);
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
                
                if (isShowingWarning)
                {
                    Console.SetOut(stdOut);
                    Console.SetCursorPosition(0, 0);
                    Console.Write(string.Join("", Enumerable.Repeat(" ", 80)));
                    Console.SetOut(fakeWriter);
                    isShowingWarning = false;
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
                    case ConsoleKey.Spacebar:
                        if (eightQueens.QueenCount == 7 && eightQueens.GetQueenState(row, column) == 0)
                        {
                            Console.SetOut(stdOut);
                            Console.SetCursorPosition(0, 0);
                            Console.Write("The number of queens may not exceed 7.");
                            Console.SetOut(fakeWriter);
                            isShowingWarning = true;
                        } 
                        else
                        {
                            var placementResult = eightQueens.SetQueenState(row, column);
                            if (!placementResult.IsValid)
                            {
                                Console.SetOut(stdOut);
                                Console.SetCursorPosition(0, 0);
                                Console.Write(placementResult.Message);
                                Console.SetOut(fakeWriter);
                                isShowingWarning = true;
                            }
                            else
                            {
                                var currentQueenState = eightQueens.GetQueenState(row, column);
                                if (currentQueenState == 1) Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.SetOut(stdOut);
                                Console.Write(currentQueenState);
                                Console.SetOut(fakeWriter);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (eightQueens.QueenCount == 0)
                        {
                            Console.SetOut(stdOut);
                            Console.SetCursorPosition(0, 0);
                            Console.Write("At least 1 queen is required.");
                            Console.SetOut(fakeWriter);
                            isShowingWarning = true;
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
