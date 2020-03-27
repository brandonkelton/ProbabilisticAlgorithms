using System;
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

        public void SetupBoard(int queenCount)
        {
            int row = 0;
            int lastRowStart = 0;
            int col = 0;

            for (int i=0; i < queenCount; i++)
            {
                if (row > 7)
                {
                    lastRowStart++;
                    row = lastRowStart;
                }

                Board[row, col] = 1;

                row += 2;
                col++;
            }
        }

        public void DrawBoard()
        {
            Console.Clear();
            for (int row=0; row < 8; row++)
            {
                for (int col=0; col < 8; col++)
                {
                    Console.SetCursorPosition((col + 1) * BoardColumnOffset, (row + 1) * BoardRowOffset);
                    Console.Write(Board[row, col]);
                }
            }
        }

        public void SetQueen(int column, int row)
        {
            Board[row, column] = Board[row, column] == 0 ? 1 : 0;
        }

        public int GetQueenState(int column, int row)
        {
            return Board[row, column];
        }

        public int QueenCount => (from int val in Board where val == 1 select val).Count();

        public bool Solve()
        {
            

            return false;
        }
    }
}
