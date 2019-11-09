//Write a program to find all possible placements of 8 queens on a chessboard, so that no two queens can attack each other
//EXample:
// Q x x x x x x x
// x x x x x x Q x
// x x x x Q x x x
// x x x x x x x Q
// x Q x x x x x x
// x x x Q x x x x
// x x x x x Q x x
// x x Q x x x x x

using System;
using System.Collections.Generic;

namespace _06_eight_queens
{
    class Program
    {
        const int Size = 8;
        static bool[,] chessboard = new bool[Size, Size];
        //The Rows are 8, numbered from 0 to 7.
        //The Columns are 8, numbered from 0 to 7.
        //The left diagonals are 15, numbered from -7 to 7, calculated by formula: leftDiag = col - row.
        //The right diagonals are 15, numbered from 0 to 14 by the formula: rightDiag = col + row.
        //Following the definitions above for our example the queen {4, 1} occupies the row 4, column 1, left diagonal -3 and right diagonal 5.
        static HashSet<int> attackedRows = new HashSet<int>();
        static HashSet<int> attackedColumns = new HashSet<int>();
        static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        static HashSet<int> attackedRightDiagonals = new HashSet<int>();

        static int SolutionsFound = 0;

        static void Main()
        {
            PlaceQueen(0);            
        }

        private static void PlaceQueen(int row)
        {
            if (row == Size)
            {
                PrintSolution();
                return;
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkAttackedPositions(row, col);
                        PlaceQueen(row + 1);
                        UnmarkAttackedPositions(row, col);
                    }
                }
            }
        }

        private static bool CanPlaceQueen(int row, int col)
        {
            bool occupied = attackedRows.Contains(row) ||
                attackedColumns.Contains(col) ||
                attackedLeftDiagonals.Contains(col - row) ||
                attackedRightDiagonals.Contains(col + row);
            return !occupied;
        }

        private static void MarkAttackedPositions(int row, int col)
        {
            attackedRows.Add(row);
            attackedColumns.Add(col);
            attackedLeftDiagonals.Add(col - row);
            attackedRightDiagonals.Add(col + row);
            chessboard[row, col] = true;
        }

        private static void UnmarkAttackedPositions(int row, int col)
        {
            attackedRows.Remove(row);
            attackedColumns.Remove(col);
            attackedLeftDiagonals.Remove(col - row);
            attackedRightDiagonals.Remove(col + row);
            chessboard[row, col] = false;
        }

        private static void PrintSolution()
        {
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    var symbol = chessboard[i, j] ? '*' : '-';
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            SolutionsFound++;
            Console.WriteLine(SolutionsFound);
        }
    }
}
