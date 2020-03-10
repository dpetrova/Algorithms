using System;
using System.Collections.Generic;
using System.Linq;

// Kruskal's Algorithm to find MST
// Spanning Tree -> subgraph without cycles (tree) connects all vertices together
// Minimum Spanning Tree -> if edges have weights MST is those Spaning Tree which has min weight (sum of its edges' weights)

namespace _09_Kruskal_algorithm_MST_
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

        static int[] parents; //hold parent of each node
        
      
        static void Main()
        {
            //get nodes of the graph (select and union start and end nodes and get uniques)
            var nodes = graph
                .Select(x => x.FirstNode)
                .Union(graph.Select(x => x.SecondNode))
                .Distinct()
                .ToList();

            //initialize parents array
            parents = new int[nodes.Count + 1]; // + 1 for ease because nodes indexes started from 1, not 0
            //set that each node is its parent itself
            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            //set to hold sorted edges by weight
            var edges = new SortedSet<Edge>(Comparer<Edge>.Create((first, second) => first.Weight - second.Weight));

            //fill all edges
            graph.ForEach(x => edges.Add(x));

            while (edges.Count > 0)
            {
                var edge = edges.Min;
                var firstNode = edge.FirstNode;
                var secondNode = edge.SecondNode;

                //find roots of both trees that firstNode and secondNode belong
                var firstRoot = FindRoot(firstNode);
                var secondRoot = FindRoot(secondNode);

                //if both roots are equal -> it means that make a cycle -> skip it
                if(firstRoot != secondRoot)
                {
                    //print edge
                    Console.WriteLine($"{firstNode} - {secondNode}");

                    //connect them into the current tree
                    parents[firstRoot] = secondRoot;
                }

                edges.Remove(edge);
            }
        }

        static int FindRoot(int node)
        {
            //go up in the tree while reach an element that is equal to itself => it is the root of the tree (node is parent to itself)
            while (parents[node] != node)
            {
                node = parents[node];
            }

            return node;
        }

        class Edge
        {
            public int FirstNode { get; set; }

            public int SecondNode { get; set; }

            public int Weight { get; set; }
        }
    }
}
