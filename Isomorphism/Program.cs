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

            Graph G, H;
            ReadGraphsFromFile.GetGraphs("../../Data/data1.txt", out G, out H);

            SearchSubGraph subGraph;
            if (G.Vertices.Length <= H.Vertices.Length)
                subGraph = new SearchSubGraph(G, H);
            else
                subGraph = new SearchSubGraph(H, G);

            Console.WriteLine(subGraph.SubGraph.Vertices.Length);
            //bool t = FullIsomorphismChecker.AreTheyIsomorphic(first, second);
        }
    }
}