//recursive drawing

using System;

namespace _03_recursive_drawing
{
    class Program
    {
        static void Main(string[] args)
        {
            Draw(5);
        }

        static void Draw(int n)
        {
            if (n == 0) return;

            // Pre-action: print n asterisks
            Console.WriteLine(new string('*', n));

            // Recursive call: print figure of size n-1
            Draw(n - 1);

            // Post-action: print n hashtags
            Console.WriteLine(new string('#', n));

        }
    }
}
