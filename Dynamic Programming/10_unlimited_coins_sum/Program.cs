using System;

//We have a set of coins with predetermined values, e.g. 1, 2, 5, 10, 20, 50. 
//Given a sum S, the task is to find how many combinations of coins will sum up to S. 
//For each value, we can use an unlimited number of coins (we could take as many coins of a certain value as we wanted).

namespace _10_unlimited_coins_sum
{
    class Program
    {
        static void Main()
        {
            int[] coins = new int[] { 1, 2, 5 };
            int sum = 5;

            var possibleCombinations = CountWays(coins, sum);
            Console.WriteLine(possibleCombinations);
        }
        
        static int CountWays(int[] coins, int sum)
        {          
            // table[i] will be storing the number of solutions for value i. 
            //We need n+1 rows as the table is constructed in bottom up manner using the base case (n = 0) 
            int[] table = new int[sum + 1]; // initialize all table values as 0 
            
            // Base case (If given sum is 0) 
            table[0] = 1;

            // Pick all coins one by one and update the table[] values 
            // after the index greater than or equal to the value of the picked coin 
            for (int i = 0; i < coins.Length; i++)
            {
                var currCoin = coins[i];

                for (int j = currCoin; j <= sum; j++)
                {
                    //update curr table cell value with value of table cell that currCoin is removed 
                    table[j] += table[j - currCoin];
                }                   
            }                

            return table[sum];
        }
    }
}
