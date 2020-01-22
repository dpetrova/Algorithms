using System;
using System.Collections.Generic;
using HelperMethods;

namespace _03_permutations_with_repetitions
{
    class Program
    {
        static int[] elements;

        static void Main()
        {
            elements = new int[] { 1, 2, 2 };
            Permute(0);
        }

        static void Permute(int index)
        {
            if(index == elements.Length)
            {
                Console.WriteLine(string.Join(" ", elements));
            }
            else
            {
                List<int> swapped = new List<int>();
                for (int i = index; i < elements.Length; i++)
                {
                    if (!swapped.Contains(elements[i]))
                    {
                        Helper.Swap(elements, index, i);
                        Permute(index + 1);
                        Helper.Swap(elements, index, i);
                        swapped.Add(elements[i]);
                    }                    
                }
            }
        }
    }
}
