using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

//We have a set of towns and some of them are connected by direct paths. 
//Each path has a coefficient of reliability (in percentage): the chance to pass without incidents. 
//Your goal is to compute the most reliable path between two given nodes. 
//Assume all percentages will be integer numbers and round the result to the second digit after the decimal separator.

namespace _14_most_reliable_path
{
    internal class Edge
    {
        public Edge(int first, int second, int reliability)
        {
            this.First = first;
            this.Second = second;
            this.Reliability = reliability;
        }

        public int First { get; set; }
        public int Second { get; set; }
        public int Reliability { get; set; }
    }

    class Program
    {
        //each node with his child edges
        static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>
        {
            { 0, new List<Edge> {new Edge(0, 3, 85), new Edge(0, 4, 88)} },
            { 1, new List<Edge> {new Edge(1, 3, 95), new Edge(1, 5, 5), new Edge(1, 6, 100) } },
            { 2, new List<Edge> {new Edge(2, 4, 14), new Edge(2, 6, 95) } },
            { 3, new List<Edge> {new Edge(3, 0, 85), new Edge(3, 1, 95), new Edge(3, 5, 98) } },
            { 4, new List<Edge> {new Edge(4, 0, 88), new Edge(4, 2, 14), new Edge(4, 5, 99) } },
            { 5, new List<Edge> {new Edge(5, 1, 5), new Edge(5, 3, 98), new Edge(5, 4, 99), new Edge(5, 6, 90) } },
            { 6, new List<Edge> {new Edge(6, 1, 100), new Edge(6, 2, 95), new Edge(6, 5, 90) } }            
        };
        static int start = 0;
        static int end = 6;

        //static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>
        //{
        //    { 0, new List<Edge> {new Edge(0, 1, 94), new Edge(0, 2, 97)} },
        //    { 1, new List<Edge> {new Edge(1, 0, 94), new Edge(1, 3, 98) } },
        //    { 2, new List<Edge> {new Edge(2, 0, 97), new Edge(2, 3, 99) } },
        //    { 3, new List<Edge> {new Edge(3, 1, 98), new Edge(3, 2, 99) } }            
        //};
        //static int start = 0;
        //static int end = 1;
       
        static double[] percentages;
        static bool[] visited;

        static void Main()
        {
            /* read input */
            //Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
            //var nodes = int.Parse(Console.ReadLine().Split(' ')[1]);
            //var pathParts = Console.ReadLine().Split(' ');
            //var start = int.Parse(pathParts[1]);
            //var end = int.Parse(pathParts[3]);
            //var edges = int.Parse(Console.ReadLine().Split(' ')[1]);
            //for (int i = 0; i < edges; i++)
            //{
            //    var edgeParts = Console.ReadLine().Split(' ');
            //    var edge = new Edge(int.Parse(edgeParts[0]), int.Parse(edgeParts[1]), int.Parse(edgeParts[2]));
            //    if (!graph.ContainsKey(edge.First))
            //    {
            //        graph[edge.First] = new List<Edge>();
            //    }
            //    if (!graph.ContainsKey(edge.Second))
            //    {
            //        graph[edge.Second] = new List<Edge>();
            //    }

            //    graph[edge.First].Add(edge);
            //    graph[edge.Second].Add(edge);
            //}
            /* end read input */

            //initialize reliabilities array and initially fill all percentagea as min reliability
            percentages = Enumerable.Repeat<double>(-1, graph.Count).ToArray();
            
            //set reliability of start node (here startNode = 0) to 100% (if stay at start our reliabilitiy will be 100%)
            percentages[start] = 100;

            //initialize visited nodes array
            visited = new bool[graph.Count];

            //mark start node as visited
            visited[start] = true;

            //initialize priority queue and its comparer with sorted rules
            var queue = new OrderedBag<int>(Comparer<int>.Create((a, b) => (int)(percentages[b] - percentages[a])));

            //add start node in priority queue
            queue.Add(start);

            //initialize array to keep nodes to reconstruct path
            var prev = new int[graph.Count];
            prev[start] = -1;

            //while priority queue is not empty
            while (queue.Count > 0)
            {
                //get smallest node
                var min = queue.RemoveFirst();

                //reach node who has not path
                if (percentages[min] == -1)
                {
                    break;
                }

                foreach (var child in graph[min])
                {
                    //get other node of edge (one of them is min)
                    var otherNode = child.First == min ? child.Second : child.First;

                    //mark as visited and add to priority queue
                    if (!visited[otherNode])
                    {
                        visited[otherNode] = true;
                        queue.Add(otherNode);
                    }

                    //precalculate percentage reliability to this node
                    var newPercentage = percentages[min] * child.Reliability / 100 ;
                    
                    if(newPercentage > percentages[otherNode])
                    {
                        //update reliability
                        percentages[otherNode] = newPercentage;

                        //save prev node
                        prev[otherNode] = min;

                        //sort again priority queue
                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((a, b) => (int)(percentages[b] - percentages[a])));
                    }
                }                
            }
            
            Console.WriteLine($"Most reliable path reliability: {percentages[end]:F2}%");

            //reconstruct path
            var path = new List<int>();
            var curr = end;
            while (curr != -1)
            {
                path.Add(curr);
                curr = prev[curr];
            }

            path.Reverse();

            Console.WriteLine(string.Join(" -> ", path));
        }
    }
}
