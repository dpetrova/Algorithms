using System;


namespace _05_variations_with_repetitions
{
    class Program
    {
        static int[] elements;
        static int[] variations;

        static void Main()
        {
            int n = 3;
            int k = 2;
            elements = new int[] { 1, 2, 3 };
            variations = new int[k];
            Variate(0);
        }

        static void Variate(int index)
        {
            if(index == variations.Length)
            {
                Console.WriteLine(string.Join(" ", variations));
            }
            else
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    variations[index] = elements[i];
                    Variate(index + 1);
                }
            }
        }
    }
}
