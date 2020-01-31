//we are given two sets - a set of sets (we’ll call it sets) and a universe. 
//Universe is a set of elements
//The sets contain all elements from universe and no others, however, some elements are repeated. 
//The task is to find the smallest subset of sets which contains all elements in universe. 

namespace SetCover
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SetCover
    {
        public static void Main(string[] args)
        {
            var universe = new[] { 1, 3, 5, 7, 9, 11, 20, 30, 40 };
            var sets = new[]
            {
                new[] { 20 },
                new[] { 1, 5, 20, 30 },
                new[] { 3, 7, 20, 30, 40 },
                new[] { 9, 30 },
                new[] { 11, 20, 30, 40 },
                new[] { 3, 7, 40 }
            };

            var selectedSets = ChooseSets(sets.ToList(), universe.ToList());
            Console.WriteLine($"Sets to take ({selectedSets.Count}):");
            foreach (var set in selectedSets)
            {
                Console.WriteLine($"{{ {string.Join(", ", set)} }}");
            }
        }

        //TODO: use HashSet<> instead of List<> to optimize speed
        //HashSet is an unordered collection of unique elements and search and remove in it are much faster
        public static List<int[]> ChooseSets(IList<int[]> sets, IList<int> universe)
        {
            var result = new List<int[]>();

            while (universe.Count > 0)
            {
                //order sets descending by number of elements in them that are currently present in universe
                sets = sets.OrderByDescending(x => x.Count(e => universe.Contains(e))).ToList();

                //greedy choose this with largest number of elements
                var currSet = sets.First();

                //add current set to result
                result.Add(currSet);

                //remove current set from sets as it is already chosen
                sets.Remove(currSet);

                //remove already chosen elemenst from universe
                foreach (var element in currSet)
                {
                    universe.Remove(element);
                }
            }

            return result;
        }
    }
}
