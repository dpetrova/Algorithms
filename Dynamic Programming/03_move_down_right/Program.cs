using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//You are given a matrix of numbers
//Find the path with largest sum
//Start from top/left and End to bottom/right
//Move only right/down

namespace _03_move_down_right
{
    class Program
    {
        static int[,] matrix;
        static int[,] sums;
        static int rows;
        static int cols;

        static void Main()
        {                      
            matrix = new int[,] { { 1, 3, 2, 1 }, { 5, 3, 2, 1 }, { 1, 7, 3, 1 }, { 1, 3, 1, 1 } };
            rows = matrix.GetLength(0);
            cols = matrix.GetLength(1);
            sums = new int[rows, cols];

            CalculateSums();
            Console.WriteLine("Largest sum is: " + sums[rows-1, cols-1]);
            Console.WriteLine("Path in matrix: ");
            PrintPath();
        }

        static void CalculateSums()
        {
            //1. first sums element is equal to first matrix cell
            sums[0, 0] = matrix[0, 0];            

            //2. calculate sums in first row
            for (int i = 1; i < cols; i++)
            {
                //curr sum = prev sum + curr cell of matrix
                sums[0, i] = sums[0, i - 1] + matrix[0, i];
            }

            //3. calculate sums in first column
            for (int i = 1; i < rows; i++)
            {
                //curr sum = prev sum + curr cell of matrix
                sums[i, 0] = sums[i - 1, 0] + matrix[i, 0];
            }

            //4. calculate sums for other cells
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    int topSum = sums[i - 1, j];
                    int leftSum = sums[i, j - 1];
                    int largest = Math.Max(topSum, leftSum);
                    sums[i, j] = largest + matrix[i, j];
                }
            }
        }

        static void PrintPath()
        {
            var path = new List<string>();

            //construct path from end -> to start

            //1.add end cell
            var currRow = rows - 1;
            var currCol = cols - 1;
            path.Add($"[{currRow}, {currCol}]");

            //2.while reach start cell
            while (currRow != 0 || currCol != 0)
            {
                //check if sum is largest from top cell / or from left cell and add largest cell to path
                var top = -1;
                if(currRow - 1 >= 0)
                {
                    top = sums[currRow - 1, currCol];
                }

                var left = -1;
                if(currCol - 1 >= 0)
                {
                    left = sums[currRow, currCol - 1];
                }
                

                if(top > left)
                {
                    //move up
                    path.Add($"[{currRow - 1}, {currCol}]");
                    currRow -= 1;                    
                }
                else
                {
                    //move left
                    path.Add($"[{currRow}, {currCol - 1}]");
                    currCol -= 1;                    
                }
            }

            path.Reverse();
            Console.WriteLine(string.Join(" ", path));
        }
    }
}
