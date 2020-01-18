using System;
using SortingHelperMethods;

namespace _01_selection_sort
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { 1, 4, 2, -1, 10 };
            SelectionSort(arr);
            Console.WriteLine(string.Join(" ", arr));
        }

        static void SelectionSort(int[] arr)
        {
            // One by one move boundary of unsorted subarray 
            for (int i = 0; i < arr.Length; i++)
            {
                //set first as started index
                int min = i;
                //find min among the other items
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (Sorting.IsLess(arr[j], arr[min]))
                    {
                        min = j;
                    }
                }

                //swap current with min
                Sorting.Swap(arr, i, min);
            }
        }
    }
}
