using System;
using System.Collections.Generic;
using System.Linq;

//We have L persons and R tasks. Each person can complete at most one task. One task can be completed by at most one person. 
//We have a table that shows which people are able to complete which tasks.
//Find the maximum assignment that assigns tasks to persons in order to complete a maximum number of tasks.
//To solve the problem, we can model it as bipartite graph where the left nodes are the persons and the right nodes are the tasks
//and edges show who is able to complete each task. 
//Then we can add source and destination and model the problem as max-flow problem
//as connect source to all people and destination to the all tasks.

namespace _18_maximum_tasks_assignment
{
    class Program
    {
        static char[][] table = new char[][]
        {
            new char[] {'Y', 'N', 'Y' },
            new char[] {'N', 'Y', 'Y' },
            new char[] {'Y', 'Y', 'N' }
        };

        static int[][] graph;
        static int[] parents;

        static void Main()
        {
            var people = table.Length;
            var tasks = table[0].Length;
            var nodes = people + tasks + 2; //here we add also start and destination

            //initialize graph
            //e.g for n = 3 -> S A B C 1 2 3 D (S-start, D-destination, A/B/C-people, 1/2/3-tasks)
            graph = new int[nodes][];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new int[nodes];
            }

            int start = 0;
            int destination = graph.Length - 1;

            /* fill the edges -> set capacity = 1 if certain person can do certain task */

            //connect start to all people (S->A, S->B, S->C)
            for (int i = 0; i < people; i++)
            {
                graph[start][i + 1] = 1;
            }

            //connect all tasks to destination (1->D, 2->D, 3->D)
            for (int i = 0; i < tasks; i++)
            {
                graph[i + people + 1 ][destination] = 1;
            }

            //connect people to tasks accordingly given table
            for (int person = 0; person < people; person++)
            {
                var personTasks = table[person];

                for (int task = 0; task < tasks; task++)
                {
                    if(personTasks[task] == 'Y')
                    {
                        graph[person + 1][task + people + 1] = 1;
                    }
                }
            }

            //reset parents with -1 (no parent)            
            parents = Enumerable.Repeat(-1, graph.Length).ToArray();

            //traverse graph using BFS while there exists a path between start/destination
            while (BFS(start, destination))
            {
                int currentNode = destination;
                while (currentNode != start)
                {
                    int previousNode = parents[currentNode];

                    //modify pathFlow (capacity is always 1)
                    graph[previousNode][currentNode] = 0; // - 1
                    graph[currentNode][previousNode] = 1; // + 1

                    currentNode = previousNode;
                }               
            }

            //* reproduce solution */

            //all filled edges between nodes in graph (except those that are connect start and destination to other nodes)
            //are solutions (a task that can be done by a purson)
            var queue = new Queue<int>();
            var result = new SortedSet<string>();
            var visited = new bool[graph.Length];            
            queue.Enqueue(destination);
            visited[destination] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                for (int child = 0; child < graph.Length; child++)
                {
                    if (!visited[child] && graph[node][child] > 0)
                    {
                        queue.Enqueue(child);
                        visited[child] = true;

                        if(node != start && node != destination && child != start && child != destination)
                        {
                            result.Add($"{(char)(child - 1 + 'A')} - {node - people}");
                        }
                    }
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, result));
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

                for (int child = 0; child < graph.Length; child++)
                {
                    //if there is edge and is not visited
                    if (graph[node][child] > 0 && !visited[child])
                    {
                        queue.Enqueue(child);                       
                        visited[child] = true;
                        parents[child] = node;
                    }
                }
            }

            //return true if there is a path to the end node, otherwise false            
            return visited[end];
        }
    }
}
