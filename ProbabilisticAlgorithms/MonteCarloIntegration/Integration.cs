using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilisticAlgorithms.MonteCarloIntegration
{
    public class Integration
    {
        private Random _randomNumberGenerator = new Random();

        public double GetIntegrationByRandomNumberMean(FunctionType functionType, int count)
        {
            var results = new List<double>(count);
            for (int i = 0; i < count; i++)
            {
                switch (functionType)
                {
                    case FunctionType.Sine:
                        results.Add(Math.Sin(_randomNumberGenerator.NextDouble()));
                        break;
                    case FunctionType.Cosine:
                        results.Add(Math.Cos(_randomNumberGenerator.NextDouble()));
                        break;
                    case FunctionType.Tangent:
                        results.Add(Math.Tan(_randomNumberGenerator.NextDouble()));
                        break;
                }
                
            }
            return results.Average(r => r);
        }

        public double GetIntegrationByDartThrow(FunctionType functionType, int throwCount)
        {
            var results = new List<bool>(throwCount);
            for (int i = 0; i < throwCount; i++)
            {
                var x = _randomNumberGenerator.NextDouble();
                var y = _randomNumberGenerator.NextDouble() * 2;  //Make a rectangle

                switch (functionType)
                {
                    case FunctionType.Sine:
                        var testSinY = Math.Sin(x);
                        results.Add(y < testSinY);
                        break;
                    case FunctionType.Cosine:
                        var testCosY = Math.Cos(x);
                        results.Add(y < testCosY);
                        break;
                    case FunctionType.Tangent:
                        var testTanY = Math.Tan(x);
                        results.Add(y < testTanY);
                        break;
                }
            }

            return (results.Count(r => r) / (double)results.Count()) * 2;
        }
    }
}
