//a program that reverses and prints an array

using System;
using System.Linq;

namespace _09_reverse_array
{    
    class Program
    {
        //static int[] arr = new int[] { 1, 2, 3, 4, 5, 6 };
        //static int[] reverse = new int[arr.Length];

        static void Main()
        {
            //Reverse(0);
            int[] arr = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

            Reverse(arr, 0, arr.Length - 1);
            Console.WriteLine(String.Join(" ", arr));
            //Console.WriteLine(string.Join(" ", reverse));
        }

        static void Reverse(int[] arr, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int temp = arr[start];
            arr[start] = arr[end];
            arr[end] = temp;

            Reverse(arr, start + 1, end - 1);
        }

        //private static void Reverse(int index)
        //{
        //    reverse[(reverse.Length - 1) - index] = arr[index];
        //    if (index == arr.Length - 1)
        //    {
        //        return;
        //    }
        //    else
        //    {                
        //        Reverse(index + 1);

        //    }            
        //}
    }
}
