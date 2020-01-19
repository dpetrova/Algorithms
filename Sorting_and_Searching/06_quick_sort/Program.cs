using System;
using SortingHelperMethods;

namespace _06_quick_sort
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { 1, 4, 2, -1, 10 };
            Sort(arr, 0, arr.Length - 1);
            Console.WriteLine(string.Join(" ", arr));
        }

        static void Sort(int[] arr, int startIndex, int endIndex)
        {
            // array with 1 element -> do not need to split anymore
            if (startIndex >= endIndex)
            {
                return;
            }

            // find the pivot index and rearange the elements
            int pivot = Partition(arr, startIndex, endIndex);
            // sort the left and right partitions recursively
            Sort(arr, startIndex, pivot - 1);
            Sort(arr, pivot + 1, endIndex);

        }

        // pick the first element from the unsorted partition
        // and move it in such a way that all smaller elements are on its left and all greater, to its right
        // with pivot moved to its correct place, we now have two unsorted partitions – one to the left of it and one to the right
        static int Partition(int[] arr, int lo, int hi)
        {
            int pivot = arr[lo];

            while (true)
            {
                while (arr[lo] < pivot)
                {
                    lo++;
                }

                while (arr[hi] > pivot)
                {
                    hi--;
                }

                if (lo < hi)
                {
                    if (arr[lo] == arr[hi]) return hi;

                    Sorting.Swap(arr, lo, hi);
                }
                else
                {
                    return hi;
                }
            }

        }
    }
}
