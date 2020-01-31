// This problem is about finding the proper place of numbers in an array.
// You will be given a sequence of non-decreasing integers with randomly distributed "holes" among them,
// (represented by zeros).
// Then you’ll be given the needles – numbers which should be inserted into the sequence, 
// so that it remains non-decreasing(discounting the "holes").

using System;
using System.Collections.Generic;

namespace _12_needles
{
    class Program
    {
        static void Main()
        {
            List<int> sequence = new List<int> { 3, 5, 11, 0, 0, 0, 12, 12, 0, 0, 0, 12, 12, 70, 71, 0, 90, 123, 140, 150, 166, 190, 0 };
            int[] needles = new int[] { 5, 13, 90, 1, 70, 75, 7, 188, 12 };
            
            //sort needles
            Array.Sort(needles);

            for (int i = 0; i < needles.Length; i++)
            {
                int currNeedle = needles[i];
                bool inserted = false;
                for (int j = 0; j < sequence.Count; j++)
                {
                    int currSequence = sequence[j];
                    if(currNeedle <= currSequence)
                    {
                        sequence.Insert(j, currNeedle);
                        inserted = true;
                        break;
                    }
                }
                if(!inserted)
                {
                    sequence.Add(currNeedle);
                }
            }


           Console.WriteLine(string.Join(" ", sequence));
           
        }
    }
}
