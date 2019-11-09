//Generate all n-bit vectors

using System;

namespace _04_generate_bits
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[8];
            Generate(0, arr);
        }

        static void Generate(int index, int[] vector)
        {
            if(index == vector.Length)
            {
                Console.WriteLine(string.Join(" ", vector));
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    vector[index] = i;                    
                    Generate(index + 1, vector);
                }
            }

        }
    }
}
