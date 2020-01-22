using System;

namespace _01_permutations
{
    class Program
    {
        static int[] elements;
        static int[] permutations;
        static bool[] used;

        static void Main()
        {
            elements = new int[] { 1, 2, 3 };
            permutations = new int[elements.Length];
            used = new bool[elements.Length];
            Permute(0);
        }

        //@index -> current cell to fill
        static void Permute(int index)
        {
            if(index == elements.Length)
            {
                Console.WriteLine(string.Join(" ", permutations));
            }
            else
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    if (!used[i])
                    {
                        permutations[index] = elements[i];
                        //mark used
                        used[i] = true;
                        //recurr
                        Permute(index + 1);
                        //mark unused
                        used[i] = false;
                    }
                }
            }
        }
    }
}
