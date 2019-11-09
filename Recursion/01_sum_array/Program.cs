//recursive method that find sum of all numbers in array

using System;

namespace _01_sum_array
{
    class Program
    {
        static void Main()
        {
            var numbers = new[] { 1, 2, 3, 4, 5 };
            var sum = Sum(numbers, 0);
            Console.WriteLine(sum);
        }

        static int Sum(int[] arr, int index)
        {
            if(index == arr.Length - 1) //bottom of recursion
            {
                return arr[index];
            }

            return arr[index] + Sum(arr, index + 1);
        }
    }
}
