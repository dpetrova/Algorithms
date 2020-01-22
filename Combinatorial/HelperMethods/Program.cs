using System;

namespace HelperMethods
{
    public class Helper
    {
        static void Main()
        {

        }

        public static void Swap<T>(T[] collection, int from, int to)
        {
            T temp = collection[to];
            collection[to] = collection[from];
            collection[from] = temp;
        }

        public static bool IsLess(IComparable first, IComparable second)
        {
            return first.CompareTo(second) < 0;
        }
    }
}
