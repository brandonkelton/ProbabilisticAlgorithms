using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAlgorithms.PrimeNumberTest
{
    public class Number
    {
        public long Value { get; private set; }

        public bool? IsPrime { get; private set; }

        public Number(long value, bool? isPrime = null)
        {
            Value = value;
            IsPrime = isPrime;
        }
    }
}
