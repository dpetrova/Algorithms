using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Find the minimum number of (directed) edges to introduce into a directed graph to make it strongly connected
//(from any vertex you can go to any other vertex)
//algorithm:
//1.Run algorithm like Tarjan-SCC algorithm to find all SCCs.
//Consider each SCC as a new vertice, link a edge between these new vertices according to the origin graph, 
//we can get a new graph.Obviously, the new graph is a Directed Acyclic Graph(DAG).
//2.In the DAG, find all vertices whose in-degree is 0, we define them {X}; 
//find all vertices whose out-degree is 0, we define them {Y}.
//3.If DAG has only one vertice, the answer is 0; otherwise, the answer is max(|X|, |Y|).

namespace _20_minimal_addition__to_strongly_connected_graph
{
    class Program
    {
        static void Main()
        {
        }
    }
}
