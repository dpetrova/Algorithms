using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_variations
{
    class Program
    {
        static int[] elements;
        static int[] variations;
        static bool[] used;

        static void Main()
        {
            int n = 3;
            int k = 2;
            elements = new int[] { 1, 2, 3 };            
            variations = new int[k];
            used = new bool[n];
            Variate(0);
        }

        static void Variate(int index)
        {
            if(index == variations.Length)
            {
                Console.WriteLine(string.Join(" ", variations));
            }
            else
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    if (!used[i])
                    {
                        variations[index] = elements[i];
                        used[i] = true;
                        Variate(index + 1);
                        used[i] = false;
                    }
                }
            }
        }
    }
}
