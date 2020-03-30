using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

//A cable networking company plans to extend its existing cable network by connecting as many customers as possible
//within a fixed budget limit.
//You are given the existing cable network (a set of customers and connections between them) 
//along with the estimated connection costs between some pairs of customers and prospective customers. 
//A customer can only be connected to the network via a direct connection with an already connected customer.
//Find MAX number of new customers that can be connected to the existing network within the budget limit.


namespace _12_cable_network
{
    internal class Edge
    {
        public Edge(int first, int second, int cost)
        {
            this.First = first;
            this.Second = second;
            this.Cost = cost;
        }

        public int First { get; set; }
        public int Second { get; set; }
        public int Cost { get; set; }
    }

    class Program
    {    
        //network -> each node with his child edges
        static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>
        {
            { 0, new List<Edge> {new Edge(0, 5, 4), new Edge(0, 3, 9), new Edge(0, 8, 5), new Edge(0, 4, 6) } },
            { 1, new List<Edge> {new Edge(1, 4, 8), new Edge(1, 7, 7) } },
            { 2, new List<Edge> {new Edge(2, 3, 14), new Edge(2, 6, 12) } },
            { 3, new List<Edge> {new Edge(3, 2, 14), new Edge(3, 5, 2), new Edge(3, 0, 9), new Edge(3, 8, 20), new Edge(3, 6, 8) } },
            { 4, new List<Edge> {new Edge(4, 0, 6), new Edge(4, 8, 3), new Edge(4, 7, 10), new Edge(4, 1, 8) } },
            { 5, new List<Edge> {new Edge(5, 0, 4), new Edge(5, 3, 2) } },
            { 6, new List<Edge> {new Edge(6, 2, 12), new Edge(6, 3, 8), new Edge(6, 8, 9) } },
            { 7, new List<Edge> {new Edge(7, 1, 7), new Edge(7, 4, 10), new Edge(7, 8, 4) } },
            { 8, new List<Edge> {new Edge(8, 0, 5), new Edge(8, 4, 3), new Edge(8, 7, 4), new Edge(8, 6, 9), new Edge(8, 3, 20) } }
        };

        //already connected nodes (no duplicate elements)
        static HashSet<int> spanningTree = new HashSet<int>(){ 4, 0, 8, 3, 2 };
        static int budget = 20;        
        static int usedBudget = 0;

        static void Main()
        {
            Prim();
            Console.WriteLine($"Budget used: {usedBudget}");
        }

        static void Prim()
        {
            //initialize priority queue and its comparer with sorted rules
            var queue = new OrderedBag<Edge>(Comparer<Edge>.Create((a, b) => a.Cost - b.Cost));

            //all alredy connected nodes -> add their childs in priority queue
            queue.AddMany(spanningTree.SelectMany(x => graph[x]));

            //while priority queue is not empty
            while (queue.Count > 0)
            {
                //get smallest edge by cost
                var min = queue.RemoveFirst();

                //check if min node connects tree node with non tree node
                var nonTreeNode = -1;
                if(spanningTree.Contains(min.First) && !spanningTree.Contains(min.Second)) //first case
                {
                    nonTreeNode = min.Second;
                }
                if (spanningTree.Contains(min.Second) && !spanningTree.Contains(min.First)) //second case
                {
                    nonTreeNode = min.First;
                }

                //no need of this edge because if connects two tree nodes(already connected) or two non tree nodes (non node of this edge is not connected to existing network)
                if(nonTreeNode == -1)
                {
                    continue;
                }

                //check if cost of new edge to be connected fit in budget
                if(budget >= min.Cost)
                {
                    budget -= min.Cost;
                    usedBudget += min.Cost;
                }
                else
                {
                    break;
                }

                //add nonTree node to already existing network
                spanningTree.Add(nonTreeNode);

                //add its child edges to priority queue
                queue.AddMany(graph[nonTreeNode]);


            }
        }
    }
}
