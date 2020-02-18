using System;

//We are in a rectangular room. 
//On each side of the room there is a permutation of the cables, 
//e.g. on one side we always have ordered {1, 2, 3, 4, 5} and on the other side we have some permutation {5, 1, 3, 4, 2}.
//We are trying to connect each cable from one side with the corresponding cable on the other side
//connect 1 with 1, 2 with 2, etc. Cables are straight and should not overlap!
//The task is to find the maximum number of pairs we can connect given the restrictions above.

namespace _16_connecting_cables
{
    class Program
    {
        static void Main()
        {
            int[] cables = new int[] { 4, 3, 1, 2 };
            int[,] connections = FindAllConnections(cables);
            Console.WriteLine("Max possible connections: " + connections[connections.GetLength(0) - 1, connections.GetLength(1) - 1]);
        }

        static int[,] FindAllConnections(int [] cables)
        {
            int[,] connections = new int[cables.Length + 1, cables.Length + 1];

            for (int cableIndex = 1; cableIndex < connections.GetLength(0); cableIndex++)
            {
                var cable = cables[cableIndex - 1];

                for (int socket = 1; socket < connections.GetLength(1); socket++) //sockets are ordered {1,2,3,4,...}
                {
                    var up = connections[cableIndex - 1, socket];
                    var left = connections[cableIndex, socket - 1];
                    var diagonalUpLeft = connections[cableIndex - 1, socket - 1];

                    if (cable == socket)
                    {
                        connections[cableIndex, socket] = diagonalUpLeft + 1;
                    }                    
                    else
                    {
                        connections[cableIndex, socket] = Math.Max(up, left);
                    }
                }
            }

            //print connections matrix
            for (int i = 0; i < connections.GetLength(0); i++)
            {
                for (int j = 0; j < connections.GetLength(1); j++)
                {
                    Console.Write(connections[i, j] + " ");
                }
                Console.WriteLine();
            }

            return connections;
        }
    }
}
