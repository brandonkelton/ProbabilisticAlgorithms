using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProbabilisticAlgorithms.EightQueens
{
    public enum PlacementResultType
    {
        [Description("No Violation")]
        NoViolation,

        [Description("Column Violation")]
        ColumnViolation,

        [Description("Row Violation")]
        RowViolation,

        [Description("Diagonal Violation")]
        DiagonalViolation
    }
}
