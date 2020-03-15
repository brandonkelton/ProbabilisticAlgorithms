using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilisticAlgorithms.SearchArray
{
    public class Search
    {
        public readonly int GuessLimit = 5000;
        private Random _randomNumberGenerator = new Random();

        public int[] Array { get; set; } = null;

        public void BuildArray()
        {
            var hashSet = new HashSet<int>();
            for (int i=0; i<1000; i++)
            {
                while (!hashSet.Add(_randomNumberGenerator.Next(0, int.MaxValue))) ;
            }
            Array = hashSet.ToArray();
        }

        public int GetRandomValueFromArray()
        {
            if (Array == null) throw new Exception("Must call BuildArray()");
            return Array[_randomNumberGenerator.Next(0, 1000)];
        }

        public SearchResult AttemptRandomSearch(int value)
        {
            bool found = false;
            int currentIndex;
            for (currentIndex=0; currentIndex < GuessLimit; currentIndex++)
            {
                var index = new Random().Next(0, 1000);
                if (Array[index] == value)
                {
                    found = true;
                    break;
                }
            }

            return new SearchResult
            {
                Value = value,
                ComparisonCount = currentIndex,
                IsSuccessful = found
            };
        }
    }
}
