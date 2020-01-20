using System;

namespace _10_interpolation_search
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[] { -1, 1, 2, 4, 10, 14 }; //work on sorted array
            int existingIndex = InterpolationSearch(arr, 10);
            int nonExisitngIndex = InterpolationSearch(arr, 0);
            Console.WriteLine("index of 10: {0}", existingIndex);
            Console.WriteLine("index of 0: {0}", nonExisitngIndex);
        }

        static int InterpolationSearch(int[] arr, int searchElement)
        {
            int start = 0;
            int end = arr.Length - 1;

            while (arr[start] <= searchElement && arr[end] >= searchElement)
            {
                int mid = start + ((searchElement - arr[start] * (end - start)) / (arr[end] - arr[start]));

                //search in right part (move start index after mid, end index remain the same)
                if(searchElement > arr [mid])
                {
                    start = mid + 1;
                }
                //search in left part (start index remain the same, move end index before mid
                else if(searchElement < arr [mid])
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
