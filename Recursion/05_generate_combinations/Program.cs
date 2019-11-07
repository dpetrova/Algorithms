//generate all combinations to extract a subset from a set
using System;

namespace _05_generate_combinations
{
    class Program
    {
        static void Main()
        {            
            var set = new []{ 1, 2, 3, 4 };
            var subset = new int[2];
            
            GenerateCombinations(0, -1, set, subset);
        }

        private static void GenerateCombinations(int index, int border, int[]set, int[]subset)
        {
            if(index == subset.Length)
            {
                
                Console.WriteLine(string.Join(" ", subset));
            }
            else
            {
                for (int i = border + 1; i < set.Length; i++)
                {                    
                    subset[index] = set[i];
                    GenerateCombinations(index + 1, i, set, subset);
                }
            }
        }
        
    }
}
