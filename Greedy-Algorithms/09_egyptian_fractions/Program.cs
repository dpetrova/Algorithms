// In mathematics, a fraction is the rational number p/q where p and q are integers. 
// An Egyptian fraction is a sum of fractions, each with numerator 1 where all denominators are different,
// e.g. 1/2 + 1/3 + 1/16 is an Egyptian fraction, but 1/3 + 1/3 + 1/5 is not (repeated denominator 3). 
// Every positive fraction(q != 0, p<q) can be represented by an Egyptian fraction, for instance, 43/48 = 1/2 + 1/3 + 1/16. 
// Given p and q, write a program to represent the fraction p/q as an Egyptian fraction.
//Hint:
//You can complete the expression by starting with the biggest fraction with numerator 1 
//which added to the expression keeps it smaller than or equal to the target fraction. 
//The biggest fraction is the one with smallest denominator – 1/2. 
//Increase the denominator until you’ve found a solution.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_egyptian_fractions
{
    class Program
    {
        static void Main()
        {
            // fraction 43/48
            long numerator = 43; // a
            long denominator = 48; // b
            List<long> result = new List<long>();

            if(denominator < numerator)
            {
                Console.WriteLine("Error (fraction is equal to or greater than 1)");
                return;
            }

            Console.Write($"{numerator}/{denominator} = ");


            var nextSmallestDenominator = 2; // c

            while (numerator != 0)
            {
                //calculate how will remain when a/b - 1/c
                //e.g. if we have 4/7 - 1/2 -> need to multiply 4*2/7*2 - 1*7/2*7 - = 1/14
                var nextNumerator = numerator * nextSmallestDenominator; // a * c
                var fractionNumerator = 1 * denominator; // b (all egyptian fractions are with numerator 1)
                var remaining = nextNumerator - fractionNumerator; // a * c - b

                if (remaining < 0)
                {
                    nextSmallestDenominator++;
                    continue;
                }

                result.Add(nextSmallestDenominator);                

                numerator = remaining;
                denominator *= nextSmallestDenominator;

                nextSmallestDenominator++;
            }

            Console.WriteLine(string.Join(" + ", result.Select(x => $"1/{x}")));
        }
    }
}
