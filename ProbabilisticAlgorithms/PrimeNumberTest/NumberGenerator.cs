using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAlgorithms.PrimeNumberTest
{
    /*    Prime Number Source
     * 
     *    https://en.wikipedia.org/wiki/List_of_prime_numbers
     */

    public class NumberGenerator
    {
        private readonly Random _randomGenerator = new Random();
        private readonly int CompositeNumberCount;

        public readonly List<Number> Numbers = new List<Number>();

        public NumberGenerator(int compositeNumberCount)
        {
            CompositeNumberCount = compositeNumberCount;
        }

        public void BuildList()
        {
            // 30 Prime Numbers from source above

            Numbers.Add(new Number(8589935681, true));
            Numbers.Add(new Number(3010349, true));
            Numbers.Add(new Number(5879, true));
            Numbers.Add(new Number(6247, true));
            Numbers.Add(new Number(6863, true));
            Numbers.Add(new Number(7883, true));
            Numbers.Add(new Number(4943, true));
            Numbers.Add(new Number(7039, true));
            Numbers.Add(new Number(5003, true));
            Numbers.Add(new Number(7001, true));
            Numbers.Add(new Number(641, true));
            Numbers.Add(new Number(967, true));
            Numbers.Add(new Number(73, true));
            Numbers.Add(new Number(11, true));
            Numbers.Add(new Number(61, true));
            Numbers.Add(new Number(541, true));
            Numbers.Add(new Number(7919, true));
            Numbers.Add(new Number(17180131327, true));
            Numbers.Add(new Number(54018521, true));
            Numbers.Add(new Number(859433, true));
            Numbers.Add(new Number(37156667, true));
            Numbers.Add(new Number(47, true));
            Numbers.Add(new Number(797161, true));
            Numbers.Add(new Number(2147483647, true));
            Numbers.Add(new Number(6972593, true));
            Numbers.Add(new Number(24036583, true));
            Numbers.Add(new Number(32582657, true));
            Numbers.Add(new Number(7, true));
            Numbers.Add(new Number(59, true));
            Numbers.Add(new Number(1103, true));

            // Add composite numbers
            for (int i=0; i<CompositeNumberCount / 3; i++)
            {
                var randomNumber1 = _randomGenerator.Next();
                var randomNumber2 = _randomGenerator.Next();
                long num = randomNumber1 * randomNumber2;
                Numbers.Add(new Number(num, false));
            }

            // Add numbers multiplied by 5
            for (int i = 0; i < CompositeNumberCount / 3; i++)
            {
                var randomNumber = _randomGenerator.Next();
                long num = randomNumber * 5;
                Numbers.Add(new Number(num, false));
            }

            // Add numbers multiplied by 2
            for (int i = 0; i < CompositeNumberCount / 3; i++)
            {
                var randomNumber = _randomGenerator.Next();
                long num = randomNumber * 2;
                Numbers.Add(new Number(num, false));
            }
        }
    }
}
