using System;
using System.Collections.Generic;

//Write a program to check whether an undirected graph is acyclic or holds any cycles.

namespace _07_cycles_in_graph
{
    class Program
    {
        //cyclic
        static Dictionary<char, List<char>> graph = new Dictionary<char, List<char>>
        {
            { 'A', new List<char> { 'B' } },
            { 'B', new List<char> { 'C' } },
            { 'C', new List<char> { 'A' } },
        };
        //acyclic
        //static Dictionary<char, List<char>> graph = new Dictionary<char, List<char>>
        //{
        //    { 'A', new List<char> { 'B' } },
        //    { 'B', new List<char> { 'C' } },
        //    { 'C', new List<char> { 'D' } },
        //    { 'D', new List<char> {  } },
        //};
        static HashSet<char> visited = new HashSet<char>();
        static HashSet<char> currentVisited = new HashSet<char>();
        static bool hasCycle = false;
        
        static void Main()
        {
            foreach (var vertex in graph.Keys)
            {
                HasCycle(vertex);
            }

            Console.WriteLine("Acyclic: {0}", hasCycle ? "No" : "Yes");
        }

        static void HasCycle(char vertex)
        {
            if (currentVisited.Contains(vertex))
            {
                hasCycle = true;
                return;
            }

            if (visited.Contains(vertex) || hasCycle)
            {
                return;
            }

            currentVisited.Add(vertex);
            visited.Add(vertex);
           
            foreach (var child in graph[vertex])
            {
                HasCycle(child);
            }

            currentVisited.Remove(vertex);
        }
    }
}
