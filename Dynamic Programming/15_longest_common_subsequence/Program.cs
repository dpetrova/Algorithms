using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Considering two sequences S1 and S2, 
//the longest common subsequence (LCS) is a sequence which is a subsequence of both S1 and S2. 
//For instance, if we have two strings (sequences of characters), "abc" and "adb", 
//the LCS is "ab" – it is a subsequence of both sequences and it is the longest 
//(there are two other subsequences – "a" and "b")

namespace _15_longest_common_subsequence
{
    class Program
    {
        static void Main()
        {
            var sequence1 = "BDCABA";
            var sequence2 = "ABCBDAB";

            var lcsMatrix = FindLCS(sequence1, sequence2);
            var lcs = ReconstructLCS(lcsMatrix, sequence1, sequence2);
            Console.WriteLine(lcs);
        }

        static int[,] FindLCS(string seq1, string seq2)
        {
            //matrix that will hold lengths of all possible common subsequences
            //its size is + 1, because there is one additional row and one additional column for cases that subsequence.lenght == 0
            var lcs = new int[seq1.Length + 1, seq2.Length + 1];

            //fill matrix as follow:
            //lcs[-1, col] = 0
            //lcs[row, -1] = 0
            //lcs[row, col] = max( lcs[row - 1, col], lcs[row, col - 1]) when S1[row] != S2[col]
            //lcs[row, col] = lcs[row - 1, col - 1] + 1 when S1[row] == S2[col]
            for (int row = 1; row < lcs.GetLength(0); row++)
            {
                for (int col = 1; col < lcs.GetLength(1); col++)
                {
                    var up = lcs[row - 1, col];
                    var left = lcs[row, col - 1];
                    var diagonalUpLeft = lcs[row - 1, col - 1];

                    //if characters in curr indexes in both sequences are equal
                    if(seq1[row -1] == seq2[col -1])
                    {
                        //curr cell = value of upper left diagonal + 1
                        lcs[row, col] = diagonalUpLeft + 1;
                    }
                    //if characters in curr indexes in both sequences are different
                    else
                    {
                        //curr cell = max( value in up cell, value in left cell)
                        lcs[row, col] = Math.Max(up, left);
                    }
                }
            }

            //print lcs matrix
            for (int i = 0; i < lcs.GetLength(0); i++)
            {
                for (int j = 0; j < lcs.GetLength(1); j++)
                {
                    Console.Write( lcs[i, j] + " ");
                }
                Console.WriteLine();
            }

            return lcs;
        }

        static string ReconstructLCS(int[,] lcsMatrix, string seq1, string seq2)
        {
            var sequence = "";
            var currRow = lcsMatrix.GetLength(0) - 1;
            var currCol = lcsMatrix.GetLength(1) - 1;

            while (currRow > 0 && currCol > 0)
            {
                // the last characters of the two substrings are the same
                if(seq1[currRow -1] == seq2[currCol -1])
                {
                    // add the character to the list and move to the cell which is to the left and above the current one (up left diagonal)
                    sequence += seq1[currRow - 1];
                    currRow--;
                    currCol--;
                }
                // the last characters of the two substrings are different
                else
                {
                    // we need to decide where to go next – up or left
                    // we go to the cell which has largest value (if both have the same length, it doesn’t really matter)
                    var up = lcsMatrix[currRow - 1, currCol];
                    var left = lcsMatrix[currRow, currCol - 1];

                    if (up >= left)
                    {
                        currRow--;
                    }
                    else
                    {
                        currCol--;
                    }
                }
            }

           var reverse = sequence.ToArray().Reverse();
           return new string(reverse.ToArray());
        }
    }
}
