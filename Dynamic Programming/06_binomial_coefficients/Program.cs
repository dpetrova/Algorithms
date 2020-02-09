using System;

//Write a program that finds the binomial coefficient for given non-negative integers n and k.
//The coefficient can be found recursively by adding the two numbers above using the formula:
// n    n-1     n-1
//   =       + 
// k    k-1      k
//However, this leads to calculating the same coefficient multiple times.
//Use memoization to improve performance.
// n        n!
//   =  ---------- 
// k     (n-k)!k!

namespace _06_binomial_coefficients
{
    class Program
    {
        static int[,] binomials;
        static void Main()
        {
            int n = 3;
            int k = 2;
            binomials = new int[n + 1, k + 1];

            Console.WriteLine(Binomial(n,k));
        }

        static int Binomial(int n, int k)
        {
            if (k > n)
            {
                return 0;
            }

            if (k == 0 || k == n)
            {
                return 1;
            }

            //if already calculated
            if (binomials[n,k] > 0)
            {
                return binomials[n,k];
            }

            int binomial = Binomial(n - 1, k - 1) + Binomial(n - 1, k);
            binomials[n, k] = binomial;
            return binomial;
        }
    }
    
}
