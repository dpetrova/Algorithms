//Finding All Paths in a Labyrinth
//We are given a labyrinth represented as matrix of cells of size M x N
//Empty cells(-) are passable, the others(*) are not
//We start from the top left corner and can move in all 4 directions
//We want to find all paths to the exit, marked 'e'

using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_all_paths_in_labyrinth
{
    class Program
    {
        private static char[,] labyrinth =
        {
            {'-', '-', '-', '*', '-', '-', '-'},
            {'*', '*', '-', '*', '-', '*', '-'},
            {'-', '-', '-', '-', '-', '-', '-'},
            {'-', '*', '*', '*', '*', '*', '-'},
            {'-', '-', '-', '-', '-', '-', 'e'}
        };
        private static List<char> path = new List<char>();

        static void Main()
        {
            FindPath(0, 0, 'S');
        }

        private static void FindPath(int row, int col, char direction)
        {
            if (!IsPassable(row, col))
            {
                return;
            }

            path.Add(direction);

            if (IsExit(row, col))
            {
                PrintPath();
            }
            else
            {
                //pre-actions
                MarkVisitedCell(row, col);

                //recursive calls
                FindPath(row, col - 1, 'L'); //left
                FindPath(row, col + 1, 'R'); //right
                FindPath(row - 1, col, 'U'); //up
                FindPath(row + 1, col, 'D'); //down

                //post-actions
                UnmarkVisitedCell(row, col);
            }

            path.RemoveAt(path.Count - 1);
            
        }

        private static void MarkVisitedCell(int row, int col)
        {
            labyrinth[row, col] = 'x';
        }

        private static void UnmarkVisitedCell(int row, int col)
        {
            labyrinth[row, col] = '-';
        }        

        private static bool IsPassable(int row, int col)
        {
            //check labyrinth borders
            if (row < 0 || row > labyrinth.GetLength(0) - 1 || col < 0 || col > labyrinth.GetLength(1) - 1) return false;
            //check if cell is wall
            else if (labyrinth[row, col] == '*') return false;
            //check if cell is visited already
            else if (labyrinth[row, col] == 'x') return false;
            else return true;
        }

        private static bool IsExit(int row, int col)
        {
            return labyrinth[row, col] == 'e';
        }

        private static void PrintPath()
        {
            //print as remove first char (S -> Start)            
            Console.WriteLine(string.Join(string.Empty, path.Skip(1)));
        }
    }
}
