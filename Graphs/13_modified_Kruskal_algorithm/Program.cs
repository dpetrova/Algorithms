using System;
using System.Collections.Generic;
using System.Linq;

//Implement Kruskal's algorithm by keeping the disjoint sets in a forest where each node holds a parent + children. 
//Thus, when two sets need to be merged, the result can be easily optimized to have two levels only: root and leaves. 
//When two trees are merged, all nodes from the second (its root + root's children) 
//should be attached to the first tree's root node

namespace _13_modified_Kruskal_algorithm
{
    class Program
    {
        static List<Edge> graph = new List<Edge>
        {
            new Edge(1, 4, 8),
            new Edge(4, 0, 6),
            new Edge(1, 7, 7),
            new Edge(4, 7, 10),
            new Edge(4, 8, 3),
            new Edge(7, 8, 4),
            new Edge(0, 8, 5),
            new Edge(8, 6, 9),
            new Edge(8, 3, 20),
            new Edge(0, 5, 4),
            new Edge(0, 3, 9),
            new Edge(6, 3, 8),
            new Edge(6, 2, 12),
            new Edge(5, 3, 2),
            new Edge(3, 2, 14)
        };

        static void Main()
        {           
            var queue = new PriorityQueue<Edge>();
            foreach (var edge in graph)
            {
                queue.Enqueue(edge);
            }

            //get nodes of the graph (select and union start and end nodes and get uniques)
            var nodes = graph
                .Select(x => x.From)
                .Union(graph.Select(x => x.To))
                .Distinct()
                .OrderBy(x => x)
                .ToArray();
                        
            var resultMst = new List<Edge>();
            int mstWeight = 0;

            while (queue.Count > 0)
            {
                var minEdge = queue.ExtractMin();                
                int from = minEdge.From;
                int to = minEdge.To;
                int weight = minEdge.Weight;
                int fromRoot = FindRoot(from, nodes);
                int toRoot = FindRoot(to, nodes);                
                if (fromRoot != toRoot)
                {
                    resultMst.Add(minEdge);
                    MergeTrees(fromRoot, to, nodes);
                    mstWeight += weight;                    
                }
            }

            Console.WriteLine("Minimum spanning forest weight: " + mstWeight);
        }

        private static void MergeTrees(int fromRoot, int to, int[] nodes)
        {
            int currentRoot = nodes[to];
            while (currentRoot != fromRoot)
            {
                int oldRoot = nodes[currentRoot];
                nodes[currentRoot] = fromRoot;
                currentRoot = oldRoot;
            }
        }

        private static int FindRoot(int from, int[] nodes)
        {
            int root = nodes[from];            
            while (root != nodes[root])
            {
                root = nodes[root];
                Console.WriteLine(root);
                Console.WriteLine(nodes[root]);
            }
            
            return root;
        }
    }
}
