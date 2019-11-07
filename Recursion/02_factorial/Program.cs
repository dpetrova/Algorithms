//recursive method that calculate n!

using System;

namespace _02_factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;

            var factorial = Factorial(n);
            Console.WriteLine(factorial);
        }

        static long Factorial(int number)
        {
            if(number == 0)
            {
                return 1;
            }

            return number * Factorial(number - 1);

        }
    }
}
