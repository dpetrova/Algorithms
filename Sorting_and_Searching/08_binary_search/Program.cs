using System;

namespace _08_binary_search
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { -1, 1, 2, 4, 10 }; //work on sorted array
            int existingIndex = BinarySearch(arr, 10);
            int nonExisitngIndex = BinarySearch(arr, 0);
            Console.WriteLine("index of 10: {0}", existingIndex);
            Console.WriteLine("index of 0: {0}", nonExisitngIndex);
        }

        static int BinarySearch(int[] arr, int searchElement)
        {
            int start = 0;
            int end = arr.Length - 1;

            while (end >= start)
            {
                //find middle index of array
                int mid = (start + end) / 2;

                //search in right part
                if(searchElement > arr[mid])
                {
                    start = mid + 1;
                }
                //search in left part
                else if(searchElement < arr[mid])
                {
                    end = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            return -1;
        }
    }
}
