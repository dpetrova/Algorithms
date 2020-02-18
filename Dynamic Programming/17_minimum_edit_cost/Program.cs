using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//We have two strings, s1 and s2. The goal is to obtain s2 from s1 by applying the following operations:
// • replace(i, x) – in s1, replaces the symbol at index i with the character x
// • insert(i, x) – in s1, inserts the character x at index i
// • delete(i) – from s1, removes the character at index i
//Each of the three operations has a certain cost associated with it(positive integer number). 
//Note: the cost of the replace(i, x) operation is 0 if it doesn’t actually change the character.
//The goal is to find the sequence of operations which will produce s2 from s1 with minimal cost.

namespace _17_minimum_edit_cost
{
    class Program
    {
        static void Main()
        {
            int costReplace = 3;
            int costInsert = 2;
            int costDelete = 1;
            string s1 = "abracadabra";
            string s2 = "mabragabra";

            //matrix to hold all possible costs
            //matrix has one additional row and one additional column represents empty strings
            int[,] costs = new int[s1.Length + 1, s2.Length + 1];

            /* FILL POSSIBLE COSTS */
            //fill first row only by insert operations (+ costInsert for every next cell)
            //represent from empty string to current string
            for (int i = 1; i < costs.GetLength(1); i++)
            {
                costs[0, i] = costs[0, i - 1] + costInsert;
            }

            //fill first col only by delete operations (+ costDelete for every next cell)
            //represent from current string to empty string
            for (int i = 1; i < costs.GetLength(0); i++)
            {
                costs[i, 0] = costs[i - 1, 0] + costDelete;
            }

            //fill matrix
            for (int i = 1; i < costs.GetLength(0); i++)
            {
                for (int j = 1; j < costs.GetLength(1); j++)
                {                    
                    var up = costs[i - 1, j];
                    var left = costs[i, j - 1];
                    var diagonalUpLeft = costs[i - 1, j - 1];

                    //if characters of both strings in curr indexes are equal
                    if(s1[i - 1] == s2[j - 1])
                    {
                        costs[i, j] = diagonalUpLeft;
                    }
                    else
                    {
                        var delete = up + costDelete;
                        var insert = left + costInsert;
                        var replace = diagonalUpLeft + costReplace;
                        costs[i, j] = Math.Min(insert, Math.Min(delete, replace)); //get min of three operations
                    }
                }
            }

            //print costs matrix
            for (int i = 0; i < costs.GetLength(0); i++)
            {
                for (int j = 0; j < costs.GetLength(1); j++)
                {
                    Console.Write(costs[i,j] + " ");
                }
                Console.WriteLine();
            }

            //solution is the last (most right/down) cell
            Console.WriteLine($"Minimum edit distance: {costs[costs.GetLength(0) - 1, costs.GetLength(1) - 1]}");


            /* RECONSTRUCT OPERATIONS */            
            var operations = new Stack<string>();
            var currRow = costs.GetLength(0) - 1;
            var currCol = costs.GetLength(1) - 1;

            while (currRow > 0 && currCol > 0)
            {
                var curr = costs[currRow, currCol];
                var up = costs[currRow - 1, currCol];
                var left = costs[currRow, currCol - 1];
                var diagonalUpLeft = costs[currRow - 1, currCol - 1];

                //if characters of both strings in curr indexes are equal
                if (s1[currRow - 1] == s2[currCol - 1])
                {
                    //no operation made; move to up/left diagonal
                    currRow--;
                    currCol--;
                }
                else
                {
                    var delete = up + costDelete;
                    var insert = left + costInsert;
                    var replace = diagonalUpLeft + costReplace;

                    //move depends on which is minimal
                    if(delete < replace && delete < insert)
                    {
                        operations.Push($"DELETE {currRow - 1}");
                        currRow--;
                    }
                    else if(insert < delete && insert < replace)
                    {
                        operations.Push($"INSERT {currCol - 1}, {s2[currCol - 1]}");
                        currCol--;
                    }
                    else
                    {
                        operations.Push($"REPLACE {currRow - 1}, {s2[currCol -1]}");
                        currRow--;
                        currCol--;
                    }
                }    
            }

            while (currRow > 0)
            {
                operations.Push($"DELETE({currRow - 1})");
                currRow--;
            }

            while (currCol > 0)
            {
                operations.Push($"INSERT({currCol - 1}, {s2[currCol - 1]})");
                currCol--;
            }

            foreach (var operation in operations)
            {
                Console.WriteLine(operation);
            }
        }
    }
}
