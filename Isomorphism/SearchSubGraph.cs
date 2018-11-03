using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class SearchSubGraph
    {
        public Graph G {get; private set; }
        public Graph H { get; private set; }

        public Graph SubGraph { get; private set; }


        public SearchSubGraph(Graph g1, Graph g2)
        {
            G = g1;
            H = g2;
            SubGraph = createGraphTwoVertices();
        }

        private Graph createGraphTwoVertices() //Uzupełnić typ
        {
            int[,] commonVertices = new int[G.Vertices.Length, 2];
            int indexCommonVertices = 0;
            Graph subGraph = new Graph(0);
            int[,] tab = new int[,] { { 0, 1 }, { 1, 0 } };
            for (int i = 0; i < G.Vertices.Length; i++)
                for (int j = i + 1; j < G.Vertices.Length; j++)
                    if (G.Edges.Select(x => x).Where(x => x.From == i && x.To == j).Any())
                    {
                        List<int> verticesSubGraph = new List<int>();
                        verticesSubGraph.Add(i);
                        verticesSubGraph.Add(j);
                        foreach (var e in G.Vertices[i].Neighbors)
                        {
                            if (e.Index > j && e.Index > i)
                            {
                                commonVertices[indexCommonVertices, 0] = e.Index;
                                commonVertices[indexCommonVertices, 1] = 1;
                                indexCommonVertices++;
                            }
                        }
                        foreach (var e in G.Vertices[j].Neighbors)
                        {
                            if (e.Index > i && e.Index > j)
                            {
                                bool existInCommonVertices = false;
                                for (int k = 0; k < indexCommonVertices; k++)
                                {
                                    if (commonVertices[k, 0] == e.Index)
                                    {
                                        commonVertices[k, 1]++;
                                        existInCommonVertices = true;
                                    }
                                }
                                if (!existInCommonVertices)
                                {
                                    commonVertices[indexCommonVertices, 0] = e.Index;
                                    commonVertices[indexCommonVertices, 1] = 1;
                                    indexCommonVertices++;
                                }
                            }
                        }
                        Graph tempSubGraph = searchSubGraph(verticesSubGraph, commonVertices, indexCommonVertices);
                        if (tempSubGraph.Vertices.Length > subGraph.Vertices.Length)
                            subGraph = tempSubGraph.Clone();
                    }
            return subGraph;
        }

        private Graph searchSubGraph(List<int> verticesSubGraph, int[,] commonVertices, int indexCommonVertices) // Uzupełnić typ
        {
            int[,] tmpCommonVertices = new int[commonVertices.Length,2];
            List<int> seq = new List<int>();
            for(int i=0; i<H.Vertices.Length; i++)
                seq.Add(i);
            for (int i = 0; i < indexCommonVertices; i++)
                for (int j = 0; j < 2; j++)
                {
                    tmpCommonVertices[i, j] = commonVertices[i, j];
                }
            int tmpIndexCommonVertices = indexCommonVertices;
            for(int i=0; i<1; i++)
            {
                verticesSubGraph.Add(commonVertices[i, 0]);
                Console.WriteLine(verticesSubGraph.Count);
                // Rekurencyjne tworzenie  

                // Sprawdzanie izomorfizmu teraz
            }
            return new Graph(0);
        }

        private Graph createGraph(List<int> verticesSubGraph)
        {
            int[,] matrix = new int[verticesSubGraph.Count, verticesSubGraph.Count];
            verticesSubGraph.Sort();
            for(int i=0; i<verticesSubGraph.Count - 1; i++)
            {
                for(int j=i+1; j<verticesSubGraph.Count; j++)
                {
                    foreach(var e in G.Edges)
                    {
                        if(e.From == i && e.To == j)
                        {
                            matrix[i, j] = 1;
                            matrix[j, i] = 1;
                            break;
                        }
                    }
                }
            }
            return new Graph(matrix);
        }
    }
}
