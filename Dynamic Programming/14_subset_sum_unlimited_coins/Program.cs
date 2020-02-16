using System;
using System.Collections.Generic;

//We have a set of coins with predetermined values, e.g. 1, 2, 5, 10, 20, 50. 
//Given a sum S, the task is to find how many combinations of coins will sum up to S.
//For each value, we can use an unlimited number of coins

namespace _14_subset_sum_unlimited_coins
{
    class Program
    {
        static void Main()
        {
            var numbers = new int[] { 3, 5, 2 };
            var targetSum = 9;

            var possibleSums = CalcPossibleSums(numbers, targetSum);

            var subset = ReconstructSubset(possibleSums, targetSum, numbers);
        }

        static bool[] CalcPossibleSums(int[] numbers, int targetSum)
        {
            //boolean array to hold possible sum
            var possibleSums = new bool[targetSum + 1];

            //first possibleSum is 0 (no subset)
            possibleSums[0] = true;
            
            for (int sum = 0; sum <= targetSum; sum++)
            {
                if (!possibleSums[sum]) continue;

                //to each of previous possible sums add all numbers of set
                for (int i = 0; i < numbers.Length; i++)
                {
                    int newSum = sum + numbers[i];

                    if(newSum <= targetSum)
                    {
                        possibleSums[newSum] = true;
                    }
                }                
            }

            for (int i = 0; i < possibleSums.Length; i++)
            {
                Console.WriteLine($"sum: {i}, is possible: {possibleSums[i]}");
            }
            
            return possibleSums;
        }

        static List<int> ReconstructSubset(bool[] subsetSums, int targetSum, int[] numbers)
        {
            var subset = new List<int>();

            while (targetSum > 0)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    var sum = targetSum - numbers[i];

                    if(sum >= 0 && subsetSums[sum])
                    {
                        subset.Add(numbers[i]);
                        targetSum = sum;
                    }
                }
            }

            Console.WriteLine(string.Join(" ", subset));

            return subset;
        }
    }
}
