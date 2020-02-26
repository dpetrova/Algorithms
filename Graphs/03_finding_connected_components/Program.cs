using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Finding connected components in a graph

namespace _03_finding_connected_components
{
    class Program
    {
        static List<int>[] graph;
        static bool[] visited;

        static void Main()
        {            
            graph = new List<int>[]
            {
                new List<int>{1, 3, 5}, //node 0
                new List<int>{2},       //node 1
                new List<int>{},        //node 2
                new List<int>{4},       //node 3
                new List<int>{},        //node 4
                new List<int>{},        //node 5
                new List<int>{7},       //node 6
                new List<int>{6},       //node 7
                
            };

            visited = new bool[graph.Length];
            var count = 0;

           
            for (int i = 0; i < graph.Length; i++)
            {
                //perform DFS for every node
                if(!visited[i])
                {
                    Console.WriteLine("Conected component: ");
                    DFS(i);
                    count++;
                    Console.WriteLine();
                }               
            }

            Console.WriteLine($"There are {count} connected components" );
        }

        static void DFS(int currentNode)
        {
            //check if node is already visited
            if (!visited[currentNode])
            {
                visited[currentNode] = true;
                
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
