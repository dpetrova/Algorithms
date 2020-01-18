using System;
using SortingHelperMethods;

namespace _04_insertion_sort
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { 1, 4, 2, -1, 10 };
            InsertionSort(arr);
            Console.WriteLine(string.Join(" ", arr));
        }

        static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int curr = arr[i];
                int j = i - 1;

                // Move elements of arr[0..i-1], that are greater than current,
                //to one position ahead of their current position 
                while (j >= 0 && arr[j] > curr)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = curr;
            }
        }
    }
}
