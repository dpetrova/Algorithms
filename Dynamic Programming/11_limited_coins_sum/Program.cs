using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//We have a set of certain coins, e.g. 1, 5, 10, 10, 10, 50. 
//Given a sum S, the task is to find how many combinations of coins will sum up to S. 
//We can use each of coins only once.

namespace _11_limited_coins_sum
{
    class Program
    {
        static void Main()
        {
            int[] coins = new int[] { 1, 2, 2, 5 };
            int sum = 5;

            var possibleCombinations = CountWays(coins, sum);
            Console.WriteLine(possibleCombinations);
        }

        static int CountWays(int[] coins, int sum)
        {
            //init matrix with all cells 0
            int[,] maxCount = new int[coins.Length + 1, sum + 1];

            //set first column of matrix to be 1 => meaning when sum is 0 the possible ways to sum up is 1 (no coins taken)
            for (int i = 0; i <= coins.Length; i++)
            {
                maxCount[i, 0] = 1;
            }
            
            for (int i = 1; i <= coins.Length; i++)
            {
                for (int j = sum; j >= 0; j--)
                {
                    if (coins[i - 1] <= j && maxCount[i - 1, j - coins[i - 1]] != 0)
                    {
                        maxCount[i, j]++;
                    }
                    else
                    {
                        maxCount[i, j] = maxCount[i - 1, j];
                    }
                }
            }

            int count = 0;
            for (int i = 0; i <= coins.Length; i++)
            {
                if (maxCount[i, sum] != 0)
                {
                    count++;
                }
            }

            return count;

        }
    }
}
