//You are given 12 sticks of the same length, each colored with a color in the range [1 … 4]. 
//Write a program to find the number of different cubes that can be built using these sticks. 
//Note that two cubes are equal if a sequence of rotations exists that transforms the first cube to the second. 

using System;
using System.Collections.Generic;

namespace _10_cubes
{
    class Program
    {
        static void Main()
        {
            int[] colors = new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            Cube cube = new Cube();
            foreach (var item in cube.Vertices)
            {
                Console.WriteLine(item.ToString());
            }

            foreach (var item in cube.Edges)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

    
    class Cube
    {        
        int[] elements = new int[] { 0, 1 };
        int[] currVertex = new int[3];  //x,y,z
        Vertex[] currEdge = new Vertex[2]; //start, end of edge
        List<Vertex> vertices = new List<Vertex>();
        List<Edge> edges = new List<Edge>();
                
        public Cube()
        {
            //generate vertices
            GenerateVertices(0, 0);

            //generate edges
            GenerateEdges(0, 0);
        }

        public List<Vertex> Vertices {
            get { return this.vertices; }
            set
            {                
                this.vertices = value;
            }
        }

        public List<Edge> Edges
        {
            get { return this.edges; }
            set
            {
                this.edges = value;
            }
        }

        // each cube consist of vertices that are unique combinations of xyz (represented by 0 or 1)
        // to generate all verices -> variations with repetitions (n elements on k slots)
        // n = 2 (0 or 1), k = 3 (x, y, or z) => 2^3 = 8
        void GenerateVertices(int index, int start)
        {
            if (index == this.currVertex.Length)
            {
                this.Vertices.Add(new Vertex(this.currVertex[0], this.currVertex[1], this.currVertex[2]));
                return;
            }
            else
            {
                for (int i = 0; i < this.elements.Length; i++)
                {
                    this.currVertex[index] = this.elements[i];
                    GenerateVertices(index + 1, start);
                }
            }
        }

        // an edge consist from two vertices, in which one axis is changed and the others two remain same
        // edges are combinations without repetitions of vertices (8 vertices on 2 slots)
        // with some constraint that no all possible combinations can form en edge (e.g diagonals are not edges)
        void GenerateEdges(int index, int start)
        {
            if (index == this.currEdge.Length)
            {
                if(Edge.IsPossibleEdge(this.currEdge[0], this.currEdge[1]))
                {
                    this.Edges.Add(new Edge(this.currEdge[0], this.currEdge[1]));
                }
                
                return;
            }
            else
            {
                for (int i = start; i < this.vertices.Count; i++)
                {
                    this.currEdge[index] = this.vertices[i];
                    GenerateEdges(index + 1, i + 1);                    
                }
            }
        }        
    }

    class Edge
    {
        Vertex start;
        Vertex end;
        int color;

        public Edge(Vertex start, Vertex end) : this(start, end, 0)
        {            
        }

        public Edge(Vertex start, Vertex end, int color)
        {
            this.Start = start;
            this.End = end;
            this.Color = color;
        }

        public Vertex Start { get; set; }
        public Vertex End { get; set; }
        public int Color { get; set; }

        public static bool IsPossibleEdge(Vertex start, Vertex end)
        {
            //the edge is possible if one direction between the two vertices is changed, and the others remain the same
            return (start.X != end.X && start.Y == end.Y && start.Z == end.Z) || //change of X axis
                    (start.X == end.X && start.Y != end.Y && start.Z == end.Z) || //change of Y axis
                    (start.X == end.X && start.Y == end.Y && start.Z != end.Z); //change of Z axis
        }

        public override bool Equals(object obj)
        {
            Edge other = obj as Edge;           
            return (this.Start.Equals(other.Start) && this.End.Equals(other.End)) ||
                    (this.Start.Equals(other.End) && this.End.Equals(other.Start));
        }

        public override string ToString()
        {
            return string.Format("Edge: {0}, {1}", this.Start, this.End);
        }
    }

    class Vertex
    {
        int x;
        int y;
        int z;

        //each value can be either 0 or 1
        public Vertex(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }         

        public override bool Equals(object obj)
        {
            Vertex other = obj as Vertex;
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        public override string ToString()
        {
            return string.Format("Vertex: {0}{1}{2}", this.X, this.Y, this.Z);
        }
    }    
}
