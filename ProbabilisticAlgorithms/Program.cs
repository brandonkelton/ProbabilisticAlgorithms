using ProbabilisticAlgorithms.ComputingPi;
using ProbabilisticAlgorithms.PrimeNumberTest;
using System;
using System.Collections.Generic;

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
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Press a number, or <Escape> to exit: ");

            var result = Console.ReadKey();
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
                default:
                    break;
            }

            return true;
        }

        static void RunPiComputer()
        {
            Console.WriteLine();
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

        private static void ReturnToMenuWait()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
