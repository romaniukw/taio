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
            G = CreateExampleGraphs.CreateFromFile("../../Data/example8a.txt");
            H = CreateExampleGraphs.CreateFromFile("../../Data/example8b.txt");

            SearchSubGraph subGraph;
            if (G.Vertices.Length <= H.Vertices.Length)
                subGraph = new SearchSubGraph(G, H);
            else
                subGraph = new SearchSubGraph(H, G);
            foreach (var e in subGraph.VerticesFromGraphG)
                Console.Write(e + " ");
            Console.WriteLine();
            foreach (var e in subGraph.VerticesFromGraphH)
                Console.Write(e + " ");
            Console.WriteLine();
            //ConsoleFullCheckerApp.RunApp();

            //SearchSubGraph subGraph;
            //if (G.Vertices.Length <= H.Vertices.Length)
            //    subGraph = new SearchSubGraph(G, H);
            //else
            //    subGraph = new SearchSubGraph(H, G);

            //  var r = FindGraphByApproximationAlgorithm.Search(G, H);
            //  Console.WriteLine(subGraph.SubGraph.Vertices.Length);
            //bool t = FullIsomorphismChecker.AreTheyIsomorphic(first, second);
        }
    }
}