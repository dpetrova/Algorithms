using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Dijkstar's algorithm to find Shortest Paths in Graph

namespace _11_Dijkstra_algorithm
{
    class Edge
    {
        public int FirstNode { get; set; }
        public int SecondNode { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        static List<Edge> graph = new List<Edge>
        {
            new Edge {FirstNode = 0, SecondNode = 6, Weight = 10},
            new Edge {FirstNode = 0, SecondNode = 8, Weight = 12},
            new Edge {FirstNode = 6, SecondNode = 5, Weight = 6},
            new Edge {FirstNode = 6, SecondNode = 4, Weight = 17},
            new Edge {FirstNode = 8, SecondNode = 5, Weight = 3},
            new Edge {FirstNode = 8, SecondNode = 2, Weight = 14},
            new Edge {FirstNode = 5, SecondNode = 4, Weight = 5},
            new Edge {FirstNode = 5, SecondNode = 11, Weight = 33},
            new Edge {FirstNode = 4, SecondNode = 11, Weight = 11},
            new Edge {FirstNode = 2, SecondNode = 11, Weight = 9},
            new Edge {FirstNode = 2, SecondNode = 7, Weight = 15},
            new Edge {FirstNode = 11, SecondNode = 1, Weight = 6},
            new Edge {FirstNode = 11, SecondNode = 7, Weight = 20},
            new Edge {FirstNode = 1, SecondNode = 9, Weight = 5},
            new Edge {FirstNode = 1, SecondNode = 7, Weight = 26},
            new Edge {FirstNode = 7, SecondNode = 9, Weight = 3},
            new Edge {FirstNode = 3, SecondNode = 10, Weight = 7}
        };
        
        static HashSet<int> visitedNodes;
        static Dictionary<int, List<Edge>> childEdges;
        static int[] distances;
        static int?[] prevNode;

        static void Main()
        {
            //get nodes of the graph (select and union start and end nodes and get uniques and order them)
            var nodes = graph
                .Select(x => x.FirstNode)
                .Union(graph.Select(x => x.SecondNode))
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            visitedNodes = new HashSet<int>();
            childEdges = new Dictionary<int, List<Edge>>();           

            //fill childEdges
            foreach (var edge in graph)
            {
                if (!childEdges.ContainsKey(edge.FirstNode))
                {
                    childEdges[edge.FirstNode] = new List<Edge>();
                }

                if (!childEdges.ContainsKey(edge.SecondNode))
                {
                    childEdges[edge.SecondNode] = new List<Edge>();
                }

                childEdges[edge.FirstNode].Add(edge);
                childEdges[edge.SecondNode].Add(edge);
            }

            //initialize distances = new int[nodes.Max() + 1]; //to not need to substract index of 1
            distances = new int[nodes.Count];

            //initialize array that will hold previous node
            prevNode = new int?[nodes.Count];

            //initially fill all distances with infinity
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            //set distance of start node (here startNode = 0) to 0
            distances[nodes.First()] = 0;

            //create a priority queue to hold nodes sorted by distance
            var priorityQueue = new SortedSet<int>(Comparer<int>.Create((f, s) => distances[f] - distances[s]));

            //add first node to priority queue
            priorityQueue.Add(nodes.First());

            //while priority queue is not empty
            while (priorityQueue.Count > 0)
            {
                //get node with min distance
                var minNode = priorityQueue.Min;
                priorityQueue.Remove(minNode);

                //loop trough all children of minNode
                foreach (var edge in childEdges[minNode])
                {
                    //get childNode (in the edge the one node is minNothe, the other is those node of edge that is not equal the minNode)
                    var childNode = edge.FirstNode == minNode ? edge.SecondNode : edge.FirstNode;

                    //check if child is not visited (if value is infinity -> node is not visited)
                    if(distances[childNode] == int.MaxValue)
                    {
                        //add child in priority queue
                        priorityQueue.Add(childNode);
                    }

                    //for each edge precalculate distance (distance to child = distance to parent node + edge weight)
                    var newDistance = distances[minNode] + edge.Weight;

                    //as distance to child get min distance so far
                    //d[S → A] = min(d[S → A], d[S → B] + weight[B → A])
                    if (newDistance < distances[childNode])
                    {
                        //set new min distance
                        distances[childNode] = newDistance;

                        //set prev node
                        prevNode[childNode] = minNode;

                        //reorder priority queue (as initialize new SortedSet that will trigger sorting)
                        priorityQueue = 
                            new SortedSet<int>(priorityQueue, Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                        
                    }
                }
            }
                        
            //reconstruct the shortest path from sourceNode to destination {0 -> 9}
            var destinationNode = 9;
            var path = new List<int>();
            int? currNode = destinationNode;
            while (currNode != null)
            {
                path.Add(currNode.Value);
                currNode = prevNode[currNode.Value];
            }

            path.Reverse();
            Console.WriteLine(string.Join(" -> ", path));
        }
    }
}
