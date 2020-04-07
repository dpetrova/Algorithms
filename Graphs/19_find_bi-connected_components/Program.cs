using System;
using System.Collections.Generic;
using System.Linq;

//Finding the articulation points in an undirected graph is a well-known problem in computer science. 
//A related problem (a bit harder) is to find the bi-connected components in a graph – its set of maximal bi-connected subgraphs.
//Each bi-connected component has the property that removing any of its nodes,
//does not break the paths between all its other nodes. 

namespace _19_find_bi_connected_components
{
    class Program
    {
        static List<int>[] graph = new List<int>[]
        {
           new List<int>{1, 2, 6, 7, 9},    //node 0
           new List<int>{0, 6},             //node 1
           new List<int>{0, 7},             //node 2
           new List<int>{4},                //node 3
           new List<int>{3, 6, 10},         //node 4
           new List<int>{7},                //node 5
           new List<int>{0, 1, 8, 10, 11},     //node 6
           new List<int>{0, 2, 5, 9, 12},   //node 7
           new List<int>{6, 11},            //node 8
           new List<int>{0, 7},             //node 9
           new List<int>{4, 6},             //node 10
           new List<int>{6, 8},             //node 11      
           new List<int>{7}                 //node 12      
        };
        static bool[] visited;
        static int[] depth;
        static int[] lowpoint;
        static int[] parent;       
        static int counter;
        static Stack<KeyValuePair<int, int>> biconnectedConnectedComponents;
        static List<List<int>> result;

        static void Main()
        {
            //get nodes of the graph
            var nodes = graph
                .SelectMany(x => x)               
                .Distinct()
                .OrderBy(x => x)
                .ToArray();
            
            //initialized collections
            visited = new bool[nodes.Length];
            depth = new int[nodes.Length];
            lowpoint = new int[nodes.Length];
            parent = new int[nodes.Length];
            biconnectedConnectedComponents = new Stack<KeyValuePair<int, int>>();
            result = new List<List<int>>();

            //reset parents with -1 (no parent)            
            parent = Enumerable.Repeat(-1, graph.Length).ToArray();
            
            FindBiconnectedComponents(0, 1);

            //print number of bi-connected components
            Console.WriteLine($"Number of bi-connected components: {counter}");

            //print bi-connected components
            foreach (var component in result)
            {
               
                Console.WriteLine($"{{{string.Join(", ", component)}}}");
            }
        }

        private static void FindBiconnectedComponents(int node, int d)
        {
            visited[node] = true;
            depth[node] = d;
            lowpoint[node] = d;

            foreach (var childNode in graph[node])
            {
                if (!visited[childNode])
                {
                    parent[childNode] = node;
                    FindBiconnectedComponents(childNode, d + 1);

                    biconnectedConnectedComponents.Push(new KeyValuePair<int, int>(node, childNode));
                    if (lowpoint[childNode] >= depth[node])
                    {                        
                        var biConnectedComponent = new List<int>();
                        counter++;
                        if (biconnectedConnectedComponents.Count > 0)
                        {
                            var edge = biconnectedConnectedComponents.Peek();
                            biConnectedComponent.Add(edge.Key);                            
                            do
                            {
                                edge = biconnectedConnectedComponents.Pop();                                
                                biConnectedComponent.Add(edge.Value);

                            } while (biconnectedConnectedComponents.Count > 0 && (edge.Key != node || edge.Value == biconnectedConnectedComponents.Peek().Key));
                                                        
                            result.Add(biConnectedComponent);
                        }
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[childNode]);
                }
                else if (childNode != parent[node])
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depth[childNode]);
                }
            }
        }
    }
}
