//Given a board of size NxN (a standard square matrix),
//a chess knight can perform a tour of the board, visiting each cell only once. 
//The knight moves according to the rules of chess (in an L-shaped pattern) and starts from the upper-left corner. 
//Write a program which finds and prints the path the knight needs to take in order to visit all cells.
//Use Warnsdorff's rule to decide which cell the knight should visit next.
//Warnsdorff's rule is a heuristic for finding a single knight's tour. 
//The knight is moved so that it always proceeds to the square from which the knight will have the fewest onward moves. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_knight_s_tour
{
    class Program
    {
        static void Main()
        {
            int n = 5;
            List<Cell> board = new List<Cell>();

            //initialize chessboard consist of cells(row, col)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Cell cell = new Cell(i, j);
                    board.Add(cell);
                }
            }

            int counter = 0;

            //starting cell is top left corner
            var currCell = board[0];
            currCell.IsVisited = true;
            currCell.MovesToVisited = ++counter;

            //loop until all cells are visited
            while (board.Any(x => !x.IsVisited))
            {
                currCell = NextCell(currCell, board);
                currCell.IsVisited = true;
                currCell.MovesToVisited = ++counter;
            }

            PrintResult(board, n);
        }

        static Cell NextCell(Cell current, List<Cell> board)
        {
            //possible moves U(up)/D(down)/L(left)/R(right) -> UUL = 2 cells up then 1 left; LLU = 2 cells left then 1 up
            //some are equal, e.g. ULL = LLU
            var UUL = board.FirstOrDefault(x => x.Row == current.Row - 2 && x.Col == current.Col - 1);
            var LLU = board.FirstOrDefault(x => x.Row == current.Row - 1 && x.Col == current.Col - 2);
            var UUR = board.FirstOrDefault(x => x.Row == current.Row - 2 && x.Col == current.Col + 1);
            var RRU = board.FirstOrDefault(x => x.Row == current.Row - 1 && x.Col == current.Col + 2);
            var DDL = board.FirstOrDefault(x => x.Row == current.Row + 2 && x.Col == current.Col - 1);
            var LLD = board.FirstOrDefault(x => x.Row == current.Row + 1 && x.Col == current.Col - 2);
            var RRD = board.FirstOrDefault(x => x.Row == current.Row + 1 && x.Col == current.Col + 2);
            var DDR = board.FirstOrDefault(x => x.Row == current.Row + 2 && x.Col == current.Col + 1);
            //if cell is out of board -> FirstOrDefault will return null

            var possibleMoves = new List<Cell> { UUL, LLU, UUR, RRU, DDL, LLD, RRD, DDR };

            //get only valid cells (moves that are out of board are null) and these that are not visited
            //and order by possible moves count of each of the cell
            possibleMoves = possibleMoves.Where(x => x != null && !x.IsVisited)
                                         .OrderBy(x => CalculateNextPossibleMoves(x, board)).ToList();

            //get cell with fewest next possible moves
            return possibleMoves.First();
        }

        static int CalculateNextPossibleMoves(Cell current, List<Cell> board)
        {
            //if cell is out of board -> FirstOrDefault will return null
            var UUL = board.FirstOrDefault(x => x.Row == current.Row - 2 && x.Col == current.Col - 1);
            var LLU = board.FirstOrDefault(x => x.Row == current.Row - 1 && x.Col == current.Col - 2);
            var UUR = board.FirstOrDefault(x => x.Row == current.Row - 2 && x.Col == current.Col + 1);
            var RRU = board.FirstOrDefault(x => x.Row == current.Row - 1 && x.Col == current.Col + 2);
            var DDL = board.FirstOrDefault(x => x.Row == current.Row + 2 && x.Col == current.Col - 1);
            var LLD = board.FirstOrDefault(x => x.Row == current.Row + 1 && x.Col == current.Col - 2);
            var RRD = board.FirstOrDefault(x => x.Row == current.Row + 1 && x.Col == current.Col + 2);
            var DDR = board.FirstOrDefault(x => x.Row == current.Row + 2 && x.Col == current.Col + 1);

            var possibleMoves = new List<Cell> { UUL, LLU, UUR, RRU, DDL, LLD, RRD, DDR };

            //filter not null cells (moves that are out of board) and these that are not visited
            possibleMoves = possibleMoves.Where(x => x != null && !x.IsVisited).ToList();

            return possibleMoves.Count;
        }

        static void PrintResult(List<Cell> board, int boardSize)
        {
            for (int i = 0; i < boardSize; i++)
            {
                int row = i;
                for (int j = 0; j < boardSize; j++)
                {
                    Cell cell = board[row * boardSize + j];
                    Console.Write(cell.MovesToVisited.ToString().PadLeft(3) + " ");
                }
                Console.WriteLine();
            }
        }
    }

    class Cell
    {
        public Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;
            this.IsVisited = false;
            this.MovesToVisited = 0;
        }

        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsVisited { get; set; }
        public int MovesToVisited { get; set; }
    }
}
