namespace SumOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SumOfCoins
    {
        public static void Main(string[] args)
        {
            var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
            var targetSum = 58;

            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            //order coins desc
            coins = coins.OrderByDescending(x => x).ToList();

            Dictionary<int, int> chosenCoins = new Dictionary<int, int>();
            int coinIndex = 0;
            int currSum = 0;

            while (coinIndex < coins.Count && currSum != targetSum)
            {
                int currCoin = coins[coinIndex];

                //check if current coint is suitable and if not -> proceed with next by value coin
                if(currSum + currCoin > targetSum)
                {
                    coinIndex++;
                    continue;
                }

                //find how much remains to the target sum
                int remainingSum = targetSum - currSum;
                //find how many coins of next coin value to take
                int nextCoinsToTake = remainingSum / currCoin;

                //can take coins of this value
                if(nextCoinsToTake > 0)
                {
                    //add to dictionary
                    chosenCoins[currCoin] = nextCoinsToTake;
                    //add to current sum
                    currSum += nextCoinsToTake * currCoin;
                }
            }

            if(currSum != targetSum)
            {
                throw new InvalidOperationException("Greedy algorithm cannot produce desired sum with specified coins.");
            }

            return chosenCoins;
        }
    }
}