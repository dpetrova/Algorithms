using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//A strongly connected component is any subgraph in a graph that fulfills the prerequisite
//that for any two vertices u and v in the subgraph there exists a path from u to v.

//Kosaraju-Sharir algorithm -> to find strongly connected components (SCC)
//The Kosaraju-Sharir algorithm works by first finding all components of the graph. 
//This can be done by using DFS, each time a DFS finishes all visited elements represent a connected component.
//rule: 
//if a vertex is part of a SCC (strongly connected component), 
//then there is a path from that vertex to any other in the SCC and from any other in the SCC to that vertex, 
//if that is true than reversing the edges should not change this 
//(if there is a path from u -> v and from v -> u, then reversing the edges would mean there is a path from v -> u and from u -> v)

namespace _15_Kosaraju__Sharir_algorithm_SCC_
{
    class Program
    {
        private static List<int>[] graph = new List<int>[]
        {
                new List<int>() {1, 11, 13}, // children of node 0
                new List<int>() {6},         // children of node 1
                new List<int>() {0},         // children of node 2
                new List<int>() {4},         // children of node 3
                new List<int>() {3, 6},      // children of node 4
                new List<int>() {13},        // children of node 5
                new List<int>() {0, 11},     // children of node 6
                new List<int>() {12},        // children of node 7
                new List<int>() {6, 11},     // children of node 8
                new List<int>() {0},         // children of node 9
                new List<int>() {4, 6, 10},  // children of node 10
                new List<int>() {},          // children of node 11
                new List<int>() {7},         // children of node 12
                new List<int>() {2, 9},      // children of node 13
        };
        private static List<int>[] reversedGraph;
        private static bool[] visited;
        private static Stack<int> dfsNodesStack;
        private static List<List<int>> stronglyConnectedComponents;

        static void Main()
        {
            //initialize needed variables
            stronglyConnectedComponents = new List<List<int>>();            
            visited = new bool[graph.Length];
            dfsNodesStack = new Stack<int>(); //store information for the order in which we visited the vertexes for every component

            //build reverse graph
            BuildReverseGraph();

            //traverse the graph with DFS and push all nodes in the stack in post-order (on return from recursion)
            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    DFS(node);
                }
            }

            //reset visited array
            visited = new bool[graph.Length];

            //traverse the nodes from the stack and perform reverse DFS to find SCC
            while (dfsNodesStack.Count > 0)
            {
                //starting from the top of the Stack we pop an element and start a DFS in the reversed graph
                var node = dfsNodesStack.Pop();
                if (!visited[node])
                {
                    stronglyConnectedComponents.Add(new List<int>()); //add new SCC
                    ReverseDFS(node);
                }
            }
            
            //print found SCC
            Console.WriteLine("Strongly Connected Components:");
            foreach (var component in stronglyConnectedComponents)
            {
                Console.WriteLine("{{{0}}}", string.Join(", ", component));
            }
        }

        private static void BuildReverseGraph()
        {
            reversedGraph = new List<int>[graph.Length];

            //traverse reserved graph and initialize paths for every node
            for (int node = 0; node < reversedGraph.Length; node++)
            {
                reversedGraph[node] = new List<int>(); //paths for every node
            }

            //traverse original graph and reverse the paths between nodes
            for (int node = 0; node < graph.Length; node++)
            {
                foreach (var childNode in graph[node])
                {
                    //add the reverse edge
                    reversedGraph[childNode].Add(node);
                }
            }
        }

        private static void DFS(int node)
        {
            if (!visited[node])
            {
                visited[node] = true;
                foreach (var childNode in graph[node])
                {
                    DFS(childNode);
                }

                //add node to stack
                dfsNodesStack.Push(node);
            }
        }

        private static void ReverseDFS(int node)
        {
            if (!visited[node])
            {
                //mark as visited
                visited[node] = true;

                //add current node to the last list of strongly connected components
                stronglyConnectedComponents.Last().Add(node);

                foreach (var childNode in reversedGraph[node])
                {
                    ReverseDFS(childNode);
                }
            }
        }
    }
}
