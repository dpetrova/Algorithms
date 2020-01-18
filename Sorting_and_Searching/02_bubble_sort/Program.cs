using System;
using SortingHelperMethods;

namespace _02_bubble_sort
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { 1, 4, 2, -1, 10 };
            BubbleSort(arr);
            Console.WriteLine(string.Join(" ", arr));
        }

        static void BubbleSort(int[] arr)
        {            
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (Sorting.IsLess(arr[j + 1], arr[j]))
                    {
                        Sorting.Swap(arr, j + 1, j);                        
                    }
                }                
            }
        }
    }
}
