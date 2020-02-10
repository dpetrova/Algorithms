using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Alan and Bob are twins. For their birthday they received some presents 
//and now they need to split them amongst themselves.
//The goal is to minimize the difference between the values of the presents received by the two brothers,
//i.e. to divide the presents as equally as possible. 
//Assume the presents have values represented by positive integer numbers 
//and that presents cannot be split in half(a present can only go to one brother or the other). 
//Find the minimal difference that can be obtained

namespace _09_minimal_difference_DP
{
    class Program
    {
        static int[] items;
        static int[] sums;

        static void Main()
        {
            items = new int[] { 3, 2, 3, 2, 2, 77, 89, 23, 90, 11 };
            var totalSum = items.Sum();
            sums = new int[totalSum + 1];

            //fill all sums with -1, exclide the first which is 0
            for (int i = 1; i < sums.Length; i++)
            {
                sums[i] = -1;
            }

            //calculate all possible sums (combinations of items)
            //loop through all items and at each step calculate sum without this particular item
            for (int i = 0; i < items.Length; i++)
            {
                var currItem = items[i];
                for (int j = totalSum - currItem; j >= 0; j--)
                {
                    if(sums[j] != -1 && sums[j + currItem] == -1) 
                    {
                        sums[j + currItem] = i;
                    }
                }
            }

            var halfSum = totalSum / 2;
            
            //find first possible sum that is closest to half sum
            for (int i = halfSum; i >= 0; i--)
            {                
                if (sums[i] == -1) continue;

                Console.WriteLine($"Alan: {i}");
                Console.WriteLine($"Bob: {totalSum - i}");
                Console.Write($"Alan takes:");

                while (i != 0)
                {
                    Console.Write($" {items[sums[i]]}");
                    i -= items[sums[i]];
                }
                Console.WriteLine();
            }
        }
    }
}
