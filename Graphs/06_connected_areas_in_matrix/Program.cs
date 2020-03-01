using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//We are given a matrix of letters of size N * M. Two cells are neighbor if they share a common wall.
//Write a program to find the connected areas of neighbor cells holding the same letter.

namespace _06_connected_areas_in_matrix
{
    class Program
    {
        private static char[,] matrix =
        {
            {'a', 'a', 'c', 'c', 'c', 'a', 'a', 'c'},
            {'b', 'a', 'a', 'a', 'a', 'c', 'c', 'c'},
            {'b', 'a', 'a', 'b', 'a', 'c', 'c', 'c'},
            {'b', 'b', 'd', 'a', 'a', 'c', 'c', 'c'},
            {'c', 'c', 'd', 'c', 'c', 'c', 'c', 'c'},
            {'c', 'c', 'd', 'c', 'c', 'c', 'c', 'c'}
        };

        static bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];
        static Dictionary<char, int> areas = new Dictionary<char, int>();

        static void Main()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!visited[i, j])
                    {
                        if (!areas.ContainsKey(matrix[i, j]))
                        {
                            areas[matrix[i, j]] = 0;
                        }

                       TraverseMatrix(i, j, matrix[i, j]);
                       areas[matrix[i, j]]++;
                    }
                }
            }

            foreach (var symbol in areas.Keys.OrderBy(x => x))
            {
                Console.WriteLine($"Letter '{symbol}' -> {areas[symbol]}");
            }
        }

        private static void TraverseMatrix(int row, int col, char symbol)
        {
            if (row < 0 || 
                row >= matrix.GetLength(0) || 
                col < 0 || 
                col >= matrix.GetLength(1) || 
                visited[row, col] == true || 
                matrix[row, col] != symbol)
            {
                return;
            }
            else
            {
                visited[row, col] = true;
                TraverseMatrix(row - 1, col, symbol);
                TraverseMatrix(row + 1, col, symbol);
                TraverseMatrix(row, col - 1, symbol);
                TraverseMatrix(row, col + 1, symbol);
            }
        }
    }
}
