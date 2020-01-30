using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_knapsack_fractional
{
    class Program
    {
        static void Main()
        {
            var knapsack = new Knapsack(16);
            var items = new Item[] {
                new Item { Price = 25, Weight = 10 },
                new Item { Price = 12, Weight = 8 },
                new Item { Price = 16, Weight = 8 }
            };

            CollectItems(items, knapsack);
            Console.WriteLine(knapsack.ToString());
        }

        static void CollectItems(IList<Item> items, Knapsack knapsack)
        {
            items = items.OrderByDescending(x => x.ValuableRatio()).ToList();
            var index = 0;

            while (knapsack.Capacity > 0 && index < items.Count)
            {
                var currItem = items[index];

                //how many I can take depending on remaining knapsack capacity
                var weigthToTaken = Math.Min(knapsack.Capacity, currItem.Weight);

                //calculate needed percentage of item
                var percentQuantity = weigthToTaken / currItem.Weight;

                //add item in knapsack
                knapsack.AddItem(currItem, percentQuantity);

                //recalculate remaining free capacity
                knapsack.Capacity -= weigthToTaken;

                index++;
            }
        }
    }

    class Knapsack
    {
        public Knapsack(double capacity)
        {
            this.Capacity = capacity;
            this.Items = new Dictionary<Item, double>();
        }

        public double Capacity { get; set; }
        public Dictionary<Item, double> Items { get; set; }

        public void AddItem(Item item, double quantity)
        {
            this.Items.Add(item, quantity);
        }
        
        public double GetTotalPrice()
        {
            //key->item, value->quantity
            return this.Items.Sum(x => x.Key.Price * x.Value);
        }

        public override string ToString()
        {
            string result = "";
            foreach (var entry in this.Items)
            {
                var item = entry.Key;
                var quantity = entry.Value * 100;
                result += string.Format("Take {0}% of item with price {1} and weight {2} \n", quantity, item.Price, item.Weight);
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
