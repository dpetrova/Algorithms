using System;

namespace _09_binary_search_recursive
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { -1, 1, 2, 4, 10 }; //work on sorted array
            int existingIndex = BinarySearch(arr, 10, 0, arr.Length - 1);
            int nonExisitngIndex = BinarySearch(arr, 0, 0, arr.Length - 1);
            Console.WriteLine("index of 10: {0}", existingIndex);
            Console.WriteLine("index of 0: {0}", nonExisitngIndex);
        }

        static int BinarySearch(int[] arr, int searchElement, int start, int end)
        {
            if( end < start)
            {
                return -1;
            }
            else
            {
                int mid = (start + end) / 2;
                if (searchElement < arr[mid])
                {
                    return BinarySearch(arr, searchElement, start, mid - 1);
                }
                else if (searchElement > arr[mid])
                {
                    return BinarySearch(arr, searchElement, mid + 1, end);
                }
                else
                {
                    return mid;
                }
            }            
        }
    }
}
