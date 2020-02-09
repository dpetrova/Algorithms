using System;
using System.Collections.Generic;

//Find the best way to cut up a rod with a specified length. 
//You are also given to prices of all possible lengths starting from 0.

namespace _04_rod_cutting_recursive
{
    class Program
    {
        static int[] rodPrices;
        static int[] bestPrices;
        static int[] bestCombo;

        static void Main()
        {
            rodPrices = new int[] { 0, 1, 5, 8, 9, 10, 17, 17, 20, 24, 30};
            bestPrices = new int[rodPrices.Length]; //initialize array of best prices with all 0
            bestCombo = new int[rodPrices.Length]; //initialize array with all 0           
            int n = 8;  //how many parts rod can be cut

            //best price
            var bestSolution = CutRod(n);
            Console.WriteLine(bestSolution);

            //print parts that rod is cut to recieve best price
            ReconstructSolution(n);
        }

        private static int CutRod(int length)
        {
            //if already calculate best price for this length
            if (bestPrices[length] > 0) return bestPrices[length];

            //rod with no length
            if (length == 0) return 0;

            var bestPrice = 0;

            //loop through all possible lengths that rod's current length can be cut further
            for (int i = 1; i <= length; i++)
            {
                //recursively calculate best price for current length
                var currBestPrice = rodPrices[i] + CutRod(length - i);
                bestPrice = Math.Max(bestPrice, currBestPrice);

                //memorize calculated best price
                if(bestPrice > bestPrices[length])
                {
                    bestPrices[length] = bestPrice;
                    bestCombo[length] = i;
                }
            }

            return bestPrice;
        }

        private static void ReconstructSolution(int length)
        {
            List<int> result = new List<int>();

            while (length > 0 )
            {
                int next = bestCombo[length];
                result.Add(next);
                length -= next;
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
