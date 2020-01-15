//Let’s define a connected area in a matrix as an area of cells in which there is a path between every two cells. 
//Write a program to find all connected areas in a matrix
//Order the areas by size (in descending order) so that the largest area is printed first. 
//If several areas have the same size, order them by their position, first by the row, then by the column of the top-left corner. 
//So, if there are two connected areas with the same size, the one which is above and/or to the left of the other will be printed first
//On the first line, you will get the number of rows.
//On the second line, you will get the number of columns.
//The rest of the input will be the actual matrix

using System;
using System.Collections.Generic;

namespace _14_connected_areas_in_matrix
{
    class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }

    class Area : IComparable
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Size { get; set; }

        public int CompareTo(object other)
        {
            Area otherArea = (Area)other;

            if (this.Size != otherArea.Size)
            {
                return otherArea.Size.CompareTo(this.Size);
            }

            if (this.Row != otherArea.Row)
            {
                return this.Row.CompareTo(otherArea.Row);
            }

            return this.Col.CompareTo(otherArea.Col);
        }
    }

    class Program
    {
        private static char[,] matrix =
        {
            {'-', '-', '-', '*', '-', '-', '-', '*', '-'},
            {'-', '-', '-', '*', '-', '-', '-', '*', '-'},
            {'-', '-', '-', '*', '-', '-', '-', '*', '-'},
            {'-', '-', '-', '-', '*', '-', '*', '-', '-'}            
        };
        static SortedSet<Area> areas = new SortedSet<Area>();

        static void Main()
        {
            Cell unmarkedCell;

            while ((unmarkedCell = FindCell()) != null)
            {
                Area current = new Area { Row = unmarkedCell.Row, Col = unmarkedCell.Col, Size = 0 };
                int size = TraverseArea(current.Row, current.Col);
                current.Size = size;
                areas.Add(current);
            }

            Console.WriteLine($"Total areas found: {areas.Count}");
            int counter = 1;
            foreach (var area in areas)
            {
                Console.WriteLine($"Area #{counter++} at ({area.Row}, {area.Col}), size: {area.Size}");
            }
        }

        private static int TraverseArea(int row, int col)
        {
            if (row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1) || matrix[row, col] == '*' || matrix[row, col] == 'v')
            {
                return 0;
            }
            else
            {
                matrix[row, col] = 'v';
                return 1 + TraverseArea(row - 1, col) + TraverseArea(row + 1, col) + TraverseArea(row, col - 1) + TraverseArea(row, col + 1);
            }
        }

        private static Cell FindCell()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != '*' && matrix[i, j] != 'v')
                    {
                        return new Cell { Row = i, Col = j };
                    }
                }
            }

            return null;
        }

        private static void ReadMatrix()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            matrix = new char[rows, cols];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string line = Console.ReadLine();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = line[j];
                }
            }
        }
    }
}
