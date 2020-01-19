using System;

namespace _07_linear_search
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { 1, 4, 2, -1, 10 };
            int existingIndex = LinearSearch(arr, -1);
            int nonExisitngIndex = LinearSearch(arr, 0);
            Console.WriteLine("index of -1: {0}", existingIndex);
            Console.WriteLine("index of 0: {0}", nonExisitngIndex);
        }

        static int LinearSearch(int[] arr, int element)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
