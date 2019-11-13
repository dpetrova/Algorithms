//a program that simulates the execution of n nested loops from 1 to n which prints the values of all its iteration variables at any given time on a single line

using System;

namespace _10_nested_loops
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int[] array = new int[n];            
            GenerarateValues(array, 0);
        }

        private static void GenerarateValues(int[] array, int idx)
        {
            if (idx == array.Length)
            {
                Console.WriteLine(string.Join(" ", array));
                return;
            }

            for (int i = 1; i <= array.Length; i++)
            {
                array[idx] = i;
                GenerarateValues(array, idx + 1);
            }
        }
    }
}
