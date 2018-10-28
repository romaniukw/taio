using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] tab = new int[,] { { 0, 1 }, { 1, 0 } };
            int[,] fullGraph = new int[,] { { 0, 1, 1, 1, 1 }, { 1, 0, 1, 1, 1 }, { 1, 1, 0, 1, 1 }, { 1, 1, 1, 0, 1 }, { 1, 1, 1, 1, 0 } };

            Graph G = new Graph(fullGraph);
            Graph H = new Graph(fullGraph);
            SearchSubGraph subGraph;
            if (G.Vertices.Length <= H.Vertices.Length)
                subGraph = new SearchSubGraph(G, H);
            else
                subGraph = new SearchSubGraph(H, G);

        }
    }
}
