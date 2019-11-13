using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_tower_of_hanoi
{
    class Program
    {
        static IEnumerable<int> range = Enumerable.Range(1, 3);
        static Stack<int> source = new Stack<int>(Enumerable.Range(1, 3).Reverse());
        static Stack<int> destination = new Stack<int>();
        static Stack<int> spare = new Stack<int>();


        static void Main()
        {
            
        }
    }
}
