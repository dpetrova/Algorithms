using System;
using System.Collections.Generic;

//We are given a directed graph. We are given also a set of pairs of vertices (u -> startNode; v-> dextinationNode) 
//Find the shortest distance between each pair of vertices or -1 if there is no path connecting them.

namespace _05_distance_between_vertices
{
    class Program
    {
        static List<int>[] graph;
        static bool[] visited;
        static int[] distances;

        static void Main()
        {
            graph = new List<int>[]
            {
                new List<int>{3},       //node 0
                new List<int>{3},       //node 1
                new List<int>{3, 4},    //node 2
                new List<int>{5},       //node 3
                new List<int>{2, 6, 7}, //node 4
                new List<int>{},        //node 5
                new List<int>{7},       //node 6
                new List<int>{}         //node 7
            };

            // for keeping track of visited node in BFS 
            visited = new bool[graph.Length];
            // for keeping calculated distances from started node u
            distances = new int[graph.Length];

            //calculate min distance between 0 and 5 node
            int dist = CalculateMinDistanceBFS(0, 5);
            Console.WriteLine(dist);
        }

        static int CalculateMinDistanceBFS(int u, int v) //u -> startVertex; v-> dextinationVertex
        {
            // queue to do BFS
            Queue<int> vertices = new Queue<int>();

            visited[u] = true;
            distances[u] = 0;
            vertices.Enqueue(u);

            while (vertices.Count > 0)
            {
                int currNode = vertices.Dequeue();
                var children = graph[currNode];

                for (int i = 0; i < children.Count; i++)
                {                    
                    if (visited[children[i]]) continue;

                    // update distance for i
                    visited[children[i]] = true;
                    distances[children[i]] = distances[currNode] + 1;
                    vertices.Enqueue(children[i]);                                        
                }
            }

            return distances[v];
        }
    }
}
