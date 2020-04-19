using System;
using System.Collections.Generic;

//problem is combinatorial: generating all 2 x 2 blocks holding n letters
//We are given an integer n (4 ≤ n ≤ 10). 
//Using the first n capital Latin letters, generate all distinct blocks of 2 x 2 letters.
//Use each letter inside a block at most once (no repeating letters are allowed in the blocks). 
//We assume that blocks obtained by rotating another block are the same and should be skipped.
//You can represent blocks as strings, e.g. ABCD
//This is obviously a combinatorics problem. We need to generate variations of 4 elements out of n elements without repetitions.
//Break down the problem:
//    1) We receive an integer from the console
//    2) Based on the received number we need to generate the letters we’ll use for building the blocks
//    3) Generate all variations of 4 symbols using an algorithm you already know or one you can quickly find online
//    4) Save all blocks obtained in a collection
//    5) Keep track of rotated blocks – blocks obtained from other blocks by rotation
//    6) Output the results in the required format
//Choose appropriate data structures:
//    • A structure to hold the set of letters – an array of chars
//    • A structure to hold the currently obtained block – array of chars
//    • A structure to hold the results – HashSet<string> (blocks should be unique)
//    • A structure to hold the rotated blocks – HashSet<string>

namespace _01_blocks
{
    class Program
    {
        static char[] letters; //hold the set of letters        
        static HashSet<string> blocks = new HashSet<string>(); //hold the result
        static readonly HashSet<string> usedCombinations = new HashSet<string>(); //hold rotated blocks

        static void Main()
        {
            // 1) Get the unput data
            int numberOfLetters = int.Parse(Console.ReadLine());

            // 2) Generate the set of letters to use
            letters = new char[numberOfLetters];
            FillLetters(numberOfLetters, letters);

            // 3) Generate variations
            bool[] used = new bool[letters.Length];
            char[] currCombination = new char[letters.Length]; //hold the currently obtained block
            GenerateVariations(letters, currCombination, used);

            // 6) Print result
            PrintBlocks(blocks);
        }

        static void FillLetters(int numberOfLetters, char[] letters)
        {
            for (int i = 0; i < numberOfLetters; i++)
            {
                letters[i] = (char)('A' + i);
            }
        }

        static void GenerateVariations(char[] letters, char[] currCombination, bool[] used, int index = 0)
        {
            if(index == currCombination.Length)
            {
                // 4) Add unique blocks to result
                // 5) Keep track of rotated bocks
                AddBlock(currCombination);
            }
            else
            {
                for (int i = 0; i < letters.Length; i++)
                {
                    if (!used[i])
                    {                        
                        used[i] = true; //mark element as used
                        currCombination[index] = letters[i];
                        GenerateVariations(letters, currCombination, used, index + 1); //generate variations from current index onward
                        used[i] = false; //unmark element
                    }
                }
            }
        }

        static void AddBlock(char[] combination)
        {
            string currCombination = new string(combination);

            //check if the combination is in the rotated blocks
            //if not – add it to the result along with all rotated equivalents to the rotated blocks collection
            if (!usedCombinations.Contains(currCombination))
            {
                //add block to collections
                blocks.Add(currCombination);
                usedCombinations.Add(currCombination);
                //add rotating blocks
                usedCombinations.Add(new string(new[] { combination[3], combination[0], combination[2], combination[1] }));
                usedCombinations.Add(new string(new[] { combination[2], combination[3], combination[1], combination[0] }));
                usedCombinations.Add(new string(new[] { combination[1], combination[2], combination[0], combination[3] }));
            }
        }

        static void PrintBlocks(HashSet<string> bloks)
        {
            Console.WriteLine($"Number of blocks: {blocks.Count}");

            foreach (var block in blocks)
            {
                Console.WriteLine(block);
            }
        }
    }
}
