using System;
using System.Collections.Generic;

//Depth first search (DFS) for graph traversal
 
namespace _01_depth_first_search
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

            DFS(0);
            //for (int i = 0; i < graph.Length; i++)
            //{
            //    //perform DFS for every node
            //    DFS(i);
            //}
        }

        static void DFS(int currentNode)
        {
            //check if node is already visited
            if(!visited[currentNode])
            {
                visited[currentNode] = true;
                //first parent node from which started, last printed deepest child
                //Console.Write($"{currentNode} ");

                //recursively traversal all children of the node
                foreach (var child in graph[currentNode])
                {
                    DFS(child);
                }

                //first print deepest node, last print those node from which started
                Console.Write($"{currentNode} ");
            }
        }
    }
}
