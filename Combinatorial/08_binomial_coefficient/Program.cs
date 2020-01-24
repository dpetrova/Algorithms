using System;

namespace _08_binomial_coefficient
{
    class Program
    {
        static void Main()
        {
            int n = 4;
            int k = 2;
            int binomialCoeff = Binomial(4, 2);
            Console.WriteLine(binomialCoeff);
        }

        static int Binomial(int n, int k)
        {
            if(k > n)
            {
                return 0;
            }

            if(k == 0 || k == n)
            {
                return 1;
            }

            return Binomial(n - 1, k - 1) + Binomial(n - 1, k);
        }
    }
}
