//We have N items, each with a certain weight and price. 
//The knapsack has a maximum capacity, so we need to choose what to take in order to maximize the value (price) of the items in it. 

using System;
using System.Collections.Generic;
using System.Linq;

namespace _06_knapsack
{
    class Program
    {
        static void Main()
        {
            var knapsack = new Knapsack(16);
            var items = new Item[] { 
                new Item { Price = 25, Weight = 10 }, 
                new Item { Price = 12, Weight = 8 }, 
                new Item { Price = 16, Weight = 6 },
                new Item { Price = 30, Weight = 40 }
            };

            CollectItems(items, knapsack);
            Console.WriteLine(knapsack.ToString());
        }

        static void CollectItems(IList<Item> items, Knapsack knapsack)
        {
            items = items.OrderByDescending(x => x.ValuableRatio()).ToList();            
            
            while (knapsack.GetTotalWeight() < knapsack.Capacity || items.Count > 0)
            {                 
                var currItem = items.First();
                if (knapsack.GetTotalWeight() + currItem.Weight > knapsack.Capacity)
                {
                    return;
                }

                knapsack.AddItem(currItem);
                items.Remove(currItem);
            }
        }
    }

    class Knapsack
    {
        public Knapsack(double capacity)
        {
            this.Capacity = capacity;
            this.Items = new List<Item>();
        }

        public double Capacity { get; set; }
        public List<Item> Items { get; set; }

        public void AddItem(Item item)
        {
            this.Items.Add(item);
        }

        public double GetTotalWeight()
        {
            return this.Items.Sum(x => x.Weight);
        }

        public double GetTotalPrice()
        {
            return this.Items.Sum(x => x.Price);
        }

        public override string ToString()
        {
            string result = "";
            foreach (var item in this.Items)
            {
                result += string.Format("Take item with price {0} and weight {1} \n", item.Price, item.Weight);
            }
            result += string.Format("Total price: {0}", this.GetTotalPrice());
            return result;
        }
    }

    class Item
    {
        public double Price { get; set; }
        public double Weight { get; set; }

        //calculate how valuable is item (price/weight ratio)
        public double ValuableRatio()
        {
            return this.Price / this.Weight;
        }
    }
}
