using System;
using System.Collections.Generic;

namespace _07_eight_queens_v2_
{
    class Program
    {
        private const int Size = 8;
        static int[,] chessboard = new int[Size, Size];
        static HashSet<int> attackedRows = new HashSet<int>();
        static HashSet<int> attackedColumns = new HashSet<int>();
        //static bool[] attackedRows = new bool[Size];
        //static bool[] attackedColumns = new bool[Size];


        static void Main()
        {
            Solve(0);
        }

        private static void Solve(int row)
        {
            if(row == Size)
            {
                PrintSolution();
                return;
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if(CanPlaceQueen(row, col))
                    {
                        MarkAttackedFields(row, col);
                        Solve(row + 1);
                        UnmarkAttackedFields(row, col);
                    }
                }
            }
        }

        private static void PrintSolution()
        {
            for (int row = 0; row < chessboard.GetLength(0); row++)
            {
                for (int col = 0; col < chessboard.GetLength(1); col++)
                {
                    var symbol = chessboard[row, col] == 1 ? '*' : '-';
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
            Console.WriteLine();            
        }

        private static void UnmarkAttackedFields(int row, int col)
        {
            chessboard[row, col] = 0;
            attackedRows.Remove(row);
            attackedColumns.Remove(col);
            //attackedRows[row] = false;
            //attackedColumns[col] = false;
        }

        private static void MarkAttackedFields(int row, int col)
        {
            chessboard[row, col] = 1;
            attackedRows.Add(row);
            attackedColumns.Add(col);
            //attackedRows[row] = true;
            //attackedColumns[col] = true;
        }

        private static bool CanPlaceQueen(int row, int col)
        {
            //check rows
            if (attackedRows.Contains(row)) return false;
            //if (attackedRows[row]) return false;

            //check cols
            if (attackedColumns.Contains(col)) return false;
            //if (attackedColumns[col]) return false;

            //check left-up diagonal
            for (int i = 1; i < Size; i++)
            {
                int currentRow = row - i;
                int currentCol = col - i;
                //check constraints
                if(currentRow < 0 || currentRow > Size - 1 || currentCol < 0 || currentCol > Size - 1 )
                {
                    break;
                }

                if (chessboard[currentRow, currentCol] == 1) return false;
            }

            //check left-down diagonal
            for (int i = 1; i < Size; i++)
            {
                int currentRow = row + i;
                int currentCol = col - i;
                //check constraints
                if (currentRow < 0 || currentRow > Size - 1 || currentCol < 0 || currentCol > Size - 1)
                {
                    break;
                }

                if (chessboard[currentRow, currentCol] == 1) return false;
            }

            //check right-up diagonal
            for (int i = 1; i < Size; i++)
            {
                int currentRow = row - i;
                int currentCol = col + i;
                //check constraints
                if (currentRow < 0 || currentRow > Size - 1 || currentCol < 0 || currentCol > Size - 1)
                {
                    break;
                }

                if (chessboard[currentRow, currentCol] == 1) return false;
            }

            //check right-down diagonal
            for (int i = 1; i < Size; i++)
            {
                int currentRow = row + i;
                int currentCol = col + i;
                //check constraints
                if (currentRow < 0 || currentRow > Size - 1 || currentCol < 0 || currentCol > Size - 1)
                {
                    break;
                }

                if (chessboard[currentRow, currentCol] == 1) return false;
            }

            return true;
        }
    }
}
