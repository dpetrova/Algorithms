﻿using System;

namespace _06_combinations
{
    class Program
    {
        static int n = 3;
        static int k = 2;
        static int[] elements;
        static int[] combinations;

        static void Main()
        {
            elements = new int[] { 1, 2, 3 };
            combinations = new int[k];
            Combine(0, 0);
        }

        static void Combine(int index, int start)
        {
            if(index == k)
            {
                Console.WriteLine(string.Join(" ", combinations));
            }
            else
            {
                for (int i = start; i < elements.Length; i++)
                {
                    combinations[index] = elements[i];
                    Combine(index + 1, i + 1);
                }
            }
        }
    }
}
