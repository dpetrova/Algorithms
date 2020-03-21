using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

// Prim's Algorithm to find MST
// Spanning Tree -> subgraph without cycles (tree) connects all vertices together
// Minimum Spanning Tree -> if edges have weights MST is those Spaning Tree which has min weight (sum of its edges' weights)

namespace _10_Prim_algoritm_MST_
{
    class Program
    {
        static List<Edge> graph = new List<Edge>
        {
            new Edge {FirstNode = 1, SecondNode = 2, Weight = 4},
            new Edge {FirstNode = 8, SecondNode = 9, Weight = 7},
            new Edge {FirstNode = 1, SecondNode = 3, Weight = 5},
            new Edge {FirstNode = 3, SecondNode = 4, Weight = 20},
            new Edge {FirstNode = 4, SecondNode = 5, Weight = 8},
            new Edge {FirstNode = 7, SecondNode = 8, Weight = 8},
            new Edge {FirstNode = 3, SecondNode = 5, Weight = 7},
            new Edge {FirstNode = 7, SecondNode = 9, Weight = 10},
            new Edge {FirstNode = 2, SecondNode = 4, Weight = 2},
            new Edge {FirstNode = 5, SecondNode = 6, Weight = 12}
        };

        static HashSet<Edge> spanningTree;
        static HashSet<int> visitedNodes;
        static Dictionary<int, List<Edge>> childEdges;

        static void Main()
        {
            //get nodes of the graph (select and union start and end nodes and get uniques and order them)
            var nodes = graph
                .Select(x => x.FirstNode)
                .Union(graph.Select(x => x.SecondNode))
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            spanningTree = new HashSet<Edge>();
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

            foreach (var node in nodes)
            {
                if (!visitedNodes.Contains(node))
                {
                    Prim(node);
                }
            }
        }

        static void Prim(int startingNode)
        {
            visitedNodes.Add(startingNode);

            var priorityQueue = 
                new OrderedBag<Edge>(Comparer<Edge>.Create((first, second) => first.Weight - second.Weight));

            //get child edges of starting node
            var startingNodeChildEdges = childEdges[startingNode];

            //add child edges to priority queue
            priorityQueue.AddMany(startingNodeChildEdges);

            //while priority queue is not empty
            while (priorityQueue.Count > 0)
            {
                //get min edge by weight
                var minEdge = priorityQueue.GetFirst();
                priorityQueue.Remove(minEdge);

                //check if minEdge will cause cycle
                //to not cause a cycle one node must be in tree, the other not
                var first = minEdge.FirstNode;
                var second = minEdge.SecondNode;
                var nonTreeNode = -1;
                                
                if(visitedNodes.Contains(first) && !visitedNodes.Contains(second))
                {
                    nonTreeNode = second;
                }

                if (visitedNodes.Contains(second) && !visitedNodes.Contains(first))
                {
                    nonTreeNode = first;
                }

                if (nonTreeNode == -1) continue; //both nodes are in visited -> cause cycle

                //add edge to spanning tree
                spanningTree.Add(minEdge);
                //print edge
                Console.WriteLine($"{minEdge.FirstNode} - {minEdge.SecondNode}");

                visitedNodes.Add(nonTreeNode);

                //enqueue all child nodes of nonTreeNode to priority queue
                priorityQueue.AddMany(childEdges[nonTreeNode]);
            }

        }

        class Edge
        {
            public int FirstNode { get; set; }

            public int SecondNode { get; set; }

            public int Weight { get; set; }
        }
    }
}
