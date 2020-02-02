using System;
//Fi
namespace _01_fibonacci_top_down_with_memoization
{
    class Program
    {
        static int[] memoization;
        static int n;

        static void Main()
        {
            n = 5;
            memoization = new int[n + 1];
            Console.WriteLine(Fibonacci(n)); 
        }

        static int Fibonacci(int n)
        {
            //already calculate it
            if(memoization[n] != 0)
            {
                return memoization[n];
            }

            //first fibonacci element is 0
            //second and third are 1
            if(n == 1 || n == 2)
            {
                return 1;
            }

            var result = Fibonacci(n - 1) + Fibonacci(n - 2);

            //cash result
            memoization[n] = result;

            return result;
        }
    }
}
