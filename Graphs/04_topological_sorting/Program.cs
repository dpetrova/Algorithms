using System;
using System.Collections.Generic;
using System.Linq;

//Source Removal Topological Sorting (ordering) of a directed graph 
//Linear ordering of its vertices, such that for every directed edge from vertex u to vertex v, u comes before v in the ordering

namespace _04_topological_sorting
{
    class Program
    {
        static List<int>[] graph;

        static void Main()
        {
            graph = new List<int>[]
            {
                new List<int>{1, 2},    //node 0
                new List<int>{3, 4},    //node 1
                new List<int>{5},       //node 2
                new List<int>{2, 5},    //node 3
                new List<int>{3},       //node 4
                new List<int>{},        //node 5                
            };

            //list to hold topologically sorted nodes
            var sortedNodes = new List<int>();

            //list to hold nodes with no incoming edges
            var nodesNoComingEdges = new HashSet<int>();

            //list to hold nodes which have incoming edges
            var nodesWithComingEdges = GetNodesWithIncomingEdges();

            //find nodes with no incoming edges
            for (int i = 0; i < graph.Length; i++)
            {
                if (!nodesWithComingEdges.Contains(i))
                {
                    nodesNoComingEdges.Add(i);
                }
            }

            //until graph is empty remove nodes
            while (nodesNoComingEdges.Count > 0)
            {
                var currNode = nodesNoComingEdges.First();

                //remove current node
                nodesNoComingEdges.Remove(currNode);

                //add current node to sorted result
                sortedNodes.Add(currNode);

                //get children of current node
                var children = graph[currNode].ToList();

                //remove edges of current node
                graph[currNode] = new List<int>();

                //get remaining nodes with incoming edges
                var remainingNodesWithComingEdges = GetNodesWithIncomingEdges();
               
                //find child with no incoming edges
                foreach (var child in children)
                {
                    if (!remainingNodesWithComingEdges.Contains(child))
                    {
                        nodesNoComingEdges.Add(child);
                    }
                }
            }

            //if graph is not empty -> solution not found due to cycle(s) exist
            if(graph.SelectMany(x => x).Any())
            {
                Console.WriteLine("Error: graph has at least one cycle");
            }
            //solution found
            else
            {
                Console.WriteLine(string.Join(" -> ", sortedNodes));
            }
        }

        static HashSet<int> GetNodesWithIncomingEdges()
        {            
            var nodesWithIncomingEdges = new HashSet<int>();

            //find nodes with incoming edges
            graph
                .SelectMany(x => x) //concat all sub lists into one
                .ToList()
                .ForEach(x => nodesWithIncomingEdges.Add(x)); //add each node to hash set (has set hold only unique elements)

            return nodesWithIncomingEdges;
        }
    }
}
