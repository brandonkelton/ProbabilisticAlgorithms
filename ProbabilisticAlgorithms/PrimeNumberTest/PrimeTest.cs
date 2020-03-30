using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilisticAlgorithms.PrimeNumberTest
{
    public class PrimeTest
    {
        public long PotentiallyPrimeNumber { get; private set; }

        public PrimeTest(long potentialPrimeNumber)
        {
            if (potentialPrimeNumber < 1) throw new Exception("Number must be greater than 0");
            PotentiallyPrimeNumber = potentialPrimeNumber;
        }

        public bool IsPrime()
        {
            if (PotentiallyPrimeNumber == 1) return false;
            if (PotentiallyPrimeNumber == 2) return true;
            return GetIsPrime();
        }

        private bool GetIsPrime()
        {
            for (long i = 2; i <= Math.Sqrt(PotentiallyPrimeNumber); i++)
            {
                if (PotentiallyPrimeNumber % i == 0) return false;
            }
            return true;
        }
    }
}
