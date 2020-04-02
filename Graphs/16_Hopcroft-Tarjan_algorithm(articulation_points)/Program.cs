using System;
using System.Collections.Generic;

// Hopcroft-Tarjan Algorithm for finding the articulation points
// work on base of modified DFS

namespace _16_Hopcroft_Tarjan_algorithm_articulation_points_
{
    class Program
    {
        //strore the graph
        private static List<int>[] graph = new List<int>[]
        {
                new List<int>() {1, 2, 6, 7, 9},      // children of node 0
                new List<int>() {0, 6},               // children of node 1
                new List<int>() {0, 7},               // children of node 2
                new List<int>() {4},                  // children of node 3
                new List<int>() {3, 6, 10},           // children of node 4
                new List<int>() {7},                  // children of node 5
                new List<int>() {0, 1, 4, 8, 10, 11}, // children of node 6
                new List<int>() {0, 2, 5, 9},         // children of node 7
                new List<int>() {6, 11},              // children of node 8
                new List<int>() {0, 7},               // children of node 9
                new List<int>() {4, 6},               // children of node 10
                new List<int>() {6, 8},               // children of node 11
        };
        private static bool[] visited; //keep visited vertices
        private static int?[] parents; //hold parenys of vertices
        private static int[] depths; //hold calculated vertices depths
        private static int[] lowpoints; //hold calculated vertices lowpoints
        private static List<int> articulationPoints; //keep the found articulation points

        static void Main()
        {
            var articulationPoints = FindArticulationPoints(graph);
            Console.WriteLine("Articulation points: " + string.Join(", ", articulationPoints));
        }

        public static List<int> FindArticulationPoints(List<int>[] graph)
        {
            //initialize needed collections            
            visited = new bool[graph.Length];
            parents = new int?[graph.Length];
            depths = new int[graph.Length];
            lowpoints = new int[graph.Length];
            articulationPoints = new List<int>();

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);
                }
            }

            return articulationPoints;
        }


        //recursive function on base of DFS
        private static void FindArticulationPoints(int node, int depth)
        {
            //mark the current node as visited and initialize its depth and lowpoint
            visited[node] = true;
            depths[node] = depth;
            lowpoints[node] = depth;

            //keep track of the number of children of the vertex
            int childCount = 0;

            //use a variable to keep track if the current point is an articulation point
            bool isArticulation = false;

            //traverse all children of a vertex
            foreach (var childNode in graph[node])
            {
                //if node is not visited
                if (!visited[childNode])
                {
                    //mark its parent as the current vertex
                    parents[childNode] = node;

                    //recursively call the function for the child with an increased depth
                    FindArticulationPoints(childNode, depth + 1);

                    //after returning from the recursion, we increment the child count
                    childCount++;

                    //compare the lowpoint of the child with to the depth of the current vertex
                    if (lowpoints[childNode] >= depths[node])
                    {
                        //it hasn’t found a different path to a previous node, so the current vertex must be an articulation point
                        isArticulation = true;
                    }

                    //set the lowpoint for the current vertex to be the smaller between itself and the child’s lowpoint
                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[childNode]);
                }
                //in case the child was visited, but is not our direct parent 
                //(so we avoid the situation where we go to a child and check back to where we came from) 
                else if (childNode != parents[node])
                {
                    //set the lowpoint of the current vertex to be the smaller of itself and the depth of the visited child
                    lowpoints[node] = Math.Min(lowpoints[node], depths[childNode]);
                }
            }

            //after we have visited all children of a vertex we can conclude whether it’s an articulation point or not
            //it’s an articulation point if:
            //1.) if the current vertex is the starting vertex and it has at least 2 children or
            //2.) if it’s not the starting node and the isArticulation Boolean is true
            if ((parents[node] == null && childCount > 1) || (parents[node] != null && isArticulation))
            {
                articulationPoints.Add(node);
            }
        }
    }
}
