using System;
using HelperMethods;

namespace _02_permutations__swap_algorithm_
{
    class Program
    {
        static int[] elements;

        static void Main()
        {
            elements = new int[] { 1, 2, 3 };
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
                for (int i = index; i < elements.Length; i++)
                {
                    Helper.Swap(elements, index, i);
                    Permute(index + 1);
                    Helper.Swap(elements, index, i);
                }
            }
        }
    }
}
