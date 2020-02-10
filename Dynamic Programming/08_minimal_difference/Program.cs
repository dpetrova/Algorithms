using System;
using System.Collections.Generic;
using System.Linq;

//Alan and Bob are twins. For their birthday they received some presents 
//and now they need to split them amongst themselves.
//The goal is to minimize the difference between the values of the presents received by the two brothers,
//i.e. to divide the presents as equally as possible. 
//Assume the presents have values represented by positive integer numbers 
//and that presents cannot be split in half(a present can only go to one brother or the other). 
//Find the minimal difference that can be obtained

namespace _08_minimal_difference
{
    class Program
    {
        static List<int> items;
        static List<int> firstPart;
        static List<int> secondPart;

        static void Main()
        {
            items = new List<int> { 3, 2, 3, 2, 2, 77, 89, 23, 90, 11 };
            
            firstPart = new List<int>();
            secondPart = new List<int>();

            var totalSum = items.Sum();            
            var halfSum = totalSum / 2;
            var firstPartSum = firstPart.Sum();
            var secondPartSum = secondPart.Sum();

            items = items.OrderBy(x => Math.Abs( x - halfSum)).ToList();
                        
            for (int i = 0; i < items.Count; i++)
            {
                //first part sum is smaller
                if (firstPartSum < secondPartSum)
                {
                    firstPartSum += items[i];
                    firstPart.Add(items[i]);
                }
                else
                {
                    secondPartSum += items[i];
                    secondPart.Add(items[i]);
                }
            }

            Console.WriteLine($"Alan: {firstPartSum}");
            Console.WriteLine($"Bob: {secondPartSum}");
            Console.WriteLine($"Alan takes: {string.Join(" ", firstPart)}");
            Console.WriteLine($"Bob takes: {string.Join(" ", secondPart)}");
        }
    }
}
