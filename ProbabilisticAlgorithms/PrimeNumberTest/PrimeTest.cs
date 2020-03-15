using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilisticAlgorithms.PrimeNumberTest
{
    public class PrimeTest
    {
        public int PotentiallyPrimeNumber { get; private set; }

        public PrimeTest(int potentialPrimeNumber)
        {
            if (potentialPrimeNumber < 1) throw new Exception("Number must be greater than 0");
            PotentiallyPrimeNumber = potentialPrimeNumber;
        }

        public bool IsPrime(int[] testNumbers)
        {
            if (PotentiallyPrimeNumber == 1) return false;
            if (PotentiallyPrimeNumber == 2) return true;

            if (testNumbers.Length == 0)
            {
                return GetIsPrimeAutomatically();
            }

            return GetIsPrimeWithTestNumbers(testNumbers);
        }

        private bool GetIsPrimeAutomatically()
        {
            for (int i = 2; i <= Math.Sqrt(PotentiallyPrimeNumber); i++)
            {
                if (PotentiallyPrimeNumber % i == 0) return false;
            }

            return true;
        }

        private bool GetIsPrimeWithTestNumbers(int[] testNumbers)
        {
            foreach (var testNumber in testNumbers)
            {
                if (PotentiallyPrimeNumber % testNumber == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
