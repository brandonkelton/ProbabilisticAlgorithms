using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAlgorithms.PrimeNumberTest
{
    public class PrimeTestResult
    {
        public long PotentiallyPrimeNumber { get; set; }
        public bool IsPrime { get; set; }
        public bool? MatchesSpecification { get; set; }
    }
}
