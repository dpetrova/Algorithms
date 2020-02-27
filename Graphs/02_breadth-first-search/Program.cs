using System;
using System.Collections.Generic;

//Breadth first search (BFS) for graph traversal

namespace _02_breadth_first_search
{
    class Program
    {
        static List<int>[] graph;
        static bool[] visited;

        static void Main()
        {
            //every list represents a node
            graph = new List<int>[]
            {
                new List<int>{1, 3, 5}, //node 0
                new List<int>{2},       //node 1
                new List<int>{},        //node 2
                new List<int>{4},       //node 3
                new List<int>{},        //node 4
                new List<int>{}         //node 5
                
            };

            visited = new bool[graph.Length];

            BFS(0);
            //for (int i = 0; i < graph.Length; i++)
            //{
            //    //perform DFS for every node
            //    BFS(i);
            //}
        }

        static void BFS(int node)
        {
            var queue = new Queue<int>();

            //add current node in the queue
            queue.Enqueue(node);
            visited[node] = true;

            //while there are nodes in the queue
            while (queue.Count > 0)
            {
                //remove last node from the queue
                var currNode = queue.Dequeue();

                //print curr node
                Console.Write($"{currNode} ");

                //traverse all children of the node
                foreach (var child in graph[currNode])
                {
                    if(!visited[child])
                    {
                        // add each child in the queue
                        queue.Enqueue(child);
                        visited[child] = true;
                    }
                    
                }
            }
           
        }
    }
}
