using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//A zigzag sequence is one that alternately increases and decreases. 
//More formally, such a sequence has to comply with one of the two rules below:
//    1) Every even element is smaller than its neighbors and every odd element is larger than its neighbors, or
//    2) Every odd element is smaller than its neighbors and every even element is larger than its neighbors
//1 3 2 is a zigzag sequence, but 1 2 3 is not.Any sequence of one or two elements is zig zag.
//Find the longest zigzag subsequence in a given sequence.

namespace _07_longest_zigzag_subsequence
{
    class Program
    {
        static int[] sequence; //given sequence of numbers
        static int[] lastIsBigger; //array to hold lengths of subsequences in which last element is bigger than previous
        static int[] lastIsSmaller; //array to hold lengths of subsequences in which last element is smaller than previous
        static int[,] prev; //two arrays to hold indexes of prev calculated longest subsequences (for lastIsBigger and lastIsSmaller)
        
        static void Main()
        {
            sequence = new int[] { 8, 3, 5, 7, 0, 8, 9, 10, 20, 20, 20, 12, 19, 11 };
            lastIsBigger = new int[sequence.Length];
            lastIsSmaller = new int[sequence.Length];
            prev = new int[sequence.Length, 2];
            
            CalculateZigzagSubsequence();            
        }

        static void CalculateZigzagSubsequence()
        {
            //any sequence of one or two elements is zig zag
            lastIsBigger[0] = 1;
            lastIsSmaller[0] = 1;

            //prev curr best solution indexes
            prev[0, 0] = prev[0, 1] = -1;
            
            int maxLength = 0;

            //in which cell of prev matrix (two arrays) is best solution
            int maxIndexRow = 0;
            int maxIndexCol = 0;

            for (int currIndex = 1; currIndex < sequence.Length; currIndex++)
            {                
                for (int prevIndex = 0; prevIndex < currIndex; prevIndex++)
                {
                    int currNumber = sequence[currIndex];
                    int prevNumber = sequence[prevIndex];

                    //to ensure zigzag, check if:

                    //first case:
                    //1. curr number > prev number
                    //2. curr best length in increasing arr < prev best length in decreasing arr
                    if (currNumber > prevNumber && lastIsBigger[currIndex] < lastIsSmaller[prevIndex] + 1)
                    {
                        lastIsBigger[currIndex] = lastIsSmaller[prevIndex] + 1;
                        prev[currIndex, 0] = prevIndex;
                    }

                    //second case:
                    //1. curr number < prev number
                    //2. curr best length in decreasing arr < prev best length in increasing arr
                    if (currNumber < prevNumber && lastIsSmaller[currIndex] < lastIsBigger[prevIndex] + 1)
                    {
                        lastIsSmaller[currIndex] = lastIsBigger[prevIndex] + 1;
                        prev[currIndex, 1] = prevIndex;
                    }
                }

                //preserve found maxLength
                if (lastIsBigger[currIndex] > maxLength)
                {
                    maxLength = lastIsBigger[currIndex];
                    maxIndexRow = currIndex;
                    maxIndexCol = 0;
                }

                if(lastIsSmaller[currIndex] > maxLength)
                {
                    maxLength = lastIsSmaller[currIndex];
                    maxIndexRow = currIndex;
                    maxIndexCol = 1;
                }
            }

            //print length of longest zigzag
            Console.WriteLine("Longest zigzag subsequence is with length: ", maxLength);

            //print zigzag subsequence
            var zigzag = new List<int>();

            while (maxIndexRow >= 0)
            {
                zigzag.Add(sequence[maxIndexRow]);
                maxIndexRow = prev[maxIndexRow, maxIndexCol];

                //at each step change array(index in lastIsBigger, then in lastIsSmaller, than again in lastIsBigger, and so on...(like zigzag)
                if(maxIndexCol == 1)
                {
                    maxIndexCol = 0;
                }
                else
                {
                    maxIndexCol = 1;
                }
            }

            zigzag.Reverse();
            Console.WriteLine(string.Join(" ", zigzag));
        }

        
    }
}
