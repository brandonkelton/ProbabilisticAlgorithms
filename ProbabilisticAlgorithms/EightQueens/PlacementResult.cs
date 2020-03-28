using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilisticAlgorithms.EightQueens
{
    public class PlacementResult
    {
        public bool IsValid => ResultTypes.Count == 0;

        public List<PlacementResultType> ResultTypes { get; private set; } = new List<PlacementResultType>();

        public string Message => string.Join(", ", ResultTypes.Select(r => r.ToDescription()));
    }
}
