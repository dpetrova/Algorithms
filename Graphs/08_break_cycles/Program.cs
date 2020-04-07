using System;
using System.Collections.Generic;
using System.Linq;

//You are given undirected multi-graph. Remove a minimal number of edges to make the graph acyclic (to break all cycles). 
//As a result, print the number of edges removed and the removed edges. 
//If several edges can be removed to break a certain cycle, remove the smallest of them in alphabetical order
//algorithm:
//1.sort edges
//2.loop through edges and continuosly remove first
//3.check if there are path between these two nodes: if no path ->return back the edge, because it does not generate cycle

namespace _08_break_cycles
{
    class Program
    {
        static SortedDictionary<int, List<int>> graph = new SortedDictionary<int, List<int>>
        {
            {1, new List<int>{ 2, 5, 4 } },
            {2, new List<int>{ 1, 3 } },
            {3, new List<int>{ 2, 5 } },
            {4, new List<int>{ 1 } },
            {5, new List<int>{ 1, 3 } },
            {6, new List<int>{ 7, 8 } },
            {7, new List<int>{ 6, 8 } },
            {8, new List<int>{ 6, 7 } }
        };

        static void Main()
        {
            var result = new List<string>();

            foreach (var item in graph)
            {
                var startNode = item.Key;

                foreach (var endNode in graph[startNode].OrderBy(x => x))
                {
                    //remove edge
                    graph[startNode].Remove(endNode);
                    graph[endNode].Remove(startNode);

                    if(HasPath(startNode, endNode))
                    {
                        result.Add($"{startNode} - {endNode}");
                    }
                    else
                    {
                        //restore edge
                        graph[startNode].Add(endNode);
                        graph[endNode].Add(startNode);
                    }
                }
            }

            Console.WriteLine($"Edges to remove: {result.Count}");
            result.ForEach(Console.WriteLine);
        }

        //check if path exists between two nodes using BFS
       static bool HasPath(int start, int end)
        {
            var queue = new Queue<int>();
            var visited = new bool[graph.Count + 1]; //nodes start from 1 (not zero)

            queue.Enqueue(start);
            visited[start] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var child in graph[node])
                {
                    if (!visited[child])
                    {
                        visited[child] = true;
                        queue.Enqueue(child);

                        if (child == end) return true; //path exists
                    }
                }
            }

            return false; //no path
        }
    }
}
