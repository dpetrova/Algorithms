using System;

//We have a set of coins with predetermined values, e.g. 1, 2, 5, 10, 20, 50. 
//Given a sum S, the task is to find how many combinations of coins will sum up to S. 
//For each value, we can use an unlimited number of coins.

namespace _10_unlimited_coins_sum
{
    class Program
    {
        static void Main()
        {
            int[] coins = new int[] { 1, 2, 5 };
            int m = coins.Length;
            int sum = 5;

            Console.Write(count(coins, m, sum));
        }

        // Returns the count of ways we can sum S[0...m-1] coins to get sum n 
        static int count(int[] S, int m, int n)
        {
            // If n is 0 then there is 1 solution (do not include any coin) 
            if (n == 0) return 1;

            // If n is less than 0 then no solution exists 
            if (n < 0) return 0;

            // If there are no coins and n is greater than 0, then no solution exist 
            if (m <= 0 && n >= 1) return 0;

            // count is sum of solutions (i) including S[m-1] (ii) excluding S[m-1] 
            return count(S, m - 1, n) + count(S, m, n - S[m - 1]);
        }
    }
}