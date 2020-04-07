using System;
using System.Collections.Generic;
using System.Linq;

//Max flow algorithm – the Edmonds-Karp algorithm
//Find the maximum flow possible in the network starting from node 0 and ending in node 5
//Max flow algorithms rely on finding a path from the start vertex (source) to the end vertex (sink) using BFS. 
//Such a path is called an augmenting path

namespace _17_3_Edmonds_Karp_algorithm_MaxFlow_
{
    class Program
    {
        //graph is represented as adjacency matrix where the rows and the columns are the vertexes 
        //and the cells represent the capacity of the edge
        //for example cell [2, 4] with value 9 would mean that there is an edge from node 2 to node 4 with capacity 9, 
        //if a cell’s value is 0 then there is no edge between the nodes
        private static int[][] graph = new int[][]
        {
                new int[] { 0, 10, 10, 0, 0, 0 },
                new int[] { 0, 0, 2, 4, 8, 0},
                new int[] { 0, 0, 0, 0, 9, 0},
                new int[] { 0, 0, 0, 0, 0, 10 },
                new int[] { 0, 0, 0, 6, 0, 10 },
                new int[] { 0, 0, 0, 0, 0, 0 },
        };

        //keep track of the parents of vertexes (to can reconstruct path from end to start)
        private static int[] parents; 

        static void Main()
        {
            var maxFlow = FindMaxFlow(graph);
            Console.WriteLine($"Max flow = {maxFlow}");
        }

        public static int FindMaxFlow(int[][] graph)
        {
            //initialize collections and variables            
            parents = new int[graph.Length];
            int start = 0;
            int end = graph.Length - 1;
            int maxFlow = 0;

            //reset parents with -1 (no parent)
            parents = Enumerable.Repeat(-1, graph.Length).ToArray();

            //traverse graph using BFS while there exists a path between end(sink)/start
            while (BFS(start, end))
            {
                //find the smallest edge capacity we’ve passed in our path
                //(because the most amount of flow we can pass through the path is at most the amount we can pass through the smallest edge)
                int pathFlow = int.MaxValue;
                int currentNode = end;
                while (currentNode != start)
                {
                    int previousNode = parents[currentNode];
                    int currentFlow = graph[previousNode][currentNode];

                    //modify pathFlow to hold the smallest edge capacity in the path
                    //(go back the path and keep smallest capacity)
                    if (currentFlow > 0 && currentFlow < pathFlow)
                    {
                        pathFlow = currentFlow;
                    }

                    currentNode = previousNode;
                }

                //add pathFlow to the max flow
                maxFlow += pathFlow;

                //going back -> start modify the capacities of the edges in the augmenting path
                currentNode = end;
                while (currentNode != start)
                {
                    int previousNode = parents[currentNode];

                    //subtract path flow amount from each edge capacity in the augmenting path
                    graph[previousNode][currentNode] -= pathFlow;
                    //increase the capacity of the reverse edges with the same path flow amount
                    graph[currentNode][previousNode] += pathFlow;

                    currentNode = previousNode;
                }
            }

            return maxFlow;
        }

        //find the augmenting path using BFS
        private static bool BFS(int start, int end)
        {
            bool[] visited = new bool[graph.Length];
            var queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = true;

            //fill parents array during BFS
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                for (int child = 0; child < graph[node].Length; child++)
                {
                    //if there is edge and is not visited
                    if (graph[node][child] > 0 && !visited[child])
                    {
                        queue.Enqueue(child);
                        parents[child] = node;
                        visited[child] = true;
                    }
                }
            }

            //return true if there is a path to the sink (end) node, otherwise false
            //e.g. to the end we can not reach if we spent all available capacity
            return visited[end];
        }
    }
}
