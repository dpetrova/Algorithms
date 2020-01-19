using System;

namespace _05_merge_sort
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
            if(startIndex >= endIndex)
            {
                return;
            }

            //find the middle of array to split ot it
            int middleIndex = (startIndex + endIndex) / 2;

            //split and sort 
            Sort(arr, startIndex, middleIndex); //left subarray
            Sort(arr, middleIndex + 1, endIndex); //end subarray

            // merge subarrays
            //left subarray is arr[startIndex...middleIndex]; right subarray is arr[middleIndex + 1...endIndex]
            Merge(arr, startIndex, middleIndex, endIndex);
        }
        
        static void Merge(int[] arr, int lo, int mid, int hi)
        {
            //check if subarrays are already sorted or index is outside the array
            if(arr[mid] <= arr[mid + 1] || mid < 0 || mid + 1 > arr.Length)
            {
                return;
            }

            //keep initial unsorted elements in a helper array, so in our original array we can directly put sorted elements
            int[] helpArray = new int[arr.Length];
            for (int i = lo; i <= hi; i++)
            {
                helpArray[i] = arr[i];

            }
            
            int leftCurrIndex = lo;
            int rightCurrIndex = mid + 1;

            for (int i = lo; i <= hi; i++)
            {
                // if all elements in left subarray are already moved -> get elements from right subarray and move curr rigth index on
                if(leftCurrIndex > mid)
                {
                    arr[i] = helpArray[rightCurrIndex];
                    rightCurrIndex++;
                }
                // if all elements in right subarray are already moved -> get elements from left subarray and move curr left index on
                else if (rightCurrIndex > hi)
                {
                    arr[i] = helpArray[leftCurrIndex];
                    leftCurrIndex++;
                }
                // compare two elements form left and right subarrays
                else if (helpArray[leftCurrIndex] <= helpArray[rightCurrIndex])
                {
                    arr[i] = helpArray[leftCurrIndex];
                    leftCurrIndex++;
                }
                // compare two elements form left and right subarrays
                else
                {
                    arr[i] = helpArray[rightCurrIndex];
                    rightCurrIndex++;
                }
            }            
        }
    }
}
