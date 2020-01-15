//In this problem, you have three rods (let’s call them source, destination and spare).
//Initially, there are n disks, all placed on the source rod
//Your objective is to move all disks from the source rod to the destination rod.There are several rules:
//1) Only one disk can be moved at a time
//2) Only the topmost disk on a rod can be moved
//3) A disk can only be placed on top of a larger disk or on an empty rod

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private static Stack<int> source;
    private static Stack<int> destination = new Stack<int>();
    private static Stack<int> spare = new Stack<int>();
    private static int stepsTaken = 0;

    static void Main()
    {
        int numberOfDisks = int.Parse(Console.ReadLine());
        source = new Stack<int>(Enumerable.Range(1, numberOfDisks).Reverse());
        PrintRods();
        MoveDisks(numberOfDisks, source, destination, spare);
    }

    private static void MoveDisks(int bottomDisk, Stack<int> source, Stack<int> destination, Stack<int> spare)
    {
        if (bottomDisk == 1)
        {
            stepsTaken++;
            destination.Push(source.Pop());
            Console.WriteLine($"Step #{stepsTaken}: Moved disk");
            PrintRods();
        }
        else
        {
            MoveDisks(bottomDisk - 1, source, spare, destination);
            stepsTaken++;
            destination.Push(source.Pop());
            Console.WriteLine($"Step #{stepsTaken}: Moved disk");
            PrintRods();
            MoveDisks(bottomDisk - 1, spare, destination, source);
        }
    }

    private static void PrintRods()
    {
        Console.WriteLine($"Source: {String.Join(", ", source.Reverse())}");
        Console.WriteLine($"Destination: {String.Join(", ", destination.Reverse())}");
        Console.WriteLine($"Spare: {String.Join(", ", spare.Reverse())}");
        Console.WriteLine();
    }
}