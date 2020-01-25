using System;
using System.Collections.Generic;

namespace _09_snakes
{
    class Program
    {
        static int n;
        static Stack<Cell> snake;
        static List<char> directions;
        static HashSet<string> used; //unordered collection of unique elements

        static void Main()
        {
            n = 4;
            snake = new Stack<Cell>();
            directions = new List<char>();
            used = new HashSet<string>();
            GenerateSnake(new Cell(0, 0), 'S');
        }

        static void GenerateSnake(Cell cell, char direction)
        {
            if (snake.Count == n)
            {
                string currentSnake = new string(directions.ToArray());
                if (!used.Contains(currentSnake))
                {
                    Console.WriteLine(currentSnake);
                    used.Add(currentSnake);
                    CheckIsomorphic(directions);
                }

                return;
            }

            if (snake.Contains(cell))
            {
                return;
            }
            else
            {
                snake.Push(cell);
                directions.Add(direction);

                // generate in right direction
                Cell right = new Cell(cell.Row, cell.Col + 1);
                GenerateSnake(right, 'R');

                // generate in down direction
                Cell down = new Cell(cell.Row + 1, cell.Col);
                GenerateSnake(down, 'D');

                // generate in left direction
                Cell left = new Cell(cell.Row, cell.Col - 1);
                GenerateSnake(left, 'L');

                // generate in up direction
                Cell up = new Cell(cell.Row - 1, cell.Col);
                GenerateSnake(up, 'U');

                snake.Pop();
                directions.RemoveAt(directions.Count - 1);
            }
        }

        static void CheckIsomorphic(List<char> directions)
        {
            //flip snake
            Flip(directions);
            if(!used.Contains(new string(directions.ToArray())))
            {
                used.Add(new string(directions.ToArray()));
            }

            //unflip snake
            Flip(directions);

            //rotatie until reach initial state (5th returns to initial state)
            for (int i = 0; i <= 5; i++)
            {
                RotateClockwise(directions);
                if (!used.Contains(new string(directions.ToArray())))
                {
                    used.Add(new string(directions.ToArray()));
                }
            }
        }

        //flip mirror
        //  xx  ->  xx
        //  x        x 
        static void Flip(List<char> directions)
        {
            for (int i = 0; i < directions.Count; i++)
            {
                switch (directions[i])
                {
                    case 'L': directions[i] = 'R'; break;
                    case 'R': directions[i] = 'L'; break;
                }
            }
        }

        //rotate clickwise
        //  xx  ->  xx  ->   x  -> x    -> xx
        //  x        x      xx     xx      x
        public static void RotateClockwise(List<char> directions)
        {
            for (int i = 0; i < directions.Count; i++)
            {
                switch (directions[i])
                {
                    case 'R': directions[i] = 'D'; break;
                    case 'L': directions[i] = 'U'; break;
                    case 'U': directions[i] = 'R'; break;
                    case 'D': directions[i] = 'L'; break;
                }
            }
        }
    }


    class Cell
    {
        int row;
        int col;

        public Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
        public int Row { get; set; }

        public int Col { get; set; }
    }

}







