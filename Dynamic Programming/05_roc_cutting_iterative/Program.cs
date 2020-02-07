using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Find the best way to cut up a rod with a specified length. 
//You are also given to prices of all possible lengths starting from 0.

namespace _05_roc_cutting_iterative
{
    class Program
    {
        static int[] prices;
        static int[] bestPrices;
        static int[] bestCombo;

        static void Main()
        {
            prices = new int[] { 0, 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 8 };
            bestPrices = new int[prices.Length]; //initialize array of best prices with all 0
            bestCombo = new int[prices.Length]; //initialize array with all 0
            int n = 8;  //how many parts rod can be cut

            //best price
            var bestSolution = CutRod(n);
            Console.WriteLine(bestSolution);

            //print parts that rod is cut to recieve best price
            ReconstructSolution(n);
        }

        private static int CutRod(int length)
        {     
            //loop through all possible lengths that rod can be cutted
            for (int i = 1; i <= length; i++)
            {
                var bestPrice = 0;

                //loop through all possible lengths that current length can be cutted
                for (int j = 1; j <= i; j++)
                {
                    var currBestPrice = prices[j] + bestPrices[i - j];
                    bestPrice = Math.Max(bestPrices[i], currBestPrice);

                    //memorize calculated best price
                    if (bestPrice > bestPrices[i])
                    {
                        bestPrices[i] = bestPrice;
                        bestCombo[i] = j;
                    }
                }                
            }

            return bestPrices[length];
        }

        private static void ReconstructSolution(int length)
        {
            List<int> result = new List<int>();

            while (length > 0)
            {
                int next = bestCombo[length];
                result.Add(next);
                length -= next;
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
