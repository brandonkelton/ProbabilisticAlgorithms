using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilisticAlgorithms.ComputingPi
{
    public class PiComputer
    {
        private readonly int _attemptLimit;
        private readonly Random _randomGenerator = new Random();

        public PiComputer(int attemptLimit)
        {
            _attemptLimit = attemptLimit;
        }

        public double GetPi()
        {
            var hits = 0;

            for (int i = 0; i < _attemptLimit; i++)
            {
                var x = _randomGenerator.NextDouble();
                var y = _randomGenerator.NextDouble();

                if (Math.Pow(x, 2) + Math.Pow(y, 2) <= 1)
                {
                    hits++;
                }
            }

            return 4 * (hits / (double)_attemptLimit);
        }
    }
}
