using System;
using System.Collections.Generic;

//We have a hierarchy between the employees in a company. Employees can have one or several direct managers. 
//People who manage nobody are called regular employees and their salaries are 1. 
//People who manage at least one employee are called managers. 
//Each manager takes a salary which is equal to the sum of the salaries of their directly managed employees. 
//Managers cannot manage directly or indirectly (transitively) themselves. 
//Some employees might have no manager (like the big boss).

namespace _08_salaries
{
    class Program
    {
        static List<int>[] graph = new List<int>[]
        {
            new List<int>{},        //employee 0
            new List<int>{0, 2, 5}, //employee 1 -> manager of 0, 2, 5
            new List<int>{0, 5},    //employee 2 -> maneger of 0, 5
            new List<int>{},        //employee 3
            new List<int>{0, 2},    //employee 4 -> manager of 0 and 2 
            new List<int>{0, 3}     //employee 5 -> manager of 0 and 3
        };
              
        static bool[] visited = new bool[graph.Length];
        static int[] salaries = new int[graph.Length];

        static void Main()
        {               
            //calculate salaries
            for (int i = 0; i < graph.Length; i++)
            {
                CalculateSalaries(i);
            }

            //print salaries
            for (int i = 0; i < salaries.Length; i++)
            {
                Console.WriteLine($"employee{i} -> salary {salaries[i]}");
            }
        }

        static void CalculateSalaries(int node)
        {
            //already visited
            if (salaries[node] != 0 || visited[node])
            {
                return;
            }

            //regular employee who has salary 1
            if (graph[node].Count == 0)
            {
                salaries[node] = 1;
                return;
            }

            visited[node] = true;

            //calculate salary for manegers who have employees
            int sum = 0;
            foreach (int child in graph[node])
            {
                if (salaries[child] == 0)
                {
                    CalculateSalaries(child);
                }

                sum += salaries[child];
            }

            salaries[node] = sum;
        }
    }
}
