using System;
using SortingHelperMethods;

namespace _03_bubble_sort_recursive
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { 1, 4, 2, -1, 10 };
            BubbleSort(arr, arr.Length);
            Console.WriteLine(string.Join(" ", arr));
        }

        static void BubbleSort(int[] arr, int index)
        {
            // Base case 
            if (index == 1)
            {
                return;
            }

            // One pass of bubble sort. After this pass, the largest element is moved (or bubbled) to end.
            for (int i = 0; i < index - 1; i++)
            {
                if(Sorting.IsLess(arr[i + 1], arr[i]))
                {
                    Sorting.Swap(arr, i + 1, i);
                }
            }

            // Largest element is fixed, recur for remaining array 
            BubbleSort(arr, index - 1);
        }
    }
}
