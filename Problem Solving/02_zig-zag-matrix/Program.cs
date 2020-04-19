using System;
using System.Collections.Generic;
using System.Linq;

//You are given a matrix of positive integer numbers. 
//A sig-zag path in the matrix starts from some cell in the first column, goes to some cell up in the second column, 
//then to some cell down in the third column, etc. until the last column is reached. 
//Your task is to write a program that finds the zigzag path with the maximal sum.
//On the first line of input you’ll receive the number of rows N. 
//On the second line you’ll receive the number of columns M.  
//On each of the next N rows you’ll receive M positive integer numbers separated by a comma.
//algorithm: use dynamic programming – for each cell find the maximum path and then recover the path when we find the global maximum
//Break down the problem:
//    1) Read the input and fill the matrix we’ll be working with
//    2) Using DP find the maximal path leading to each cell
//    3) Recover the path after the DP algorithm is finished
//    4) Print the output
//Choose appropriate data structures:
//    • jagged array (array of arrays) of integersto hold input matrix
//    • matrix to hold the max path for each cell (we’ll fill this using a DP approach)
//    • matrix to keep track of the path (for each cell we’ll keep the row index of the cell which led to it)
//    • list for the path when we recover it

namespace _02_zig_zag_matrix
{
    class Program
    {
        static int[][] matrix; //input matrix
        static int[,] maxPaths; //matrix to keep the max path for each cell
        static int[,] previousRowIndex; //matrix to keep track of the path

        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            matrix = new int[rows][];
            maxPaths = new int[rows, cols];
            previousRowIndex = new int[rows, cols];            

            // 1) Read the input and fill the matrix
            ReadMatrix(rows, matrix);

            // 2) DP to find the maximal path leading to each cell
            FindMaxPathToEachCell(maxPaths);

            // 3) Recover the path
            //check what is the row index of the last cell in the path
            int currentRowIndex = GetLastRowIndexOfPath(maxPaths);
            var path = RecoverMaxPath(matrix, currentRowIndex, previousRowIndex);
            Console.WriteLine($"{path.Sum()} = {String.Join(" + ", path)}");
        }

        static void ReadMatrix(int rows, int[][] matrix)
        {
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
            }
        }

        static void FindMaxPathToEachCell(int[,] maxPaths)
        {
            //initialize first column of maxPaths
            for (int row = 1; row < maxPaths.GetLength(0); row++)
            {
                maxPaths[row, 0] = matrix[row][0];
            }

            //fill max paths
            //find the best path for each cell as adding the cell’s value to the best path from the previous column
            //because of the zigzag requirement, we need to check if the column is even or odd
            //if the column is odd, this means the path needs to come from a cell below the current one
            //if the column is even we’ll check only rows which are above the current cell’s row
            for (int col = 1; col < maxPaths.GetLength(1); col++)
            {
                for (int row = 0; row < maxPaths.GetLength(0); row++)
                {
                    int previousMax = 0;

                    //on odd columns se check cells below and one column to the left
                    if(col % 2 != 0)
                    {
                        for (int i = row + 1; i < maxPaths.GetLength(0); i++)
                        {
                            if(maxPaths[i, col - 1] > previousMax)
                            {
                                //update previousMax
                                previousMax = maxPaths[i, col - 1];
                                //mark the best path to cell in the previusRowIndex matrix
                                previousRowIndex[row, col] = i;
                            }
                        }
                    }
                    //on even columns we check cells above and one column to the left
                    else
                    {
                        for (int i = 0; i < row; i++)
                        {
                            if (maxPaths[i, col - 1] > previousMax)
                            {
                                //update previousMax
                                previousMax = maxPaths[i, col - 1];
                                //mark the best path to cell in the previusRowIndex matrix
                                previousRowIndex[row, col] = i;
                            }
                        }
                    }

                    //add the cell’s value to the best path from the previous column
                    maxPaths[row, col] = previousMax + matrix[row][col];
                }
            }
        }

        static int GetLastRowIndexOfPath(int[,] maxPaths)
        {
            int currRowIndex = -1;
            int globalMax = 0;

            //traverse the last column and get the row index of the max value contained in the maxPaths matrix
            for (int row = 0; row < maxPaths.GetLength(0); row++)
            {
                if(maxPaths[row, maxPaths.GetLength(1) - 1] > globalMax)
                {
                    globalMax = maxPaths[row, maxPaths.GetLength(1) - 1];
                    currRowIndex = row;
                }
            }

            return currRowIndex;
        }
        
        static List<int> RecoverMaxPath(int[][] matrix, int rowIndex, int[,] previousRowIndex)
        {
            List<int> path = new List<int>();
            int columnIndex = previousRowIndex.GetLength(1) - 1;

            //start at the currentRowIndex and the last column and follow the path kept in previousRowIndex for that cell
            while (columnIndex >= 0)
            {
                //add cell to path (found at rowIndex)
                path.Add(matrix[rowIndex][columnIndex]);

                //update rowIndex using the info in previousRowIndex
                rowIndex = previousRowIndex[rowIndex, columnIndex];

                columnIndex--;
            }

            path.Reverse();
            return path;
        }
    }
}
