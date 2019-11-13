//a recursive program for generating and printing all combinations with duplicates of k elements from a set of n elements
//the order of elements doesn’t matter, therefore (1 2) and (2 1) are the same combination

using System;
using System.Linq;

namespace _11_combinations_with_repetitions
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            int[] subset = new int[k];
            GenerateCombinations(0, 1, n, subset);
        }

        static void GenerateCombinations(int index, int border, int n, int[]subset)
        {
            if (index == subset.Length)
            {

                Console.WriteLine(string.Join(" ", subset));
                return;
            }
            else
            {
                for (int i = border; i <= n; i++)
                {
                    subset[index] = i;
                    GenerateCombinations(index + 1, i, n, subset);
                }
            }
        }
    }
}
