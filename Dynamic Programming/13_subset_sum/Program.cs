using System;
using System.Collections.Generic;
using System.Linq;

//Given a set of integers numbers = {n1, n2,...ni} and an target sum S,
//find a subset of numbers whose sum is S

namespace _13_subset_sum
{
    class Program
    {
        static void Main()
        {
            var numbers = new int[] { 3, 5, 1, 4, 2 };
            var targetSum = 9;

            //calculate sums of all possible subsets
            var possibleSums = CalcPossibleSums(numbers);

            //recover the subset which sum is equal to target sum
            ReconstructSubset(possibleSums, targetSum);
        }

        static Dictionary<int, int> CalcPossibleSums(int[] numbers)
        {
            //possible sum -> subset ending at curr number
            var possibleSums = new Dictionary<int, int>();

            //Sum = 0 -> no subset
            possibleSums.Add(0, 0);

            for (int i = 0; i < numbers.Length; i++)
            {
                var currNumber = numbers[i];
                
                foreach (var sum in possibleSums.Keys.ToList())
                {
                    //to each of previous sums add current number
                    var newSum = sum + currNumber;

                    if (!possibleSums.ContainsKey(newSum))
                    {
                        possibleSums.Add(newSum, currNumber);
                    }
                }
            }

            return possibleSums;
        }

        static void ReconstructSubset(Dictionary<int, int> subsetSums, int targetSum)
        {
            if(subsetSums.ContainsKey(targetSum))
            {
                Console.WriteLine("Subset with target sum exists");
                Console.Write("Subset: ");

                while (targetSum != 0)
                {
                    var foundSum = subsetSums[targetSum];
                    Console.Write(foundSum);
                    targetSum -= foundSum;
                }
            }
            else
            {
                Console.WriteLine("No subset with target sum exists");
            }
        }
    }
}
