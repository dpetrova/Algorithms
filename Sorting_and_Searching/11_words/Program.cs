﻿// You are given a string containing Latin letters.
// Write a program that finds the number of all words with no two consecutive equal characters
// that can be generated by reordering the given letters. 
// The generated words should contain all given letters.

using System;
using System.Linq;

namespace _11_words
{
    class Program
    {
        static void Main()
        {
            string letters = "aahhhaa";            
            Permute(letters, 0, letters.Length -1);
        }

        static void Permute(string letters, int start, int end)
        {
            if (start == end)
            {
                Console.WriteLine(letters);                
            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    //if current letter to be placed is equal to previous/next -> move to next iteration
                    int prevStart = start - 1;                
                    if (prevStart >= 0 && letters[prevStart] == letters[i]) continue;
                    int prevCurr = i - 1;
                    if (prevCurr >= 0 && letters[prevCurr] == letters[start]) continue;
                    int nextCurr = i + 1;
                    if (nextCurr <= letters.Length -1 && letters[nextCurr] == letters[start]) continue;
                    //pre action: swap curr index with letter in position i
                    letters = Swap(letters, start, i);
                    //recurr
                    Permute(letters, start + 1, end);                    
                }
            }
        }

        static string Swap(string letters, int i, int j)
        {
            //Console.WriteLine(" i: {0}", letters[i]);
            //Console.WriteLine(" j: {0}", letters[j]);
            //if(i - 1 >= 0)
            //{
            //    Console.WriteLine("pre i: {0}", letters[i - 1]);
            //}
            //if (i + 1 <= letters.Length -1)
            //{
            //    Console.WriteLine("next i: {0}", letters[i + 1]);
            //}
            //if (j - 1 >= 0)
            //{
            //    Console.WriteLine("pre j: {0}", letters[j - 1]);
            //}
            //if (j + 1 <= letters.Length - 1)
            //{
            //    Console.WriteLine("pre j: {0}", letters[i + 1]);
            //}
            
           
            char[] charArray = letters.ToCharArray();
            char temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            return new string(charArray);
        }
    }
}
