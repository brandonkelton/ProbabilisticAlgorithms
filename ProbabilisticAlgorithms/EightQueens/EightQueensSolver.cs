﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilisticAlgorithms.EightQueens
{
    public class EightQueensSolver
    {
        public readonly int BoardColumnOffset = 4;
        public readonly int BoardRowOffset = 2;

        private readonly int[,] Board = { { 0, 0, 0, 0, 0, 0, 0, 0 },
                                          { 0, 0, 0, 0, 0, 0, 0, 0 },
                                          { 0, 0, 0, 0, 0, 0, 0, 0 },
                                          { 0, 0, 0, 0, 0, 0, 0, 0 },
                                          { 0, 0, 0, 0, 0, 0, 0, 0 },
                                          { 0, 0, 0, 0, 0, 0, 0, 0 },
                                          { 0, 0, 0, 0, 0, 0, 0, 0 },
                                          { 0, 0, 0, 0, 0, 0, 0, 0 } };

        public TimeSpan LastRunTime { get; private set; }

        public void DrawBoard()
        {
            Console.Clear();
            for (int row=0; row < 8; row++)
            {
                for (int col=0; col < 8; col++)
                {
                    Console.SetCursorPosition((col + 1) * BoardColumnOffset, (row + 1) * BoardRowOffset);
                    if (Board[row, col] == 0) Console.ForegroundColor = ConsoleColor.White;
                    else Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(Board[row, col]);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public PlacementResult SetQueenState(int row, int col)
        {
            var placementResult = new PlacementResult();

            if (GetQueenState(row, col) == 0)
            {
                if (!IsSafeInColumn(col)) placementResult.ResultTypes.Add(PlacementResultType.ColumnViolation);
                if (!IsSafeInRow(row)) placementResult.ResultTypes.Add(PlacementResultType.RowViolation);
                if (!IsSafeDiagonally(row, col)) placementResult.ResultTypes.Add(PlacementResultType.DiagonalViolation);
            }

            if (placementResult.IsValid) Board[row, col] = Board[row, col] == 0 ? 1 : 0;

            return placementResult;
        }

        public int GetQueenState(int row, int col)
        {
            return Board[row, col];
        }

        public int QueenCount => (from int val in Board where val == 1 select val).Count();

        public bool Solve()
        {
            var startTime = DateTime.Now;
            var isSolved = FindSolution();
            var stopTime = DateTime.Now;

            LastRunTime = stopTime - startTime;

            return isSolved;
        }

        private bool FindSolution(int queen = 1, int col = 0)
        {
            if (QueenCount == 8) return true;
            if (col > 7) return false;

            for (int row = 0; row < 8; row++)
            {
                if (IsSafeInRow(row))
                {
                    if (IsSafeInColumn(col))
                    {
                        if (IsSafeDiagonally(row, col))
                        {
                            Board[row, col] = 1;
                            if (FindSolution(queen + 1, col + 1)) return true;
                            Board[row, col] = 0;
                        }
                    } 
                    else
                    {
                        if (FindSolution(queen, col + 1)) return true;
                        Board[row, col] = 0;
                    }
                }
            }

            return false;
        }

        private bool IsSafeInRow(int row)
        {
            return (from int val in GetRow(Board, row) where val == 1 select val).Count() == 0;
        }

        private bool IsSafeInColumn(int col)
        {
            return (from int val in GetColumn(Board, col) where val == 1 select val).Count() == 0;
        }

        private bool IsSafeDiagonally(int row, int col)
        {
            // Diagonal Up Left Side
            for (int i = row, j = col; i >= 0 && j >= 0; i--, j--)
                if (Board[i, j] == 1)
                    return false;

            // Diagonal Up Right Side
            for (int i = row, j = col; i >= 0 && j < 7; i--, j++)
                if (Board[i, j] == 1)
                    return false;

            // Diagonal Down Left Side
            for (int i = row, j = col; j >= 0 && i < 7; i++, j--)
                if (Board[i, j] == 1)
                    return false;

            // Diagonal Down Right Side
            for (int i = row, j = col; j < 7 && i < 7; i++, j++)
                if (Board[i, j] == 1)
                    return false;

            return true;
        }

        private int[] GetColumn(int[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        private int[] GetRow(int[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}
