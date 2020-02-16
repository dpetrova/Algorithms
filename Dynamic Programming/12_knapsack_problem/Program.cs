using System;
using System.Collections.Generic;
using System.Linq;

//Imagine you have a bag (knapsack) and you want to fill it with as many of your most valuable items as you can. 
//The knapsack, of course, cannot hold an infinite number of items, it has a weight limit (capacity). 
//Based on this capacity, you need to decide which items to put in it to maximize the value of the items in the knapsack.

namespace _12_knapsack_problem
{
    class Program
    {
        static Item[] items; // given items
        static int maxCapacity; // knapsack capacity
        static int[,] prices; // matrix holds max parices
        static bool[,] itemsIncluded; // boolean matrix holds which item is taken

        static void Main()
        {
            maxCapacity = 20;

            items = new Item[]
            {
                new Item{Name = "Item1", Weight=5, Price = 30},
                new Item{Name = "Item2", Weight=8, Price = 120},
                new Item{Name = "Item3", Weight=7, Price = 10},
                new Item{Name = "Item4", Weight=0, Price = 20},
                new Item{Name = "Item5", Weight=4, Price = 50},
                new Item{Name = "Item6", Weight=5, Price = 80},
                new Item{Name = "Item7", Weight=2, Price = 10}
            };
                        
            prices = new int[items.Length + 1, maxCapacity + 1];            
            itemsIncluded = new bool[items.Length + 1, maxCapacity + 1];

            //found max total price
            var maxTotalPrice = FillKnapsack();
            Console.WriteLine($"Total value: {maxTotalPrice}");

            //taken items
            var takenItems = TakenItems(maxCapacity);
            Console.WriteLine($"Total weight: {takenItems.Sum(x => x.Weight)}");
            Console.WriteLine("Taken items: ");
            foreach (var item in takenItems)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static int FillKnapsack()
        {
            for (int itemIndex = 0; itemIndex < items.Length; itemIndex++)
            {                
                var currItem = items[itemIndex];
                var matrixRow = itemIndex + 1; //because there is one more row with zeroes (for no items)

                for (int capacity = 0; capacity <= maxCapacity; capacity++)
                {                    
                    // if item weights more than current capacity drop it
                    if(currItem.Weight > capacity)
                    {
                        continue;
                    }
                                       
                    var excluding = prices[matrixRow - 1, capacity];                    
                    var including = currItem.Price + prices[matrixRow - 1, capacity - currItem.Weight];

                    // if include item
                    if(including > excluding)
                    {
                        // calc max curr value
                        prices[matrixRow, capacity] = including;
                        // mark item is taken
                        itemsIncluded[matrixRow, capacity] = true;
                    }
                    // if exclude item
                    else
                    {
                        prices[matrixRow, capacity] = excluding;
                        //itemsIncluded[matrixRow, capacity] = false;
                    }
                }
            }

            //print prices matrix
            //for (int i = 0; i < prices.GetLength(0); i++)
            //{
            //    for (int j = 0; j < prices.GetLength(1); j++)
            //    {
            //        Console.Write(prices[i,j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            //found max total value
            var maxTotalValue = prices[items.Length, maxCapacity];
            return maxTotalValue;
        }

        static List<Item> TakenItems(int maxCapacity)
        {
            var takenItems = new List<Item>();            
            var capacity = maxCapacity;

            for (int i = items.Length - 1; i >= 0; i--)
            {
                if (capacity <= 0) break;

                var item = items[i];
                var matrixRow = i + 1;

                if (itemsIncluded[matrixRow, capacity])
                {
                    takenItems.Add(item);
                    capacity -= item.Weight;
                }                
            }

            takenItems.Reverse();
            return takenItems;
        }
    }


    class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
    }
}
