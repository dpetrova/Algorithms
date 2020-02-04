using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Find longest increasing subsequence of given sequence of numbers

namespace _02_longest_increasing_subsequence
{
    class Program
    {
        static void Main()
        {
            int[] sequence = { 3, 14, 5, 12, 15, 7, 8, 9, 11, 10, 1 };
            int[] solution = new int[sequence.Length];
            int[] prev = new int[sequence.Length];
            int maxSolutionLength = 1;
            int maxSolutionIndex = 0;

            for (int i = 0; i < sequence.Length; i++)
            {
                //the worst case when current number cannot combine with any previous number
                int currentSolutionLength = 1;
                int prevIndex = -1;
                int currentNumber = sequence[i];

                //loop through all previous solutions to 
                for (int j = 0; j < i; j++)
                {
                    int previousNumber = sequence[j];
                    int previousSolutionLength = solution[j];

                    //check if number is increasing and solutions length
                    if(currentNumber > previousNumber && currentSolutionLength < previousSolutionLength + 1)
                    {
                        currentSolutionLength = previousSolutionLength + 1;
                        prevIndex = j;
                    }
                }

                solution[i] = currentSolutionLength;
                prev[i] = prevIndex;

                if(currentSolutionLength > maxSolutionLength)
                {
                    maxSolutionLength = currentSolutionLength;
                    maxSolutionIndex = i;
                }

            }

            //print length of LIS
            Console.WriteLine($"Longest subsequence length: {maxSolutionLength}");

            //print LIS
            int index = maxSolutionIndex;
            var LIS = new List<int>();
            while (index != -1)
            {
                var currNumber = sequence[index];
                LIS.Add(currNumber);
                index = prev[index];
            }
            LIS.Reverse();
            Console.WriteLine(string.Join(" ", LIS));
            
        }
    }
}
